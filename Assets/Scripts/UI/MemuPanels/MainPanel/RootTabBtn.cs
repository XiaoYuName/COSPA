using ARPG.UI.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class RootTabBtn : UIBase
    {
        private Button btn;
        public TableType _type;
        private RootTableItem data;
        private GameObject anim;
        private GameObject icon;

        public override void Init()
        {
            btn = GetComponent<Button>();
            anim = Get("animation");
            icon = Get("icon");
            data = MainPanel.Instance.GetTabeleData(_type);
            Bind(btn, OnClick, "UI_click");
        }

        public void OnClick()
        {
            if (data.Mode == TableMode.Close)
            {
                UISystem.Instance.ShowPopWindows("提示","暂未开放","确定");
                return;
            }
            FadeManager.Instance.PlayFade(0.25f, delegate
            {
                MainPanel.Instance.SwitchTabBtn(_type);
                UISystem.Instance.OpenUI(data.OpenUIName);
                PlayAnimation(true);
            },0.25f);
        }

        public void PlayAnimation(bool isPlay)
        {
            anim.gameObject.SetActive(isPlay);
            icon.gameObject.SetActive(!isPlay);
        }

    }
}

