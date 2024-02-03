using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using System;
using UnityEngine;

namespace KSS.Editor.Process
{
    class GlobalProcessGraph
    {
        private string _assetsPath;
        private OdinMenuTree _tree;

        [InfoBox("Add/Remove combat flow here.")]
        [ListDrawerSettings(CustomAddFunction = "AddProcess", CustomRemoveElementFunction = "RemoveProcess")]
        public List<GlobalGraphElement> ProcessGraph = new List<GlobalGraphElement>();

        private readonly string _processAssetNamePrefix = "GloablProcess";
        private readonly int _processAssetDefaultOrder = 1000;
        private readonly string _processElementNamePrefix = "PLEASE_SET_NAME";
        private readonly string _processElementLabelPrefix = "PLEASE_SET_CN_LABLE";

        public GlobalProcessGraph(OdinMenuTree menuTree, string path)
        {
            this._tree = menuTree;
            this._assetsPath = path;

            InitializeGraph();
        }

        private void InitializeGraph()
        {
            var configs = Resources.FindObjectsOfTypeAll<GlobalProcess>();
            ProcessGraph.Clear();
            foreach (var data in configs)
            {
                var process = new GlobalGraphElement()
                {
                    Name = data.Name,
                    CN_Label = data.CN_Label,
                    Order = data.Order,
                    process = data
                };
                ProcessGraph.Add(process);
            }
            ProcessGraph.Sort((x, y) => { return x.Order - y.Order; });
        }

        private GlobalProcess CreateGlobalProcess(GlobalGraphElement element)
        {
            var configs = Resources.FindObjectsOfTypeAll<GlobalProcess>();
            var processObj = ScriptableObject.CreateInstance<GlobalProcess>();
            string objName = string.Format("{0}{1}", _processAssetNamePrefix, configs.Length + 1);

            {
                processObj.Name = element.Name;
                processObj.CN_Label = element.CN_Label;
                processObj.Order = element.Order;
            }
            
            AssetDatabase.CreateAsset(processObj, string.Format("Assets/{0}/{1}.asset", this._assetsPath, objName));
            EditorUtility.SetDirty(processObj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return processObj;
        }

        private GlobalGraphElement AddProcess()
        {
            var configs = Resources.FindObjectsOfTypeAll<GlobalProcess>();
            GlobalGraphElement element = new();
            element.Name = string.Format("{0}{1}", _processElementNamePrefix, configs.Length + 1);
            element.CN_Label = _processElementLabelPrefix;
            element.Order = _processAssetDefaultOrder;
            element.process = CreateGlobalProcess(element);
            UnityEngine.Debug.Log("Create new global process success!");
            return element;
        }

        private void RemoveProcess(GlobalGraphElement target)
        {
            var configs = Resources.FindObjectsOfTypeAll<GlobalProcess>();
            foreach (var data in configs)
            {
                if (data.Name.Equals(target.Name))
                {
                    AssetDatabase.DeleteAsset(string.Format("Assets/{0}/{1}.asset", this._assetsPath, data.name));
                    AssetDatabase.Refresh();
                    break;
                }
            }
            ProcessGraph.Remove(target);
            UnityEngine.Debug.Log("Delete global process success!");
        }

        [InfoBox("After modifying the preceding information, Save it.")]
        [Button(ButtonSizes.Gigantic)]
        private void Save()
        {
            var configs = Resources.FindObjectsOfTypeAll<GlobalProcess>();

            foreach (var data in configs)
            {
                EditorUtility.SetDirty(data);
            }
            AssetDatabase.SaveAssets();
        }
    }

    [Serializable]
    public class GlobalGraphElement
    {
        [OnValueChanged("ChangeValue")]
        public string Name;
        [OnValueChanged("ChangeValue")]
        public string CN_Label;
        [HideInInspector]
        public int Order;
        [HideInInspector]
        public GlobalProcess process;

        private void ChangeValue()
        {
            process.Name = this.Name;
            process.CN_Label = this.CN_Label;
        }
    }
}
