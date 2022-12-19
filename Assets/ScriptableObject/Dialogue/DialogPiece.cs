using System.Collections.Generic;
using NaughtyAttributes;
using Spine.Unity;
using UnityEngine;

namespace ARPG.UI.Config
{
    /// <summary>
    /// 每单个对话数据
    /// </summary>
    [System.Serializable]
    public class DialogPiece
    {
        /// <summary>
        /// 对话唯一ID
        /// </summary>
        public int dialogID;
        /// <summary>
        /// 对话者的名字
        /// </summary>
        public string dialogName;
        /// <summary>
        /// 对话者图标
        /// </summary>
        public Sprite dialogSprite;
        /// <summary>
        /// 对话Spine动画
        /// </summary>
        public SkeletonDataAsset dialogueSpine;
        /// <summary>
        /// 对话者语音
        /// </summary>
        public string dialogClipName;
        /// <summary>
        /// 对话内容
        /// </summary>
        [ResizableTextArea]
        public string dialogText;
        /// <summary>
        /// 对话反馈的选项
        /// </summary>
        public List<DialogOption> Options = new List<DialogOption>();
        
    }
}

