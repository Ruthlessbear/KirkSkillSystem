using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KSS.Runtime.Skill
{
    /// <summary>
    /// LK:使用效果池存储技能效果，减少同类技能复用导致的内存浪费
    /// LK:例如 朝左侧和右侧发射一个火球，期望把“左侧和右侧”参数化，而其余共通行为封装成几个技能效果
    /// </summary>
    public sealed class EffectAbilityPool
    {
        private Dictionary<string, Type> _effectLibrary { get; set; }
        /// <summary>
        /// 已激活的效果池
        /// </summary>
        private Dictionary<string, IEffectAbility> _activeAbilityPool;
        /// <summary>
        /// 暂时挂起的效果池
        /// </summary>
        private Dictionary<string, IEffectAbility> _waitAbilityPool;

        private int MAX_WAIT_ABILITY_NUM = 5;

        /// <summary>
        /// 轮询效果池状态
        /// 轮询时机在每一次效果池相关操作完毕后
        /// </summary>
        private void Update()
        {
            foreach (var ability in _waitAbilityPool.Values)
            {
                if (ability.WaitTimer >= MAX_WAIT_ABILITY_NUM)
                {
                    ability.ChangeStateType(false, false);
                }
                else
                {
                    ability.WaitTimer++;
                }
            }

            foreach (var ability in _activeAbilityPool.Values)
            {
                ability.WaitTimer = 0;
            }
        }

        /// <summary>
        /// 效果池初始化
        /// </summary>
        public void Start()
        {
            Dictionary<string, Type> collection = new Dictionary<string, Type>();
            Assembly curAssembly = Assembly.GetAssembly(typeof(EffectAbilityPool));
            Type[] types = curAssembly.GetExportedTypes();
            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<EffectAbilityAttribute>();
                if (attribute == null) continue;
                collection.Add(attribute.EffectId, type);
            }
            _effectLibrary = collection;
            _activeAbilityPool = new();
            _waitAbilityPool = new();
        }

        #region Operation Pool
        public List<IEffectAbility> GetAbilities(string effectIds)
        {
            //LK:这里做技能效果数据处理
            //举例暂定格式 xxx|xxx|xxx
            var splits = effectIds.Split('|');
            List<string> skillIds = new List<string>();
            foreach (var id in splits)
            {
                skillIds.Add(id);
            }

            List<IEffectAbility> abilitySets = new List<IEffectAbility>();
            foreach (var id in skillIds)
            {
                var ability = GetAbility(id);
                if(ability != null)
                    abilitySets.Add(ability);
            }

            Update();

            return abilitySets;
        }


        public IEffectAbility GetAbility(string effectID)
        {
            IEffectAbility ability;
            //优先查询激活池
            _activeAbilityPool.TryGetValue(effectID, out ability);
            if (ability != null)
            {
                if (ability.CheckStateType())
                {
                    ability.AddRelevance();
                    return ability;
                }
                else //实际不会出现
                    return null;
            }

            //次优先查询等待池
            _waitAbilityPool.TryGetValue(effectID, out ability);
            if (ability != null)
            {
                if(!ability.CheckStateType())
                {
                    ability.ChangeStateType(true, false);
                    ability.AddRelevance();
                    _activeAbilityPool.Add(effectID, ability);
                    _waitAbilityPool.Remove(effectID);
                    return ability;
                }
                else //实际不会出现
                    return null;
            }

            //不在上述池中时
            Type abilityType = FindAbilityType(effectID);
            if (abilityType == null) return null;
            ability = Activator.CreateInstance(abilityType) as IEffectAbility;
            {
                if (ability.CheckStateType()) return null; //实际不会出现
                ability.ChangeStateType(true, false);
                _activeAbilityPool.Add(effectID, ability);
                ability.AddRelevance();
                return ability;
            }
        }

        public Type FindAbilityType(string effectID)
        {
            if (_effectLibrary.ContainsKey(effectID))
            {
                return _effectLibrary[effectID];
            }
            else
            {
                UnityEngine.Debug.LogError(string.Format("No corresponding skill effect was found.Please check effect id : {0}", effectID));
                return null;
            }
        }
        #endregion
    }
}
