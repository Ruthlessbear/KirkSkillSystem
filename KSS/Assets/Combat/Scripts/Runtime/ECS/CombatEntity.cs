using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KSS.Runtime.ECS
{
    /// <summary>
    /// 战斗实体挂载容器
    /// </summary>
    public class CombatEntity : MonoBehaviour
    {
        private int _hookId;
        public int HookID
        {
            get {
                return _hookId;
            }
        }

        private void Awake()
        {
            _hookId = CreateHook();
        }

        /// <summary>
        ///  创建属性挂钩
        /// </summary>
        private int CreateHook()
        {
            //TODO:...
            return default(int);
        }
    }
}
