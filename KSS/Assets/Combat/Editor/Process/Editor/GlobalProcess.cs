using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace KSS.Editor.Process
{
    [Serializable]
    public class GlobalProcess : ScriptableObject
    {
        public string Name;
        public string CN_Label;

        [ReadOnly]
        public int Order = 1000;
    }
}
