using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class NoticeLineUI : UIBase
    {
        private Image icon;
        private Button OnClick;
        private TextMeshProUGUI titleName;
        private TextMeshProUGUI description;
        private NoticeData currentData;
        public override void Init()
        {
            icon = Get<Image>("icon");
            OnClick = Get<Button>("NextBtn");
            titleName = Get<TextMeshProUGUI>("PropValue/PropName");
            description = Get<TextMeshProUGUI>("description");
            Bind(OnClick,OpenNotice,"OnChick");
        }

        public void InitData(NoticeData data)
        {
            currentData = data;
            if (data.icon == null)
            {
                icon.gameObject.SetActive(false);
            }
            else
            {
                icon.gameObject.SetActive(true);
                icon.sprite = data.icon;
            }
            titleName.text = data._Mode.ToString();
            description.text = data._TitleName;
        }

        private void OpenNotice()
        {
            if (currentData == null) return;
            PopNotice notice = UISystem.Instance.GetUI<PopNotice>("PopNotice");
            notice.ShowLog(currentData);
        }
    }
}

