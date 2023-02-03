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
        private bool isEndClick;
        private List<BuffData> SelectBuffList;


        public override void Init()
        {
            Title = Get<TextMeshProUGUI>("UIMask/Title");
            content = Get<RectTransform>("UIMask/Content");
            CloseBtn = Get<Button>("UIMask/CloseBtn");
            Bind(CloseBtn,OnChick,"OnChick");
            SelectBuffList = new List<BuffData>();
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

        private IEnumerator CreatBuffSwitchUI(List<BuffIDMode> Buff)
        {
            isEndClick = false;
            for (int i = 0; i < Buff.Count; i++)
            {
                SwitchBuffUI buffUI =  UISystem.Instance.InstanceUI<SwitchBuffUI>("SwitchBuffUI",content);
                buffUI.IniData(Buff[i].ToString());
                yield return new WaitForSeconds(0.25f);
            }
            
            while (!isEndClick)
            {
                //在这里阻塞携程，直到BUFF选择完毕
                yield return null;
            }
           
        }

        private void OnChick()
        {
            
            if (SelectBuffList.Count < CurrentWaweBuff.count)
            {
                string des = "当前还有:" + (CurrentWaweBuff.count - SelectBuffList.Count) + "个BUFF未选择";
                UISystem.Instance.ShowTips(des);
                return;
            }

            //TODO: 给玩家添加上BUFF,关闭自身
            isEndClick = true;
            UIHelper.Clear(content);
            CurrentWaweBuff = null;
            Close();
            
        }


        public bool Add_BUFF(BuffData data)
        {
            if (!SelectBuffList.Contains(data) && SelectBuffList.Count < CurrentWaweBuff.count)
            {
                SelectBuffList.Add(data);
                return true;
            }

            return false;
        }

        public bool Remove_BUFF(BuffData data)
        {
            if (SelectBuffList.Contains(data))
            {
                SelectBuffList.Remove(data);
                return true;
            }
            return false;
        }
    }
}

