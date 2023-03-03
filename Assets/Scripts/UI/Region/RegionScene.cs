using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 主线章节UI
    /// </summary>
    public class RegionScene : UIBase
    {
        private Button CloseBtn;
        private RectTransform RegionContent;
        private Scrollbar TweenBar;
        private GameObject Mask;
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,UiAudioID.UI_click);
            RegionContent = Get<RectTransform>("UIMask/RegionView/Content");
            TweenBar = Get<Scrollbar>("UIMask/TweenScrollbar");
            Mask = Get("MainMask");
        }

        public IEnumerator InitData(RegionLine data)
        {
            yield return Instantiate(data.SceneGrid, RegionContent);
            RegionGridScene regionGridScene = GetComponentInChildren<RegionGridScene>();
            if (regionGridScene == null) yield break;
            regionGridScene.Init();
            regionGridScene.InitData(data);
            Open();
        }

        public void PlayTweenBar()
        {
            TweenBar.value = 0;
            Mask.gameObject.SetActive(true);
            DOTween.To(() => TweenBar.value, x => TweenBar.value = x, 1, 2f).OnComplete(delegate
            {
                TweenBar.value = 1;
                DOTween.To(() => TweenBar.value, x => TweenBar.value = x, 0, 2f).OnComplete(()=>Mask.gameObject.SetActive(false));
            });
        }

        public override void Close()
        {
            MainPanel.Instance.AddTbaleChild("RegionScene");
            base.Close();
        }
    }
}

