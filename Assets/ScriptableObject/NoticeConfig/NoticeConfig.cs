using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace ARPG.UI.Config
{
    [CreateAssetMenu(fileName = "NoticeConfig",menuName = "ARPG/公告配置")]
    public class NoticeConfig : ScriptableObject
    {
        public List<NoticeData> _NoticeDatas = new List<NoticeData>();

        public List<NoticeData> GetTypeData(NoticeType _type)
        {
            return _NoticeDatas.FindAll(t => t._Type == _type);
        }
    }

    [System.Serializable]
    public class NoticeData
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Header("类型")]
        public NoticeType _Type;
        /// <summary>
        /// PropName 抬头
        /// </summary>
        [Header("抬头")]
        public NoticeMode _Mode;
        /// <summary>
        /// icon
        /// </summary>
        [Header("icon")]
        public Sprite icon;

        /// <summary>
        /// 标题
        /// </summary>
        [Header("标题")]
        public string _TitleName;

        /// <summary>
        /// 时间
        /// </summary>
        [ResizableTextArea,Header("时间")]
        public string _Time;

        [ResizableTextArea,Header("内容")] 
        public string description;
    }
}

