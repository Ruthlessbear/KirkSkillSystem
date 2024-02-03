using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;

namespace KSS.Config
{
    public interface ICombatData
    {
        List<string> GetGlobalSchedulerConfig();
    }

    public class CombatDataSource : ICombatData
    {
        public List<string> GetGlobalSchedulerConfig()
        {
            string path = CombatAssetsPath.GetGlobalSchedulerConfigPath();
            var configs = Resources.Load(path);
            Type type = configs.GetType();
            FieldInfo[] infos = type.GetFields();
            List<string> scheduler = new List<string>();
            foreach (var field in infos)
            {
                if (field.Name.Equals("ProcessPipeline"))
                {
                    var pipeline = field.GetValue(configs) as List<string>;
                    foreach (var process in pipeline)
                    {
                        scheduler.Add(process);
                    }
                }
            }
            return scheduler;
        }
    }
}


