using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace KSS.Runtime.Skill
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EffectAbilityAttribute : Attribute
    {
        public string EffectId;

        public EffectAbilityAttribute(string id)
        {
            this.EffectId = id;
        }
    }

    public interface IEffectAbility
    {
        /// <summary>
        /// 挂起计数器
        /// </summary>
        public int WaitTimer{ get; set; }

        /// <summary>
        /// 获取技能效果
        /// </summary>
        /// <returns></returns>
        public Action<object[]> GetEffect();

        /// <summary>
        /// 增加关联性
        /// </summary>
        public void AddRelevance();

        /// <summary>
        /// 减少关联性
        /// </summary>
        public void ReduceRelevance();

        /// <summary>
        /// 切换效果状态
        /// </summary>
        public void ChangeStateType(bool isActivite, bool isWait = false);

        /// <summary>
        /// 检查效果状态正确性
        /// </summary>
        public bool CheckStateType();
    }

    //TODO:这里得移动一下 触发判定应该是生成效果实体 过滤器 选择器分配完后 再挂载的
    //TODO:EffectAbility就是纯粹的技能效果 其中用于回调
    //TODO:想想有没有好点的形式 
    public abstract class EffectAbility : IEffectAbility
    {
        private enum AbilityMode
        {
            Activited,
            Unactivited,
            Wait
        }
        private AbilityMode _abilityMode = AbilityMode.Unactivited;

        private int _relevantNum;

        public int WaitTimer { get; set; }

        protected abstract void SkillEffect(object[] para);

        public Action<object[]> GetEffect()
        {
            Action<object[]> action = SkillEffect;
            return action;
        }

        public void AddRelevance()
        {
            bool is_activite = CheckStateType();
            if (is_activite)
                _relevantNum++;
            else
            { 
                ChangeStateType(true);
                _relevantNum = 0;
                _relevantNum++;
            }
        }

        public void ReduceRelevance()
        {
            bool is_activite = CheckStateType();
            if (is_activite)
            {
                _relevantNum--;
                if (_relevantNum <= 0)
                    ChangeStateType(false, true);
            }
            else
            { 
                //不在激活状态下跳过
            }
        }

        public void ChangeStateType(bool isActivite, bool isWait = false)
        {
            if (isActivite)
                _abilityMode = AbilityMode.Activited;
            else if (isWait)
            {
                _abilityMode = AbilityMode.Wait;
                _relevantNum = 0;
            }
            else if (!isActivite && !isWait)
            {
                _abilityMode = AbilityMode.Unactivited;
                _relevantNum = 0;
            }
        }

        public bool CheckStateType()
        {
            switch (_abilityMode)
            {
                case AbilityMode.Activited:
                    return true;
                case AbilityMode.Wait:
                case AbilityMode.Unactivited:
                    return false;
            }
            return false;
        }
    }
}
