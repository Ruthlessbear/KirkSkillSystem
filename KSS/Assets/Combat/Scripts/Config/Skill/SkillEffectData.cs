using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSS.Runtime.Data
{
    public enum TriggerMode
    {
        Passive,
        Single,
        Timer,
        Space,
        Mechanism
    }

    public enum ReleaseMode
    { 
        Single,

    }



    //TODO:缺导表转换流程
    public class SkillEffectData
    {
        /// <summary>
        /// 触发方式
        /// </summary>
        public TriggerMode TriggerType;
        /// <summary>
        /// 技能效果(EffectId)
        /// </summary>
        public string SkillEffect;
        /// <summary>
        /// 记录状态(True/False)
        /// </summary>
        public bool RecordState;
        /// <summary>
        /// 技能条件(由Filter解析)
        /// </summary>
        public string SkillCondition;
        /// <summary>
        /// 技能冷却
        /// 回合制战斗所以使用int代表回合数
        /// </summary>
        public int SkillCooling;
        /// <summary>
        /// 伤害公式
        /// 调用公式库,存储的是公式标识
        /// </summary>
        public string DamageEquation;
        /// <summary>
        /// 表现方式
        /// </summary>
        public string ShowMode;
        /// <summary>
        /// 判定时机
        //  TODO:独立成一个类 
        /// </summary>
        public string DecisionTime;
        /// <summary>
        /// 多时机触发策略
        /// </summary>
        public string TriggerStragegy;
        /// <summary>
        /// 附加触发点
        /// </summary>
        public string TriggerAddPoint;
        /// <summary>
        /// 附加触发效果
        /// </summary>
        public string TriggerAddEffect;

        public SkillEffectData()
        {

            //TODO:模拟的临时数据
            SimulationEffectData();
        }

        private void SimulationEffectData()
        {
            /*技能数据
             * 触发方式：被动触发 单体命中触发 时间触发（回合数） 空间位置触发 机制时机触发
             * 技能效果：通用效果类型库+可输入参数
             * 状态记录：是/否 （Hook上会有一个和该效果关联的状态记录触发器）
             * 释放条件：调取条件库
             * 释放方式(暂时忽略)：单体/多选/区域型 (与触发方式二异了，先只用一种)
             * 技能冷却：小回合数/大回合数
             * 伤害公式：与公式计算库关联
             * 表现方式：特效编号(总之单独开个Render给调吧)
             * 附加机制(暂时忽略)：额外可选项
             * 判定时机：可多时机触发
             * 多时机触发策略：拓展上一项
             * 附加触发点： 可能导致这个技能触发分支效果的判定
             * 附加触发效果： 上述对应效果
             */

            TriggerType = TriggerMode.Single;
            //TODO:填充数据测试
        }
    }
}
