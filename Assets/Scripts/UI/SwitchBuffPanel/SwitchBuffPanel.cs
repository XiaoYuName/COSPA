using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG
{
    public class SwitchBuffPanel : UIBase
    {
        private TextMeshProUGUI Title;
        private RectTransform content;
        private RegionBuffData CurrentWaweBuff;
        private Button CloseBtn;
        
        public override void Init()
        {
            Title = Get<TextMeshProUGUI>("UIMask/Title");
            content = Get<RectTransform>("UIMask/Content");
            CloseBtn = Get<Button>("UIMask/CloseBtn");
            Bind(CloseBtn,OnClick,"OnClick");
        }


        /// <summary>
        /// 开启BUFF选择器
        /// </summary>
        /// <param name="buffData">选择器BUFF波数</param>
        public IEnumerator OpenSwitchBuff(RegionBuffData buffData)
        {
            CurrentWaweBuff = buffData;
            UIHelper.Clear(content);
            Title.text = "选择你的BUFF";
            yield return CreatBuffSwitchUI(buffData.Buff_ID);
        }

        private IEnumerator CreatBuffSwitchUI(List<string> Buff)
        {
            for (int i = 0; i < Buff.Count; i++)
            {
                SwitchBuffUI buffUI =  UISystem.Instance.InstanceUI<SwitchBuffUI>("SwitchBuffUI");
                buffUI.IniData(Buff[i]);
                yield return new WaitForSeconds(0.025f);
            }
        }

        private void OnClick()
        {
            //TODO: 给玩家添加上BUFF,关闭自身
        }
    }
}

