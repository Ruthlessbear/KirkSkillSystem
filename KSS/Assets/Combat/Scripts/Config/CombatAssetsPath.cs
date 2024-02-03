using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KSS.Config
{
    //TODO:路径优化下 因为Editor路径走的配置 Runtime不去涉及Editor的东西 中间转一层json？
    public class CombatAssetsPath
    {
        private static readonly string _unifyOutputFile = "Output";

        private static readonly string _globalSchedulerAssetName = "GlobalProcessConfiguration";

        public static string GetGlobalSchedulerConfigPath()
        {
            string path = string.Format("Combat/{0}/{1}", _unifyOutputFile, _globalSchedulerAssetName);
            return path;
        }
    }
}
