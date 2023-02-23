using System.Collections.Generic;
using System.Linq;
using ARPG.Config;
using UnityEngine;

namespace ARPG.UI.Config
{
    /// <summary>
    /// 商店配置表
    /// </summary>
    [CreateAssetMenu(fileName = "物品商城",menuName = "ARPG/Store/物品商城设定配置")]
    public class EquipStoenConfig : ScriptableObject
    {
        public List<EquipStoenLine> EquipStoenLines = new List<EquipStoenLine>();


        /// <summary>
        /// 获取对应Type类型的所有商品
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public List<EquipStoenData> GetTypeList(EquipTableType type)
        {
            if (EquipStoenLines.Any(a => a.Type == type))
            {
                return EquipStoenLines.Find(a => a.Type == type).Data;
            }

            Debug.Log("没有对应商品的类型商品请确认");
            return null;
        }
    }

    [System.Serializable]
    public class EquipStoenLine
    {
        public EquipTableType Type;

        public List<EquipStoenData> Data;
    }

    [System.Serializable]
    public class EquipStoenData
    {
        [Header("购买后获得商品")]
        public ItemBag RewordItem;
        
        [Header("需求商品")]
        public ItemBag SubItem;

        [Header("需求类型，如果是材料,则使用SubItem的icon来展示")]
        public StoenGoldIconType StoenGoldIconType;
    }

    public enum StoenGoldIconType
    {
        材料 =0,
        玛那 =1,
        宝石 =2,
    }
}

