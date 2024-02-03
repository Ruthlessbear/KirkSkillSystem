using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KSS.Runtime.Process
{
    public interface IFSMStateMachine<T>
    {
        void CreateMachine();
        void AddNode(T process);
        void RemoveNode(T process);
        void ChangeNode(string processId, bool isEnd = false);
        void ChangeNode(T process, bool isEnd = false);
    }

    //LK:实际上可以加一个消息系统，但其与框架关联性较高，故这没有做，可以自行添加
    public class FSMStateMachine<T> : IFSMStateMachine<T>
    {
        private List<T> _processSets;
        private T _currentProcess;

        public void CreateMachine()
        {
            _processSets = new List<T>();
        }

        public void AddNode(T process)
        {
            if (!_processSets.Contains(process))
                _processSets.Add(process);
        }

        public void ChangeNode(T process, bool isEnd = false)
        {
            if (_processSets.Contains(process))
            {
                if (process.GetType().IsAssignableFrom(typeof(IGlobalProcess)))
                {
                    if (isEnd) (_currentProcess as IGlobalProcess).End();
                    (process as IGlobalProcess).Start();
                }
                else if (process.GetType().IsAssignableFrom(typeof(IDecisionProcess)))
                {
                    if (isEnd) (_currentProcess as IDecisionProcess).End();
                    (process as IDecisionProcess).Start();
                }
                _currentProcess = process;
            }
        }

        public void ChangeNode(string processId, bool isEnd = false)
        {
            foreach (var process in _processSets)
            {
                Type type = process.GetType();
                if (process.GetType().IsAssignableFrom(typeof(IGlobalProcess)))
                {
                    var attribute = type.GetCustomAttribute<GlobalProcessAttribute>();
                    if (attribute.ProcessID.Equals(processId))
                    {
                        ChangeNode(process);
                        break;
                    }
                }
                else if (process.GetType().IsAssignableFrom(typeof(IDecisionProcess)))
                {
                    var attribute = type.GetCustomAttribute<DecisionProcessAttribute>();
                    if (attribute.ProcessID.Equals(processId))
                    {
                        ChangeNode(process, isEnd);
                        break;
                    }
                }
            }
        }

        public void RemoveNode(T process)
        {
            if (_processSets.Contains(process))
                _processSets.Remove(process);
        }
    }
}
