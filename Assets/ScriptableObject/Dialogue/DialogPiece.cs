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
        [Header("没有任何意义,只是方便Scriptable 里观看")]
        public string DataInof;
        
        /// <summary>
        /// 对话唯一ID
        /// </summary>
        public int dialogID;
        /// <summary>
        /// 对话者的名字
        /// </summary>
        public string dialogName;
        [Header("是否在左边")]
        public bool isLeftSpine;
        /// <summary>
        /// 对话Spine动画
        /// </summary>
        public SkeletonDataAsset dialogueSpine;
        /// <summary>
        /// 对话者语音
        /// </summary>
        public string AudioID;

        /// <summary>
        /// 是否循环播放该动画
        /// </summary>
        public bool isLoop;

        [Header("如果希望该条对话跳转到此后,请勾选")]
        public bool isEnd;
        [Header("Spine动画播放名字")]
        public SpineDialogueAnimation SpineAnimationName;
        
        [Header("Spine Skin 皮肤名称")]
        public SpineDialogueSkin SpineSkinName;
        
        
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

