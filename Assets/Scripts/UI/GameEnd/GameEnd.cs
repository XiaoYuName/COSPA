using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class GameEnd : UIBase
    {
        private Image TitleText;

        public override void Init()
        {
            TitleText = Get<Image>("UIMask/TitleText");
            TitleText.transform.localScale = Vector3.zero;
        }

        public void ShowEndGame()
        {
            TitleText.transform.DOScale(new Vector3(1.25f, 1.25f, 1), 1.25f).SetEase(Ease.OutElastic)
                .OnComplete(delegate
                {
                    TitleText.transform.DOLocalMoveY(TitleText.transform.position.y-8f, 1.25f);
                });
        }
    }

}
