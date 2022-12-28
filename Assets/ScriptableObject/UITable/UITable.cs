using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace ARPG.Config
{
    /// <summary>
    /// UITable 配置类:
    ///     该配置表主要由UISystem 调用,
    /// </summary>
    [CreateAssetMenu(fileName = "UITable",menuName = "ARPG/UI/UITable")]
    public class UITable : Config<UITableItem>
    {
        [Header("UI根节点列表")]
        public List<string> UIParents = new List<string>();

        /// <summary>
        /// 获取所有一开始就加载的界面
        /// </summary>
        /// <returns></returns>
        public List<UITableItem> GetLoadInitPrefab()
        {
            return BaseDatas.FindAll(e => e.isInit);
        }
    }
    
    /// <summary>
    /// TableItem 配置表
    /// </summary>
    [System.Serializable]
    public class UITableItem : ConfigData
    {
        [Header("预制体名称")]
        public GameObject Prefab;

        [Header("描述")]
        public string details;

        [Header("UI父级名称"),Layer]
        public string parentName;

        [Header("适配UI层级")]
        public UITableType Type;
        
        [Header("是否一开始就加载")]
        public bool isInit;
    }
}

