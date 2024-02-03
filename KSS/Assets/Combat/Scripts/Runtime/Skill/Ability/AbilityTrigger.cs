using KSS.Runtime.ECS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KSS.Runtime.Skill
{
    public abstract class AbilityTrigger
    {
        /// <summary>
        /// 检查触发对象
        /// </summary>
        public abstract bool CheckAbilityTrigger(CombatEntity target);
        /// <summary>
        /// 获取触发对象
        /// </summary>
        public abstract CombatEntity GetAbilityTriggerTarget();
        /// <summary>
        /// 在触发对象上生效效果
        /// </summary>
        public abstract bool ExecuteAbilityTrigger(object[] para, bool isDestroy = false);
        /// <summary>
        /// 在触发对象上生效效果
        /// </summary>
        /// <param name="target"></param>
        public abstract bool ExecuteAbilityTrigger(CombatEntity target, object[] para, bool isDestroy = false);
        /// <summary>
        /// 绑定效果实体
        /// </summary>
        /// <param name="entity"></param>
        public abstract void BindingEffectEntity(EffectEntity entity);
        /// <summary>
        /// 绑定选择器
        /// </summary>
        /// <param name="selector"></param>
        public abstract void BindingSelector(ICombatSelector selector);
    }

    public sealed class BaseAbilityTrigger : AbilityTrigger
    {
        private EffectEntity _effectEntity;
        private ICombatSelector _selector;

        public override void BindingEffectEntity(EffectEntity entity)
        {
            //LK:其他检索行为可自行添加
            if (entity != null)
                _effectEntity = entity;
        }

        public override void BindingSelector(ICombatSelector selector)
        {
            //LK:其他检索行为可自行添加
            if (selector != null)
                _selector = selector;
        }

        public override bool CheckAbilityTrigger(CombatEntity target)
        {
            if (_selector == null) return false;
            bool is_allow = _selector.CheckTarget(target);
            return is_allow;
        }

        public override bool ExecuteAbilityTrigger(object[] para, bool isDestroy = false)
        {
            var target = GetAbilityTriggerTarget();
            return ExecuteAbilityTrigger(target, para, isDestroy);
        }

        public override bool ExecuteAbilityTrigger(CombatEntity target, object[] para, bool isDestroy = false)
        {
            if (!CheckAbilityTrigger(target)) return false;
            if (_effectEntity == null) return false;
            try
            {
                _effectEntity.ExecuteEffect(para, isDestroy);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override CombatEntity GetAbilityTriggerTarget()
        {
            //TODO:关联碰撞器或者消息事件
            //TODO:通过实体或者hookID在ECS模块内去查找挂钩 可以获得关联CombatEntity
            return null;
        }
    }
}
