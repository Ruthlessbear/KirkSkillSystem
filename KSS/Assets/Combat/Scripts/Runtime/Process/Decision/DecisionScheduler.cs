using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;

namespace KSS.Runtime.Process
{
    public class DecisionRuntimeData
    {
        public List<string> schedulerList;
    }

    public class DecisionProcessAdder
    {
        public string ProcessId;
        public string BeforeProcessId;
    }

    public class DecisionScheduler : IDecisionScheduler
    {
        private Stack<IDecisionProcess> _currentScheduler;
        private IFSMStateMachine<IDecisionProcess> _fsmMachine;

        /// <summary>
        /// 流程初始化
        /// </summary>
        /// <param name="data"></param>
        public DecisionScheduler(DecisionRuntimeData data)
        {
            _fsmMachine = CreateStateMachine();
            _currentScheduler = InitializeScheduler(data.schedulerList, _fsmMachine);
        }

        /// <summary>
        /// 可嵌入流程初始化
        /// </summary>
        /// <param name="data"></param>
        /// <param name=""></param>
        public DecisionScheduler(DecisionRuntimeData data, List<DecisionProcessAdder> adder)
        {
            _fsmMachine = CreateStateMachine();
            _currentScheduler = InitializeScheduler(data.schedulerList, adder, _fsmMachine);
        }

        public Stack<IDecisionProcess> InitializeScheduler(List<string> schedulerList, List<DecisionProcessAdder> adder, IFSMStateMachine<IDecisionProcess> stateMachine)
        {
            if (schedulerList == null) return new Stack<IDecisionProcess>();
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

        public Stack<IDecisionProcess> InitializeScheduler(List<string> schedulerList, IFSMStateMachine<IDecisionProcess> stateMachine)
        {
            if (schedulerList == null) return new Stack<IDecisionProcess>();

            try
            {
                var asm = Assembly.GetAssembly(typeof(DecisionProcessAttribute));
                Type[] types = asm.GetExportedTypes();
                Dictionary<string, Type> processCollection = new Dictionary<string, Type>();
                foreach (var type in types)
                {
                    var attributes = type.GetCustomAttribute<DecisionProcessAttribute>();
                    if (attributes != null)
                    {
                        processCollection.Add(attributes.ProcessID, type);
                    }
                }

                Stack<IDecisionProcess> processStack = new Stack<IDecisionProcess>();
                for (int i = schedulerList.Count - 1; i >= 0; i--)
                {
                    var process = schedulerList[i];
                    if (!processCollection.ContainsKey(process))
                    {
                        UnityEngine.Debug.LogError("No corresponding process instance found, make sure to compile.");
                        continue;
                    }

                    processCollection.TryGetValue(process, out Type type);
                    var instance = Activator.CreateInstance(type) as IDecisionProcess;
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

        private IFSMStateMachine<IDecisionProcess> CreateStateMachine()
        {
            return new FSMStateMachine<IDecisionProcess>();
        }

        public void Next()
        {
            var process = _currentScheduler.Pop();
            _fsmMachine.ChangeNode(process);
        }
    }
}
