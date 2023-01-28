using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class MemuPanel : UIBase
    {
        private Button coroutineBtn;
        private Button settingsBtn;
        private Button quitBtn;
        
        public override void Init()
        {
            coroutineBtn = Get<Button>("UIMask/Back/CorontineBtn");
            settingsBtn = Get<Button>("UIMask/Back/SettingsBtn");
            quitBtn = Get<Button>("UIMask/Back/QuitBtn");
            
            Bind(coroutineBtn,Close,"OutChick");
            Bind(settingsBtn,delegate {  },"OutChick");
            Bind(quitBtn, delegate
            {
                UISystem.Instance.ShowPopDialogue("退出","确认退出吗,现在退出将不会获得任何奖励","确认","关闭", delegate
                {
                    GameManager.Instance.QuitGameScene();
                }, Close);
            }, "OutChick");
        }


        public override void Open()
        {
            Time.timeScale = 0;
            base.Open();
        }

        public override void Close()
        {
            Time.timeScale = 1;
            base.Close();
        }
    }

}
