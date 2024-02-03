using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KSS.Runtime.Process
{
    public interface IGlobalScheduler
    {
        /// <summary>
        /// 初始化载入流程配置
        /// </summary>
        Stack<IGlobalProcess> InitializeScheduler(List<string> schedulerList, IFSMStateMachine<IGlobalProcess> stateMachine);

        /// <summary>
        /// 初始化载入流程配置，可嵌入其他流程
        /// </summary>
        /// <param name="schedulerList"></param>
        /// <param name="adder"></param>
        /// <returns></returns>
        Stack<IGlobalProcess> InitializeScheduler(List<string> schedulerList, List<GlobalProcessAdder> adder, IFSMStateMachine<IGlobalProcess> stateMachine);

        /// <summary>
        /// 执行下一个流程
        /// </summary>
        void Next();
    }
}
