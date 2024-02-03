using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KSS.Runtime.Data;

namespace KSS.Runtime.Skill
{
    #region
    /// <summary>
    /// 根据队伍筛选目标 |多选
    /// </summary>
    [Flags]
    public enum TargetTeam
    { 
        TARGET_TEAM_BOTH = 1,        //双方队伍
        TARGET_TEAM_FRIENDLY = 2,  //友方队伍
        TARGET_TEAM_ENEMY = 4,      //敌方队伍
        TARGET_TEAM_NONE = 8,       //无
    }

    /// <summary>
    /// 指定目标类型 |多选
    /// </summary>
    /// LK:枚举为样例
    [Flags]
    public enum TargetType
    { 
        TARGET_TYPE_ALL = 1,
        TARGET_TYPE_BASIC = 2,
        TARGET_TYPE_GX = 4,
        TARGET_TYPE_V = 8,
        TARGET_TYPE_VMAX = 16,
    }

    /// <summary>
    /// 指定目标状态 |多选
    /// </summary>
    [Flags]
    public enum TargetFlag
    { 
        TARGET_FLAG_DEAD = 1,                 //死亡
        TARGET_FLAG_INVULNERABLE = 2,   //无敌
        TARGET_FLAG_BURN = 4,                 //燃烧
        TARGET_FLAG_POISONING = 8,        //中毒
        TARGET_FLAG_PARALYSIS = 16,         //麻痹
        TARGET_FLAG_CHAOS = 32,               //混乱
        TARGET_FLAG_NORMAL = 64,             //正常
    }

    #endregion

    public interface ICombatFilter
    {
        FilterEntity CombineFilter(SkillEffectData data);
    }

    public sealed class FilterContiner : ICombatFilter
    {
        public FilterEntity CombineFilter(SkillEffectData data)
        {
            FilterEntity filter = new FilterEntity();
            filter = filter.InjectFilterData(data);
            return filter;
        }
    }
}
