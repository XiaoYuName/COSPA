using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace ARPG.UI.Config
{
    [CreateAssetMenu(fileName = "HelpTextConfig",menuName = "ARPG/HelpConifg")]
    public class HelpConfig : ScriptableObject
    {
        public List<HelpTextItem> HelpTextItems = new List<HelpTextItem>();

        public  HelpTextItem Get(HelpType type)
        {
            return HelpTextItems.FindLast(t => t.type == type);
        }
    }

    
    [System.Serializable]
    public class HelpTextItem
    {
        public HelpType type;
        public string title;
        [ResizableTextArea]
        public string description;
    }

    public enum HelpType
    {
        记忆选择,
        装备强化,
        觉醒,
        无尽回廊,
    }
}
