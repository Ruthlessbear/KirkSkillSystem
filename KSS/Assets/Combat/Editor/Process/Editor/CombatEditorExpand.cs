using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using System;
using UnityEngine;
using System.Linq;

namespace KSS.Editor.Process
{
    /// <summary>
    /// 用于菜单item回调
    /// </summary>
    interface IOdinMenuItemRefresher
    {
        void Refresh();
    }
}
