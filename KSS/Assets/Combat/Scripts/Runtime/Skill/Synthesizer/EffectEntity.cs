using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KSS.Runtime.Skill
{
    public class EffectEntity
    {
        private List<Action<object[]>> _effectEvents = new List<Action<object[]>>();

        /// <summary>
        /// 新增效果回调
        /// </summary>
        /// <param name="effectEvent"></param>
        public void AddEffect(Action<object[]> effectEvent)
        {
            Action<object[]> evt = effectEvent;
            _effectEvents.Add(evt);
        }

        /// <summary>
        /// 移除效果回调
        /// </summary>
        /// <param name="effectEvent"></param>
        public void RemoveEffect(Action<object[]> effectEvent)
        {
            _effectEvents.Remove(effectEvent);
        }

        /// <summary>
        /// 执行效果，可传入参数封装，拆包在回调内发生
        /// </summary>
        /// <param name="para"></param>
        /// <param name="isDestroy"></param>
        public void ExecuteEffect(object[] para, bool isDestroy = false)
        {
            foreach (var action in _effectEvents)
            {
                action.Invoke(para);
            }
            if (isDestroy) _effectEvents.Clear();
        }
    }
}
