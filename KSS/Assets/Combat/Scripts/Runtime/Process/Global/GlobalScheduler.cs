using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;

namespace KSS.Runtime.Process
{
    public class GlobalRuntimeData
    {
        public List<string> schedulerList;
    }

    public class GlobalProcessAdder
    {
        public string ProcessId;
        public string BeforeProcessId;
    }

    public class GlobalScheduler : IGlobalScheduler
    {
        private Stack<IGlobalProcess> _currentScheduler;
        private IFSMStateMachine<IGlobalProcess> _fsmMachine;

        /// <summary>
        /// 基础初始化流程
        /// </summary>
        /// <param name="data"></param>
        public GlobalScheduler(GlobalRuntimeData data)
        {
            _fsmMachine = CreateStateMachine();
            _currentScheduler = InitializeScheduler(data.schedulerList, _fsmMachine);
        }

        /// <summary>
        /// 可嵌入流程初始化
        /// </summary>
        /// <param name="data"></param>
        /// <param name=""></param>
        public GlobalScheduler(GlobalRuntimeData data, List<GlobalProcessAdder> adder)
        {
            _fsmMachine = CreateStateMachine();
            _currentScheduler = InitializeScheduler(data.schedulerList, adder, _fsmMachine);
        }

        public Stack<IGlobalProcess> InitializeScheduler(List<string> schedulerList, List<GlobalProcessAdder> adder, IFSMStateMachine<IGlobalProcess> stateMachine)
        {
            if (schedulerList == null) return new Stack<IGlobalProcess>();
            if (adder == null) return InitializeScheduler(schedulerList, stateMachine);

            string[] temp = new string[schedulerList.Count];
            schedulerList.CopyTo(temp);
            var copySchedulerList = temp.ToList();

            foreach (var addProcess in adder)
            {
                int index = schedulerList.FindIndex((x) => x.Equals(addProcess.BeforeProcessId));
                if (index == -1) continue;
                copySchedulerList.Insert(index + 1, addProcess.ProcessId);
            }
            return InitializeScheduler(copySchedulerList, stateMachine);
        }

        public Stack<IGlobalProcess> InitializeScheduler(List<string> schedulerList, IFSMStateMachine<IGlobalProcess> stateMachine)
        {
            if (schedulerList == null) return new Stack<IGlobalProcess>();

            try
            {
                var asm = Assembly.GetAssembly(typeof(GlobalProcessAttribute));
                Type[] types = asm.GetExportedTypes();
                Dictionary<string, Type> processCollection = new Dictionary<string, Type>();
                foreach (var type in types)
                {
                    var attributes = type.GetCustomAttribute<GlobalProcessAttribute>();
                    if (attributes != null)
                    {
                        processCollection.Add(attributes.ProcessID, type);
                    }
                }

                Stack<IGlobalProcess> processStack = new Stack<IGlobalProcess>();
                for (int i = schedulerList.Count - 1; i>= 0; i--)
                {
                    var process = schedulerList[i];
                    if (!processCollection.ContainsKey(process))
                    {
                        UnityEngine.Debug.LogError("No corresponding process instance found, make sure to compile.");
                        continue;
                    }

                    processCollection.TryGetValue(process, out Type type);
                    var instance = Activator.CreateInstance(type) as IGlobalProcess;
                    //LK:Register Process
                    stateMachine.AddNode(instance);
                    processStack.Push(instance);
                }

                return processStack;
            }
            catch
            {
                Debug.LogError("Initialize global scheduler faild, Please check the relevant data.");
                return null;
            }
        }

        private IFSMStateMachine<IGlobalProcess> CreateStateMachine()
        {
            return new FSMStateMachine<IGlobalProcess>();
        }

        public void Next()
        {
            var process = _currentScheduler.Pop();
            _fsmMachine.ChangeNode(process);
        }
    }
}
