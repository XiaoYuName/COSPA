using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace ARPG.UI.Config
{
    [CreateAssetMenu(fileName = "RootTable",menuName = "ARPG/RootUI/RootTable")]
    public class RootTableConfig : ScriptableObject
    {
        public List<RootTableItem> tableItems = new List<RootTableItem>();

        public RootTableItem Get(TableType tupe)
        {
            return tableItems.Find(t => t.Type == tupe);
        }
    }

    [System.Serializable]
    public class RootTableItem
    {
        [Header("按钮类型")]
        public TableType Type;
       
        [Header("是否开启")]
        public TableMode Mode;
        
        [Header("打开UI界面的名字")]
        public string OpenUIName;

        [Header("icon"),ShowAssetPreview]
        public Sprite icon;

        [Header("选中icon"),ShowAssetPreview]
        public Sprite selecticon;
    }

    public enum TableMode
    {
        Open,
        Close,
    }


    /// <summary>
    /// 切换的界面类型列表
    /// </summary>
    public enum TableType
    {
        我的主页,
        角色,
        剧情,
        冒险,
        工会之家,
        扭蛋,
        主菜单,
    }
}

