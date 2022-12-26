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
        private Button NextBtn;

        public override void Init()
        {
            TitleText = Get<Image>("UIMask/TitleText");
            TitleText.transform.localScale = Vector3.zero;
            NextBtn = Get<Button>("UIMask/NextBtn");
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
            panelUI.transform.position = new Vector3(UIPoint.x,UIPoint.y - 30,UIPoint.z);
            yield return panelUI.OpentionLevelAndFavorability(Reword);
            //4.等待玩家点击下一部,进行奖励界面的处理
            var netPoint = NextBtn.transform.position;
            NextBtn.transform.DOMove(new Vector3(netPoint.x, netPoint.y + 250, netPoint.z), 1.25f).SetEase(Ease.OutElastic)
                .OnComplete(() =>
                {
                    Bind(NextBtn, delegate
                    {
                        StartCoroutine(ShowRewowrdItem(Reword,panelUI));
                    }, "UI_click");
                });
        }

        public IEnumerator ShowRewowrdItem(MapItem reword,LevelPanelUI levelPanelUI)
        {
            var netPoint = levelPanelUI.transform.position;
            levelPanelUI.transform.DOMove(new Vector3(netPoint.x, netPoint.y - 450, netPoint.z), 1.25f)
                .SetEase(Ease.OutElastic).OnComplete(()=> levelPanelUI.gameObject.SetActive(false));
            //2.Player移出屏幕
            Character Player = GameManager.Instance.Player;
            //2.1将视口坐标转化为世界坐标
            var word = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0.25f, 0));
            Vector3 target = new Vector3(word.x + 30, word.y, word.z);
            //2.2 解除2DCamera 的跟随
            GameManager.Instance.RelieveFollowPlayer();
            //2.3 开始移动
            while (Vector2.Distance(Player.transform.position,target) > 0.15f)
            {
                Player.transform.position= Vector2.MoveTowards(Player.transform.position, target, 3.5f * Time.deltaTime);
                Player.anim.SetBool("isMovenemt",true);
                yield return null;
            }
            Player.anim.SetBool("isMovenemt",false);
            
            
            //逐步生成奖励界面
            
            
            yield return null;
        }
    }

}
