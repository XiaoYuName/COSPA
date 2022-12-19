using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.UI.Config
{
    /// <summary>
    /// 对话系统的对话数据
    /// </summary>
    [System.Serializable]
    public class DialogData :ConfigData
    {
        [Header("对话内容与数据")]
        public List<DialogPiece> Pieces = new List<DialogPiece>();

        public override string Get()
        {
            return ID;
        }
    }
}

