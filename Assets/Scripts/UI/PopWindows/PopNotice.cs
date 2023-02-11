using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using ARPG.UI.Config;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG
{
    /// <summary>
    /// 通知界面
    /// </summary>
    public class PopNotice : UIBase
    {
        private Button CloseBtn;
        //-table-//
        private Button DisclaimerBtn;
        private Button VersionBtn;
        private Button BUGBtn;
        private GameObject DisclaimerPanel;
        private GameObject VersionPanel;
        private GameObject BUGPanel;
        private GameObject LogPanel;
        private RectTransform DisclaimerConent;
        private RectTransform VersionPanelCoent;
        private RectTransform BUGContent;
        
        private Button CloseLogBtn;
        private TextMeshProUGUI LogText;
        private TextMeshProUGUI LogTimeText;
        private NoticeConfig _noticeConfig;

        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Back/Farme/Close");
            DisclaimerBtn = Get<Button>("UIMask/Back/Farme/SwitchTable/DisclaimerBtn");
            VersionBtn = Get<Button>("UIMask/Back/Farme/SwitchTable/VersionBtn");
            BUGBtn = Get<Button>("UIMask/Back/Farme/SwitchTable/BUGBtn");
            DisclaimerPanel = Get("UIMask/Back/Farme/Panel/DisclaimerPanel");
            VersionPanel = Get("UIMask/Back/Farme/Panel/VersionPanel");
            BUGPanel = Get("UIMask/Back/Farme/Panel/BUGPanel");
            LogPanel = Get("UIMask/Back/Farme/LogPanel");
            CloseLogBtn = Get<Button>("UIMask/Back/Farme/LogPanel/Top/Close");
            LogText = Get<TextMeshProUGUI>("UIMask/Back/Farme/LogPanel/Down/Scroll View/Viewport/Content/LogText");
            LogTimeText = Get<TextMeshProUGUI>("UIMask/Back/Farme/LogPanel/Top/LogTimeText");
            
             DisclaimerConent = DisclaimerPanel.transform.Find("Scroll Rect/Mask/Content").GetComponent<RectTransform>();
             VersionPanelCoent = VersionPanel.transform.Find("Scroll Rect/Mask/Content").GetComponent<RectTransform>();
             BUGContent = BUGPanel.transform.Find("Scroll Rect/Mask/Content").GetComponent<RectTransform>();
            Bind(CloseBtn,Close,"OutChick");
            Bind(DisclaimerBtn,()=>SwitchTable(NoticeType.活动信息),"OnChick");
            Bind(VersionBtn,()=>SwitchTable(NoticeType.更新日志),"OnChick");
            Bind(BUGBtn,()=>SwitchTable(NoticeType.BUG信息),"OnChick");
            Bind(CloseLogBtn,()=>LogPanel.gameObject.SetActive(false),"OnChick");
            _noticeConfig = ConfigManager.LoadConfig<NoticeConfig>("Notice/NoticeConfig");
            SwitchTable(NoticeType.更新日志);
            CreatLineItemUI();
            transform.localScale = Vector3.zero;
            
        }

        public void Show(NoticeType _type)
        {
            Open();
            transform.DOScale(Vector3.one, Settings.PopTweenTime).OnComplete(()=>SwitchTable(_type));
        }
        
        
        private void CreatLineItemUI()
        {
            UIHelper.Clear(DisclaimerConent);
            UIHelper.Clear(VersionPanelCoent);
            UIHelper.Clear(BUGContent);
            for (int i = 0; i < 3; i++)
            {
                List<NoticeData> noticeDatas = _noticeConfig.GetTypeData((NoticeType) i);
                for (int j = 0; j < noticeDatas.Count; j++)
                {
                   NoticeLineUI lineUI = UISystem.Instance.InstanceUI<NoticeLineUI>("NoticeLineUI",GetTypePanel((NoticeType)i));
                   lineUI.InitData(noticeDatas[j]);
                }
            }
            
        }

        private RectTransform GetTypePanel(NoticeType _type)
        {
            return _type switch
            {
                NoticeType.活动信息 => DisclaimerConent,
                NoticeType.更新日志 => VersionPanelCoent,
                NoticeType.BUG信息 => BUGContent,
                _ => DisclaimerConent,
            };
        }

        private void SwitchTable(NoticeType _type)
        {
            DisclaimerBtn.GetComponent<Image>().color = 
                _type == NoticeType.活动信息 ? Color.white : new Color(1, 1, 1, 0);
            DisclaimerPanel.gameObject.SetActive(_type == NoticeType.活动信息);
            
            VersionBtn.GetComponent<Image>().color = 
                _type == NoticeType.更新日志 ? Color.white : new Color(1, 1, 1, 0);
            VersionPanel.gameObject.SetActive(_type == NoticeType.更新日志);
            
            BUGBtn.GetComponent<Image>().color = 
                _type == NoticeType.BUG信息 ? Color.white : new Color(1, 1, 1, 0);
            BUGPanel.gameObject.SetActive(_type == NoticeType.BUG信息);
        }

        public void ShowLog(NoticeData noticeData)
        {
            
            LogText.text = noticeData.description;
            LogTimeText.text = noticeData._Time;
            LayoutRebuilder.ForceRebuildLayoutImmediate(LogPanel.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(LogText.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(LogText.transform.parent.GetComponent<RectTransform>());
            LogPanel.gameObject.SetActive(true);
        }


        public override void Close()
        {
            transform.DOScale(Vector3.zero, Settings.PopTweenTime).OnComplete(() =>
            {
                LogPanel.gameObject.SetActive(false);
                base.Close();
            });
        }
    } 
}

