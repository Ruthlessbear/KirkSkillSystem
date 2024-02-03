using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KSS.Runtime.Process
{
    public class SchedulerCreator
    {
        /// <summary>
        /// 创建战斗全局流程调度器
        /// </summary>
        /// <param name="data"></param>
        /// <param name="adder"></param>
        /// <returns></returns>
        public static IGlobalScheduler CreateGlobalScheduler(GlobalRuntimeData data, List<GlobalProcessAdder> adder = null)
        {
            IGlobalScheduler scheduler = new GlobalScheduler(data, adder);
            return scheduler;
        }

        /// <summary>
        /// 创建战斗裁定流程调度器
        /// </summary>
        /// <param name="data"></param>
        /// <param name="adder"></param>
        /// <returns></returns>
        public static IDecisionScheduler CreateDecisionScheduler(DecisionRuntimeData data, List<DecisionProcessAdder> adder = null)
        {
            IDecisionScheduler scheduler = new DecisionScheduler(data, adder);
            return scheduler;
        }
    }
}
