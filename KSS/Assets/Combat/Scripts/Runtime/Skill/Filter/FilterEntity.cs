using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KSS.Runtime.Skill
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterParaAttribute : Attribute
    {
        public string Name;
        public Type Type;

        public FilterParaAttribute(string name, Type type)
        {
            this.Name = name;
            this.Type = type;
        }
    }

    public class FilterEntity
    {
        /// <summary>
        /// 指定对应目标队伍
        /// </summary>
        [FilterPara("TargetTeam", typeof(TargetTeam))]
        public TargetTeam TargetTeam { get; private set; }
        /// <summary>
        /// 指定对应目标类型
        /// </summary>
        [FilterPara("TargetType", typeof(TargetType))]
        public TargetType TargetType { get; private set; }
        /// <summary>
        /// 排除对应目标类型
        /// </summary>
        [FilterPara("ExcludeTargetType", typeof(TargetType))]
        public TargetType ExcludeTargetType { get; private set; }
        /// <summary>
        /// 指定自身类型
        /// </summary>
        [FilterPara("SelfType", typeof(TargetType))]
        public TargetType SelfType { get; private set; }
        /// <summary>
        /// 排除自身类型
        /// </summary>
        [FilterPara("ExcludeSelfType", typeof(TargetType))]
        public TargetType ExcludeSelfType { get; private set; }
        /// <summary>
        /// 指定对应目标状态
        /// </summary>
        [FilterPara("TargetFlag", typeof(TargetFlag))]
        public TargetFlag TargetFlag { get; private set; }
        /// <summary>
        /// 排除对应目标类型
        /// </summary>
        [FilterPara("ExcludeTargetFlag", typeof(TargetFlag))]
        public TargetFlag ExcludeTargetFlag { get; private set; }
        /// <summary>
        /// 指定自身状态
        /// </summary>
        [FilterPara("SelfFlag", typeof(TargetFlag))]
        public TargetFlag SelfFlag { get; private set; }
        /// <summary>
        /// 排除自身状态
        /// </summary>
        [FilterPara("ExcludeSelfFlag", typeof(TargetFlag))]
        public TargetFlag ExcludeSelfFlag { get; private set; }
    }
}
