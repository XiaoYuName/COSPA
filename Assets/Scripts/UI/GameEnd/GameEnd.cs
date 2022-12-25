using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI.Config;
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

        public void ShowEndGame(MapItem RewordItem)
        {
            TitleText.transform.DOScale(new Vector3(1.25f, 1.25f, 1), 1.25f).SetEase(Ease.OutElastic)
                .OnComplete(delegate
                {
                    TitleText.transform.DOMove(new Vector3(TitleText.transform.position.x,TitleText.transform.position.y+300,transform.position.z), 1.25f).OnComplete(
                        ()=>StartCoroutine(SettlementGameVictory(RewordItem)));
                });
        }

        /// <summary>
        /// 结算奖励函数
        /// </summary>
        /// <returns></returns>
        public IEnumerator SettlementGameVictory(MapItem Reword)
        {
            //1.获取Player坐标
            Vector3 playerPoint = GameManager.Instance.Player.transform.position;
            //2.Player 坐标转换为UI坐标系
            Vector3 UIPoint = Camera.main.WorldToScreenPoint(playerPoint);
            //3.在UI坐标系下生成LevelPanel信息
            LevelPanelUI panelUI =  UISystem.Instance.InstanceUI<LevelPanelUI>("LevelPanelUI",gameObject.transform);
            panelUI.transform.position = UIPoint;
            yield return panelUI.OpentionLevelAndFavorability(Reword);
            //4.进入下一个界面,进行奖励界面的处理
            
        }
    }

}
