using System;
using System.Collections.Generic;
using System.Linq;

namespace KSS.Runtime.Skill
{
    [EffectAbility("skill1")]
    public class ExampleAbility : EffectAbility
    {
        protected override void SkillEffect(object[] para)
        {
            UnityEngine.Debug.Log("Activate skill effect 1");
        }
    }

    [EffectAbility("skill2")]
    public class Example2Ability : EffectAbility
    {
        protected override void SkillEffect(object[] para)
        {
            UnityEngine.Debug.Log("Activate skill effect 2");
        }
    }
}
