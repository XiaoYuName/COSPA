using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace ARPG.Config
{
    [CreateAssetMenu(fileName = "Tale",menuName = "ARPG/主线剧情")]
    public class TaleConfig : ScriptableObject
    {
        public List<TaleItemData> TaleItemDatas = new List<TaleItemData>();
    }

    
    [System.Serializable]
    public class TaleItemData
    {
        public int ID;

        [Tooltip("章节名称")]
        public string titleName;

        [Tooltip("章节标题")]
        public string ItemName;

        [Tooltip("简要描述"),ResizableTextArea]
        public string description;
        
        [Header("对话数据ID")]
        public string DialogueID;
    }

}

