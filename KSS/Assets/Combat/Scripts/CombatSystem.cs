using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KSS.Config;
using KSS.Runtime.Process;
using KSS.Runtime.Skill;

namespace KSS.Runtime.Main
{
    //TODO:Mono是测试用的 实际删掉
    public sealed class CombatSystem : MonoBehaviour
    {
        #region Instance
        private static CombatSystem Instance;
        public static CombatSystem inst
        {
            get
            {
                if (Instance == null)
                    Instance = new();
                return Instance;
            }
        }
        #endregion

        private CombatDataSource _dataSource;
        private IGlobalScheduler _globalScheduler;

        private void Start()
        {
            Initialize();
            Execute();
        }

        /// <summary>
        /// 战斗系统初始化
        /// </summary>
        public void Initialize()
        {
            InitDataSource();
            InitGlobalScheduler();
        }

        public void Execute()
        {
            _globalScheduler.Next();
        }

        #region 配置管理
        private void InitDataSource()
        {
            _dataSource = new CombatDataSource();
        }

        #endregion

        #region 战斗流程
        /// <summary>
        /// 初始化战斗流程器
        /// </summary>
        private void InitGlobalScheduler()
        {
            //TODO:数据的合成做一层封装
            GlobalRuntimeData data = new GlobalRuntimeData();
            {
                data.schedulerList = _dataSource.GetGlobalSchedulerConfig();
            }

            _globalScheduler = SchedulerCreator.CreateGlobalScheduler(data);
        }
        #endregion
    }
}

