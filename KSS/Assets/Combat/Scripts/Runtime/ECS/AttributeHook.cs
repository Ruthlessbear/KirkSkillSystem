using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSS.Runtime.ECS
{
    public sealed class AttributeHook
    {
        private int _hookId;
        public int HookId
        {
            get {
                //TODO:这里要写一个hook的ID分配策略
                return _hookId;
            }
        }

        //TODO:特性Tag怎么存储？ 按类别分存字典？

        public void AddAddribute()
        { 
        
        }

        public void RemoveAttribute(string tag)
        {

        }
    }
}
