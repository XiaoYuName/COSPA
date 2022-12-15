using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class RootTabBtn : UIBase
    {
        private Button btn;
        private Image icon;
        private TextMeshProUGUI titleText;
        private Image BgImage;
        public TableType _type;
        private RootTableItem _data;
        private Animator anim;
        private static readonly int s_IsSelect = Animator.StringToHash("isSelect");

        public override void Init()
        {
            btn = GetComponent<Button>();
            icon = Get<Image>("icon");
            titleText = Get<TextMeshProUGUI>("TabName");
            BgImage = GetComponent<Image>();
            anim = GetComponent<Animator>();
        }

        public void InitData(RootTableItem data)
        {
            _data = data;
            _type = data.Type;
            icon.sprite = data.icon;
            icon.SetNativeSize();
            titleText.text = data.Type.ToString();
            anim.SetBool(s_IsSelect,false);
            Bind(btn, delegate
            {
                if (data.Mode == TableMode.Close)
                {
                    UISystem.Instance.ShowPopWindows("提示","暂未开放","确定");
                    return;
                }
                MainPanel.Instance.SwitchTabBtn(_type);
                FadeManager.Instance.PlayFade(1, delegate
                {
                    UISystem.Instance.OpenUI(data.OpenUIName);
                },2);
            }, "UI_click");
        }


        /// <summary>
        /// 设置按钮的界面状态，true为选中状态，false 为非选中状态
        /// </summary>
        /// <param name="State"></param>
        public void SetState(bool State)
        {
            if (State)
            {
                BgImage.color = Settings.ActiveColor;
                titleText.color = Settings.ActiveColor;
                icon.sprite = _data.selecticon;
            }
            else
            {
                BgImage.color = Settings.NotActiveColor;
                titleText.color = Settings.NotActiveColor;
                icon.sprite = _data.icon;
            }
            icon.SetNativeSize();
            anim.SetBool(s_IsSelect,State);
        }
    }
}

