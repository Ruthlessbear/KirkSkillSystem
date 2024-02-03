using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSS.Runtime.ECS
{
    public sealed class HookController
    {
        #region Instance
        private static HookController _instance;
        public static HookController Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new();
                }
                return _instance;
            }
        }
        #endregion


    }
}
