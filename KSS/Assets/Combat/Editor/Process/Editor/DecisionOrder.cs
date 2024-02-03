using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using System;

namespace KSS.Editor.Process
{
    [HideLabel]
    [Serializable]
    public class DecisionOrder
    {
        [MultiLineProperty(3), Title("Basic Odin Menu Editor Window", "Inherit from OdinMenuEditorWindow, and build your menu tree")]
        public string Test1 = "This value is persistent cross reloads, but will reset once you restart Unity or close the window.";
    }
}
