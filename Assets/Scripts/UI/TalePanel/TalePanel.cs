using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.UI
{
    /// <summary>
    /// 主线剧情面板
    /// </summary>
    public class TalePanel : UIBase
    {
        private RectTransform content;
        public override void Init()
        {
            content = Get<RectTransform>("TalePanel/UIMask/TaleView/Viewport/Content");
        }

        private void CreatTaleItemUI()
        {
            
        }
    }
}

