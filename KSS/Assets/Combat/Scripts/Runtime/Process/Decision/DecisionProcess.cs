using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSS.Runtime.Skill;
using KSS.Runtime.Data;

namespace KSS.Runtime.Process
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DecisionProcessAttribute : Attribute
    {
        public string ProcessID;

        public DecisionProcessAttribute(string processId)
        {
            this.ProcessID = processId;
        }
    }

    public interface IDecisionProcess
    {
        /// <summary>
        /// 子流程开始
        /// </summary>
        void Start();
        /// <summary>
        /// 执行子流程
        /// </summary>
        void Execute();
        /// <summary>
        /// 子流程结束
        /// </summary>
        void End();
        /// <summary>
        /// 子流程冻结点
        /// </summary>
        void FreezePoint();

        /// <summary>
        /// 组合效果实体
        /// </summary>
        EffectEntity GetEffectEntity(SkillEffectData data);

        /// <summary>
        /// 创建效果组合选择器
        /// </summary>
        ICombatSelector CreateSelector(SkillEffectData data);

        /// <summary>
        /// 效果实体发射器
        /// </summary>
        void EntityLaunch(); 
    }

    public abstract class DecisionBaseProcess : IDecisionProcess
    {
        public abstract void Start();
        public abstract void Execute();
        public abstract void End();
        public abstract void FreezePoint();

        public EffectEntity GetEffectEntity(SkillEffectData data)
        {
            return SynthesizerManager.inst.CreateEffectEntity(data);
        }

        public ICombatSelector CreateSelector(SkillEffectData data)
        {
            return SynthesizerManager.inst.CreateSelector(data);
        }

        public void EntityLaunch()
        {
            throw new NotImplementedException();
        }
    }
}
