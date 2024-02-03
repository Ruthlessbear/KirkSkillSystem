using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace KSS.Editor.Process
{
    [Serializable]
    public class GlobalProcessOutput : ScriptableObject
    {
        [ReadOnly]
        public List<string> ProcessPipeline = new List<string>();
    }
}