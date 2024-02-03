using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KSS.Runtime.Data;

namespace KSS.Runtime.Skill
{
    //LK：如果将条件分表，则需要修改该接口
    public static class FilterInjecter
    {

        //TODO:这里是暂时的解析格式，理想是将条件转成json格式，通过ID关联
        // 格式 Name:XXXX | XXXX; Name:XXXX | XXXX
        public static FilterEntity InjectFilterData(this FilterEntity entity, SkillEffectData effectData)
        {
            //Temp check
            if (string.IsNullOrEmpty(effectData.SkillCondition)) return entity;

            string skillCondition = effectData.SkillCondition;
            //Temp analysis
            string[] temp = skillCondition.Split(';');
            Dictionary<string, string[]> skillAnalysis = new Dictionary<string, string[]>();
            foreach (var data in temp)
            {
                var conditon = data.Split(':');
                skillAnalysis.Add(conditon[0], conditon[1].Split("|"));
            }
            Dictionary<string, string[]> entryAnalysis = new Dictionary<string, string[]>();
            //Reflection inject
            Type type = entity.GetType();
            var propertyInfo = type.GetProperties();
            List<FilterParaAttribute> attributes = new List<FilterParaAttribute>();
            foreach (var info in propertyInfo)
            {
                var attribute = info.GetCustomAttributes<FilterParaAttribute>();
                if (attribute != null) attributes.Add(attribute.First());
            }
            
            foreach (var kv in skillAnalysis)
            {
                string name = kv.Key;
                var attribute = attributes.Find(x => x.Name.Equals(name));
                if (attribute != null)
                {
                    InjectFilterData(entity, attribute, kv.Value);
                }
                else
                {
                    //LK:日志提示，暴露表数据错误
                }
            }
            return entity;
        }

        private static void InjectFilterData(FilterEntity entity, FilterParaAttribute attribute, string[] data)
        {
            if (attribute.Type.IsAssignableFrom(typeof(TargetTeam)))
            {
                InjectTargetTeam(entity, attribute.Name, data);
            }
            else if (attribute.Type.IsAssignableFrom(typeof(TargetType)))
            {
                InjectTargetType(entity, attribute.Name, data);
            }
            else if (attribute.Type.IsAssignableFrom(typeof(TargetFlag)))
            {
                InjectTargetFlag(entity, attribute.Name, data);
            }
        }

        private static void InjectTargetTeam(FilterEntity entity, string name, string[] data)
        {
            if (data.Length <= 0) return;
            TargetTeam result = (TargetTeam)Enum.Parse(typeof(TargetTeam), data.First());
            for (int i = 1; i <= data.Length - 1; i++)
            {
                string value = data[i];
                TargetTeam temp = (TargetTeam)Enum.Parse(typeof(TargetTeam), value);
                result |= temp;
            }

            Type type = entity.GetType();
            PropertyInfo info = type.GetProperty(name);
            info.SetValue(entity, result);
        }

        private static void InjectTargetType(FilterEntity entity, string name, string[] data)
        {
            if (data.Length <= 0) return;
            TargetType result = (TargetType)Enum.Parse(typeof(TargetType), data.First());
            for (int i = 1; i <= data.Length - 1; i++)
            {
                string value = data[i];
                TargetType temp = (TargetType)Enum.Parse(typeof(TargetType), value);
                result |= temp;
            }

            Type type = entity.GetType();
            PropertyInfo info = type.GetProperty(name);
            info.SetValue(entity, result);
        }

        private static void InjectTargetFlag(FilterEntity entity, string name, string[] data)
        {
            if (data.Length <= 0) return;
            TargetFlag result = (TargetFlag)Enum.Parse(typeof(TargetFlag), data.First());
            for (int i = 1; i <= data.Length - 1; i++)
            {
                string value = data[i];
                TargetFlag temp = (TargetFlag)Enum.Parse(typeof(TargetFlag), value);
                result |= temp;
            }

            Type type = entity.GetType();
            PropertyInfo info = type.GetProperty(name);
            info.SetValue(entity, result);
        }
    }
}
