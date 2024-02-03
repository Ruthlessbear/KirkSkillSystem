using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSS.Editor
{
    //TODO:路径优化 Runtime和Editor统一读动态配置 所以ScritableObject 得做一层转换
    public class CombatEditorPath
    {
        public static readonly string RootPath = "Combat";
        public static readonly string ProcessRootPath = "Assets";

        public static string GetOutputPath()
        {
            return "Combat/Output";
        }

        public static string GetAssetsPath()
        {
            return string.Format("{0}/{1}", RootPath, ProcessRootPath);
        }
    }
}
