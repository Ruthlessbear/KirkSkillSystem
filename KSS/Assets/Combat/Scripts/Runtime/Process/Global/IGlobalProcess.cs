using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KSS.Runtime.Process
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GlobalProcessAttribute : Attribute
    {
        public string ProcessID;

        public GlobalProcessAttribute(string processId)
        {
            this.ProcessID = processId;
        }
    }

    public interface IGlobalProcess
    {
        /// <summary>
        /// 子流程开始
        /// </summary>
        void Start();
        /// <summary>
        /// 子流程进行中
        /// </summary>
        void Proceed();
        /// <summary>
        /// 子流程结束
        /// </summary>
        void End();
        /// <summary>
        /// 子流程冻结点
        /// </summary>
        void FreezePoint();
    }
}
