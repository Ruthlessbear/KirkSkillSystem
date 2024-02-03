using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KSS.Runtime.Process
{
    public interface IDecisionScheduler
    {
        /// <summary>
        /// 初始化载入流程配置
        /// </summary>
        Stack<IDecisionProcess> InitializeScheduler(List<string> schedulerList, IFSMStateMachine<IDecisionProcess> stateMachine);

        /// <summary>
        /// 初始化载入流程配置，可嵌入其他流程
        /// </summary>
        /// <param name="schedulerList"></param>
        /// <param name="adder"></param>
        /// <returns></returns>
        Stack<IDecisionProcess> InitializeScheduler(List<string> schedulerList, List<DecisionProcessAdder> adder, IFSMStateMachine<IDecisionProcess> stateMachine);

        /// <summary>
        /// 执行下一个流程
        /// </summary>
        void Next();
    }
}
