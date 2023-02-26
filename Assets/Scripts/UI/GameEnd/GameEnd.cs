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
        private RewordUI _RewordUI;
        private LevelPanelUI PanelUI;

        public Sprite VietorySpien;
        public Sprite OverSpine;
        
        //1.记录初始坐标
        private Vector3 NextBtnStarPoint;
        private Vector3 TitleStarPoint;
        public override void Init()
        {
            TitleText = Get<Image>("UIMask/TitleText");
            var transform1 = TitleText.transform;
            transform1.localScale = Vector3.zero;
            TitleStarPoint = transform1.position;

            NextBtn = Get<Button>("UIMask/NextBtn");
            NextBtnStarPoint = NextBtn.transform.position;
            
            _RewordUI = Get<RewordUI>("UIMask/RewordUI");
            _RewordUI.Init();
        }

        public void ShowEndGame(MapItem RewordItem)
        {
            TitleText.sprite = VietorySpien;
            TitleText.transform.DOScale(new Vector3(1.25f, 1.25f, 1), 1.25f).SetEase(Ease.OutElastic)
                .OnComplete(delegate
                {
                    TitleText.transform.DOLocalMove(new Vector3(TitleText.transform.localPosition.x,TitleText.transform.localPosition.y+300,transform.position.z), 1.25f).OnComplete(
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
            // //2.Player 坐标转换为UI坐标系
            Vector3 UIPoint = Camera.main.WorldToScreenPoint(playerPoint);
            //UI 坐标系转换为RectTransform 坐标系
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)gameObject.transform, UIPoint,
                UICamear.Instance.GetUICamera, out var rectPoint);
            //3.在UI坐标系下生成LevelPanel信息
            LevelPanelUI panelUI =  UISystem.Instance.InstanceUI<LevelPanelUI>("LevelPanelUI",gameObject.transform);
            PanelUI = panelUI;
            panelUI.transform.localPosition = rectPoint;
            yield return panelUI.OpentionLevelAndFavorability(Reword);
            //4.等待玩家点击下一部,进行奖励界面的处理
            var netPoint = NextBtn.transform.localPosition;
            NextBtn.transform.DOLocalMove(new Vector3(netPoint.x, netPoint.y + 250, netPoint.z), 1.25f).SetEase(Ease.OutElastic)
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
                .OnComplete(()=> levelPanelUI.gameObject.SetActive(false));
            TitleText.transform.DOMove(
                new Vector3(TitleText.transform.position.x, TitleText.transform.position.y + 30, transform.position.z),
                0.25f);
            
            NextBtn.interactable = false;
            //2.Player移出屏幕
            Character Player = GameManager.Instance.Player;
            //3.关闭Player的Collider
            Player.GetComponent<Collider2D>().enabled = false;
            //2.1将视口坐标转化为世界坐标
            var word = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0.25f, 0));
            Vector3 target = new Vector3(word.x + 3, word.y, word.z);
            //2.2 解除2DCamera 的跟随
            GameManager.Instance.RelieveFollowPlayer();
            //2.3 开始移动
            while (Vector2.Distance(Player.transform.localPosition,target) > 0.15f)
            {
                Player.transform.localPosition= Vector2.MoveTowards(Player.transform.localPosition, target, 3.5f * Time.deltaTime);
                Player.anim.SetBool("isMovenemt",true);
                yield return null;
            }
            Player.anim.SetBool("isMovenemt",false);
            Player.GetComponent<Collider2D>().enabled = true;
            //逐步生成奖励界面
            List<RewordItemBag> rewordItemBags = new List<RewordItemBag>();
            foreach (var Re in  reword.RewordItemList)
            {
                rewordItemBags.Add(Re);
            }
            foreach (var Re in reword.MoneyReword)
            {
                rewordItemBags.Add(Re);
            }
            _RewordUI.gameObject.SetActive(true);
            yield return _RewordUI.InitData(rewordItemBags);
            
            Bind(NextBtn,GameManager.Instance.VictoryQuitScene,"OutChick");
            NextBtn.interactable = true;
            
            yield return null;
        }

        /// <summary>
        /// 玩家死亡战斗失败界面
        /// </summary>
        public void ShowGameOver()
        {
            TitleText.sprite = OverSpine;
            TitleText.transform.DOScale(new Vector3(1.25f, 1.25f, 1), 1.25f).SetEase(Ease.OutElastic)
                .OnComplete(delegate
                {
                    TitleText.transform
                        .DOLocalMove(new Vector3(TitleText.transform.localPosition.x, TitleText.transform.localPosition.y + 400,transform.position.z), 1.25f).
                        OnComplete(
                            delegate
                            {
                                var netPoint = NextBtn.transform.localPosition;
                                NextBtn.transform.DOLocalMove(new Vector3(netPoint.x, netPoint.y + 230, netPoint.z), 1.25f).SetEase(Ease.OutElastic)
                                    .OnComplete(() =>
                                    {
                                        Bind(NextBtn, delegate
                                        {
                                            GameManager.Instance.VictoryQuitScene();
                                        }, "UI_click");
                                    });
                            }
                        );
                });
        }


        public override void Close()
        {
            base.Close();
            if(PanelUI != null)
                Destroy(PanelUI.gameObject);
            NextBtn.transform.position = NextBtnStarPoint;
            NextBtn.onClick.RemoveAllListeners();
            _RewordUI.Close();
            
            TitleText.transform.localScale = Vector3.zero;
            TitleText.transform.position = TitleStarPoint;
        }
    }

}
