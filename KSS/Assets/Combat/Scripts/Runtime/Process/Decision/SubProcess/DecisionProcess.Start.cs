using System;
using System.Collections.Generic;
using System.Linq;
using KSS.Runtime.Skill;
using KSS.Runtime.Data;

namespace KSS.Runtime.Process
{
    /// <summary>
    /// Start开始单体技能流程
    /// </summary>
    [DecisionProcess("COMBAT_START")]
    public class DecisionStartProcess : DecisionBaseProcess
    {
        public override void End()
        {
            UnityEngine.Debug.Log("Global COMBAT_STAT End invoke");
        }

        public override void Execute()
        {
            UnityEngine.Debug.Log("Global COMBAT_START Execute invoke");
        }

        public override void FreezePoint()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            UnityEngine.Debug.Log("From Start: DecisionProcess Test Sucess!");
            string skillCondition = "TargetTeam:TARGET_TEAM_ENEMY;TargetType:TARGET_TYPE_V|TARGET_TYPE_VMAX;TargetFlag:TARGET_FLAG_NORMAL";
            SkillEffectData data = new SkillEffectData()
            {
                SkillEffect = string.Format("{0}|{1}", "skill1", "skill2"),
                SkillCondition = skillCondition
            };

            var effectEntity = GetEffectEntity(data);
            var selector = CreateSelector(data);

            //没有其他行为就执行下一步
            Execute();

            return;
        }
    }
}
