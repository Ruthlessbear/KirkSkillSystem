using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;
using System.Reflection;
using UnityEditor;
using System.IO;

namespace KSS.Editor.Process
{
    public class GlobalOrder : IOdinMenuItemRefresher
    {
        private string _assetsPath;

        [InfoBox("The combat process is sorted here.")]
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        [OnCollectionChanged("ChangeAfter")]
        public List<GlobalPerk> GraphOrder = new List<GlobalPerk>();
        private readonly string _outputConfigurationName = "GlobalProcessConfiguration";

        public GlobalOrder(string path)
        {
            this._assetsPath = path;

            Initialize();
        }

        private void Initialize()
        {
            var configs = Resources.FindObjectsOfTypeAll<GlobalProcess>();
            GraphOrder.Clear();
            foreach (GlobalProcess obj in configs)
            {
                GlobalPerk perk = new GlobalPerk()
                {
                    ProcessName = obj.Name,
                    Order = obj.Order,
                    CN_Label = obj.CN_Label,
                    Process = obj
                };
                GraphOrder.Add(perk);
            }
            GraphOrder.Sort((x, y) => { return x.Order - y.Order; });

            Sort();
        }

        private void Sort()
        {
            //重新赋值排序编号
            int new_order = 0;
            foreach (var perk in GraphOrder)
            {
                perk.Process.Order = ++new_order;
                perk.Order = perk.Process.Order;
            }
        }

        public void ChangeAfter(CollectionChangeInfo info, object value)
        {
            Sort();
        }

        public void Refresh()
        {
            Initialize();
        }

        #region Button
        [InfoBox("Generate the battle flow configuration.")]
        [Button(ButtonSizes.Gigantic)]
        private void BuildConfiguration()
        {
            List<string> result = new List<string>();
            foreach (var data in GraphOrder)
            {
                result.Add(data.ProcessName);
            }
            var obj = ScriptableObject.CreateInstance<GlobalProcessOutput>();
            obj.ProcessPipeline = result;
            try
            {
                string path = string.Format("Assets/Resources/{0}/{1}.asset", CombatEditorPath.GetOutputPath(), _outputConfigurationName);
                if (File.Exists(path))
                {
                    try
                    {
                        AssetDatabase.DeleteAsset(path);
                    }
                    catch
                    {
                        UnityEngine.Debug.Log(string.Format("{0} have file, but delete failed."));
                    }
                }
                AssetDatabase.CreateAsset(obj, path);
                EditorUtility.SetDirty(obj);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                UnityEngine.Debug.Log("Create process config in " + _assetsPath + " success!"
                + " file name: " + _outputConfigurationName);
            }
            catch
            {
                ScriptableObject.Destroy(obj);
                UnityEngine.Debug.Log("Create process confif faild, please check assets path.");
            }

            
        }
        #endregion
    }

    [Serializable]
    public class GlobalPerk
    {
        [ReadOnly]
        public string ProcessName;

        [ReadOnly]
        public string CN_Label;

        [ReadOnly]
        public int Order;

        [HideInInspector]
        public GlobalProcess Process;
    }
}
