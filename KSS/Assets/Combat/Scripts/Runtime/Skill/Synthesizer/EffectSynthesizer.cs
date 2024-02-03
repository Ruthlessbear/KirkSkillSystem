using System;
using System.Collections.Generic;
using System.Linq;
using KSS.Runtime.Data;

namespace KSS.Runtime.Skill
{
    public interface IEffectSynthesizer
    {
        /// <summary>
        /// 组装实体
        /// </summary>
        EffectEntity Assembly(IEffectAbility[] effects);
    }

    public abstract class EffectSynthesizer : IEffectSynthesizer
    {
        public EffectEntity Assembly(IEffectAbility[] effects)
        {
            EffectEntity entity = new EffectEntity();
            foreach (var effect in effects)
            {
                entity.AddEffect(effect.GetEffect());
            }

            return entity;
        }
    }
}
