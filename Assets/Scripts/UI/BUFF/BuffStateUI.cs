using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.UI
{
    public class BuffStateUI : UIBase
    {
        private RectTransform content;
        private Dictionary<IBuff, BuffUI> BuffUis = new Dictionary<IBuff, BuffUI>();
        public override void Init()
        {
            content = GetComponent<RectTransform>();
        }


        /// <summary>
        /// 添加BUFF_UI
        /// </summary>
        public void AddBuffItemUI(IBuff iBuff)
        {
            if(BuffUis.ContainsKey(iBuff))return;
            BuffUI ui = UISystem.Instance.InstanceUI<BuffUI>("BuffUI",content);
            ui.InitData(iBuff.data);
            BuffUis.Add(iBuff,ui);
        }

        /// <summary>
        /// 刷新BUFF——UI;
        /// </summary>
        public void RefBUFF_UI(IBuff iBuff)
        {
            if (BuffUis.ContainsKey(iBuff))
            {
                BuffUis[iBuff].RefUI(iBuff.curretnLevel);
            }
        }


        /// <summary>
        /// 删除BUFF_UI
        /// </summary>
        public void RemoveBUFF_UI(IBuff iBuff)
        {
            if (BuffUis.ContainsKey(iBuff))
            {
                BuffUI ui = BuffUis[iBuff];
                BuffUis.Remove(iBuff);
                Destroy(ui.gameObject);
            }
        }


        /// <summary>
        /// 清除所有UI：适用于游戏结束与对应BUFF获得者死亡
        /// </summary>
        public void RemoveAll_UI()
        {
            foreach (var Key in BuffUis)
            {
                Destroy(BuffUis[Key.Key]);
            }
            BuffUis.Clear();
        }
        
    }
}

