using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSS.Config;
using KSS.Runtime.Data;

namespace KSS.Runtime.Process
{
    /// <summary>
    /// Start开始单体技能流程
    /// </summary>
    [GlobalProcess("COMBAT_START")]
    public class GlobalStartProcess : IGlobalProcess
    {
        public void End()
        {
            throw new NotImplementedException();
        }

        public void FreezePoint()
        {
            throw new NotImplementedException();
        }

        public void Proceed()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            UnityEngine.Debug.Log("From Start: GlobalProcess Test Sucess!");
            var _dataSource = new CombatDataSource();
            DecisionRuntimeData data = new DecisionRuntimeData() { 
                schedulerList = _dataSource.GetGlobalSchedulerConfig()
            };

            IDecisionScheduler scheduler = SchedulerCreator.CreateDecisionScheduler(data);
            scheduler.Next();
        }
    }
}
