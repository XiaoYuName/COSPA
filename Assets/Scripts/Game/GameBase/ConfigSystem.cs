using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 配置相关管理器：
    ///     目的解放GameSystem 与 UISystem 的配置相关逻辑整合
    /// </summary>
    public class ConfigSystem : MonoSingleton<ConfigSystem>
    {
        private RegionConfig RegionConfig;

        private BuffConfig buffConfig;

        protected override void Awake()
        {
            base.Awake();
            LoadConfig();
        }

        private void LoadConfig()
        {
            RegionConfig = ConfigManager.LoadConfig<RegionConfig>("Region/Region");
            buffConfig = ConfigManager.LoadConfig<BuffConfig>("BUFF/Buff");
        }



        public RegionItem GetSingRegion(string Name)
        {
            return RegionConfig.GetRegionSingleton(Name);
        }

        public BuffData GetBUFFData(string name)
        {
            return buffConfig.BuffDatas.Find(b => b.BuffName == name);
        }
    }
}

