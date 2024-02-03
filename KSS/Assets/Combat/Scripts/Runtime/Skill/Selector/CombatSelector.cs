using System;
using System.Collections.Generic;
using System.Linq;
using KSS.Runtime.ECS;

namespace KSS.Runtime.Skill
{
    public interface ICombatSelector
    {
        /// <summary>
        /// 渲染接口
        /// </summary>
        void Render();
        /// <summary>
        /// 将对象添加入选择列表
        /// </summary>
        /// <param name="entity"></param>
        void EnterTarget(CombatEntity entity);
        /// <summary>
        /// 将对象移出选择列表
        /// </summary>
        /// <param name="entity"></param>
        void OutTarget(CombatEntity entity);
        /// <summary>
        /// 对外接口
        /// </summary>
        void Result();
        /// <summary>
        /// 对象条件检测
        /// </summary>
        /// <param name="entity"></param>
        bool CheckTarget(CombatEntity entity);
    }

    public sealed class CombatSelector : ICombatSelector
    {
        private delegate void TargetReadyDelegate(int entityId);
        private delegate void TargetCancelDelegate();
        private TargetReadyDelegate TargetReady;
        private TargetCancelDelegate TargetCancel;
        private FilterEntity _filter;

        private Dictionary<int, CombatEntity> _targetSets = new Dictionary<int, CombatEntity>();

        public CombatSelector(FilterEntity filter)
        {
            this._filter = filter;
        }

        public void EnterTarget(CombatEntity entity)
        {
            if (_targetSets.ContainsKey(entity.HookID)) return;
            if (!CheckTarget(entity)) return;
            _targetSets.Add(entity.HookID, entity);
        }

        public void OutTarget(CombatEntity entity)
        {
            if (_targetSets.ContainsKey(entity.HookID))
                _targetSets.Remove(entity.HookID);

            //TODO:wait...
        }

        public void Render()
        {
            //TODO:渲染接口
        }

        public void Result()
        {
            //TODO:对外接口
        }

        public bool CheckTarget(CombatEntity entity)
        {
            
            return true;
        }
    }
}
