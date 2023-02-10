using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using ARPG.UI.Config;
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
            LogText = Get<TextMeshProUGUI>("UIMask/Back/Farme/LogPanel/Down/LogText");
            LogTimeText = Get<TextMeshProUGUI>("UIMask/Back/Farme/LogPanel/Top/LogTimeText");
            Bind(CloseBtn,Close,"OutChick");
            Bind(DisclaimerBtn,()=>SwitchTable(NoticeType.免责声明),"OnChick");
            Bind(VersionBtn,()=>SwitchTable(NoticeType.更新日志),"OnChick");
            Bind(BUGBtn,()=>SwitchTable(NoticeType.BUG信息),"OnChick");
            Bind(CloseLogBtn,()=>LogPanel.gameObject.SetActive(false),"OnChick");
            _noticeConfig = ConfigManager.LoadConfig<NoticeConfig>("Notice/NoticeConfig");
            SwitchTable(NoticeType.更新日志);
            
        }


        private void CreatLineItemUI()
        {
            RectTransform DisclaimerConent =
                DisclaimerPanel.transform.Find("Scroll Rect/Content").GetComponent<RectTransform>();
            RectTransform VersionPanelCoent =
                DisclaimerPanel.transform.Find("Scroll Rect/Content").GetComponent<RectTransform>();
            RectTransform BUGContent =
                DisclaimerPanel.transform.Find("Scroll Rect/Content").GetComponent<RectTransform>();
            UIHelper.Clear(DisclaimerConent);
            UIHelper.Clear(VersionPanelCoent);
            UIHelper.Clear(BUGContent);
            for (int i = 0; i < 3; i++)
            {
                List<NoticeData> noticeDatas = _noticeConfig.GetTypeData((NoticeType) i);
                for (int j = 0; j < noticeDatas.Count; j++)
                {
                    //TODO: 生成子物体对象
                }
            }
            
        }

        private void SwitchTable(NoticeType _type)
        {
            DisclaimerBtn.GetComponent<Image>().color = 
                _type == NoticeType.免责声明 ? Color.white : new Color(1, 1, 1, 0);
            DisclaimerPanel.gameObject.SetActive(_type == NoticeType.免责声明);
            
            VersionBtn.GetComponent<Image>().color = 
                _type == NoticeType.更新日志 ? Color.white : new Color(1, 1, 1, 0);
            VersionPanel.gameObject.SetActive(_type == NoticeType.更新日志);
            
            BUGBtn.GetComponent<Image>().color = 
                _type == NoticeType.BUG信息 ? Color.white : new Color(1, 1, 1, 0);
            BUGPanel.gameObject.SetActive(_type == NoticeType.BUG信息);
        }
    } 
}

