using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KSS.Runtime.Data;

namespace KSS.Runtime.Skill
{
    public sealed class SynthesizerManager
    {
        #region Instance
        private static SynthesizerManager Instance;
        public static SynthesizerManager inst
        {
            get {
                if (Instance == null)
                {
                    Instance = new();
                }
                return Instance;
            }
        }
        #endregion

        private IEffectSynthesizer _skillSynthesizer;
        private ICombatFilter _combatFilter;
        private EffectAbilityPool _abilityPool;

        public SynthesizerManager()
        {
            Create();
            Start();
        }

        private void Create()
        {
            _skillSynthesizer = new SkillSynthesizer();
            _abilityPool = new EffectAbilityPool();
            _combatFilter = new FilterContiner();
        }

        public void Start()
        {
            _abilityPool.Start();
        }

        #region
        public EffectEntity CreateEffectEntity(SkillEffectData data)
        {
            if (string.IsNullOrEmpty(data.SkillEffect)) return null;
            List<IEffectAbility> effectAbilities = _abilityPool.GetAbilities(data.SkillEffect);
            var entity = _skillSynthesizer.Assembly(effectAbilities.ToArray());
            return entity;
        }

        public ICombatSelector CreateSelector(SkillEffectData data)
        {
            var filter = _combatFilter.CombineFilter(data);
            ICombatSelector selector = new CombatSelector(filter);
            return selector;
        }
        #endregion
    }
}
