using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace ARPG.UI
{
    public class EnemySlotUI : UIBase
    {
        private Image icon;
        protected Image faram;
        protected TextMeshProUGUI count;
        private Button OpenBtn;
        protected EnemyData _data;
        private TextMeshProUGUI winText;
        public override void Init()
        {
            icon = GetComponent<Image>();
            faram = Get<Image>("Faram");
            count = Get<TextMeshProUGUI>("Level");
            winText = Get<TextMeshProUGUI>("Win");
        }
        public void InitData(int win, EnemyBag data)
        {
            _data = EnemyManager.Instance.GetData(data.dataID);
            icon.sprite = _data.icon;
            winText.text = "Win: "+win;

            ItemMode mode = _data.Type switch
            {
                EnemyType.Ordinary => ItemMode.普通,
                EnemyType.Elite => ItemMode.稀有,
                EnemyType.BOSS => ItemMode.神器,
                _ => ItemMode.普通,
            };
            faram.sprite = InventoryManager.Instance.GetFaramIcon(mode);
            count.text = "Count: "+data.count;
        }

        
    }
}

