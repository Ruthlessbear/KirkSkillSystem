using System;
using System.Collections.Generic;
using System.Linq;

namespace KSS.Runtime.ECS
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SkillPropertyAttribute : Attribute
    {
        public string Name;

        public SkillPropertyAttribute(string name)
        {
            this.Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class CharacterPropertyAttribute : Attribute
    {
        public string Name;

        public CharacterPropertyAttribute(string name)
        {
            this.Name = name;
        }
    }

    public sealed partial class EntityAbilityLibrary{}
}
