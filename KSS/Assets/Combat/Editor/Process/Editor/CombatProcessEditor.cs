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
    public class CombatProcessEditor : OdinMenuEditorWindow
    {
        private GlobalOrder _globalOrder;
        private DecisionOrder _decisionOrder = new();

        private GlobalProcessGraph _globalGraph;

        private string _globalAssetsPath = "GlobalProcess";
        private string _decisionAssetsPath = "DecisionProcess";

        [MenuItem("Tools/Combat/ProcessEditor")]
        private static void OpenWindow()
        {
            var window = GetWindow<CombatProcessEditor>();

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: true);

            //string process_path = GetProcessPath();
            //tree.AddAssetAtPath("Setting", string.Format("{0}/CombatSetting_Default.asset", process_path), typeof(ScriptableObject));

            {
                if (_globalOrder == null)
                    _globalOrder = new(GetGlobalAsstesPath());
                tree.Add("Global/Order", this._globalOrder);
            }

            tree.Add("Decision/Order", this._decisionOrder);

            {
                if (_globalGraph == null)
                    _globalGraph = new(tree, GetGlobalAsstesPath());
                tree.Add("Global/ProcessGraph", this._globalGraph);
            }

            tree.Selection.SelectionChanged += (SelectionChangedType type) =>
            {
                if (type.Equals(SelectionChangedType.ItemAdded))
                {
                    foreach (var item in tree.Selection)
                    {
                        if (item.Value is IOdinMenuItemRefresher)
                        {
                            (item.Value as IOdinMenuItemRefresher).Refresh();
                        }
                    }
                }
            };

            return tree;
        }

        #region Global

        #endregion

        #region Decision

        #endregion


        #region Path Relevant
        private string GetProcessPath()
        {
            return CombatEditorPath.GetAssetsPath();
        }

        private string GetGlobalAsstesPath()
        {
            return string.Format("{0}/{1}", GetProcessPath(), _globalAssetsPath);
        }

        private string GetDecisionAssetsPath()
        {
            return string.Format("{0}/{1}", GetProcessPath(), _decisionAssetsPath);
        }
        #endregion
    }
}




