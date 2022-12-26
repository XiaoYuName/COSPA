using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Config
{
    /// <summary>
    /// 副本地图配置表
    /// </summary>
    [CreateAssetMenu(fileName = "地图配置",menuName = "ARPG/Game/MapData")]
    public class MapConfig : Config<MapItem>
    {
        
    }


    /// <summary>
    /// 副本配置
    /// </summary>
    [System.Serializable]
    public class MapItem : ConfigData
    {
        [Header("背景弹窗")]
        public Sprite mapIcon;
        
        [Header("奖励Item 列表")]
        public List<RewordItemBag> RewordItemList;

        [Header("奖励货币列表")]
        public RewordItemBag[] MoneyReword;

        public MapItem()
        {
            MoneyReword = new[]
            {
                new RewordItemBag
                {
                    itemBag = new ItemBag { ID = Settings.GemsthoneID, count = 0 },
                    Type = RewordType.Not,
                },
                new RewordItemBag
                {
                    itemBag = new ItemBag { ID = Settings.ManaID, count = 0 },
                    Type = RewordType.Not,
                },
                new RewordItemBag
                {
                    itemBag = new ItemBag { ID = Settings.ExpID, count = 0 },
                    Type = RewordType.Not,
                }
            };
        }
    }
}
