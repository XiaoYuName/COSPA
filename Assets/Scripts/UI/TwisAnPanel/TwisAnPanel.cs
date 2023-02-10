using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class TwisAnPanel : UIBase
    {
        private TwistAnConfig _data;
        private Button UpTwisBtn;
        private Button TwisBtn;
        private Button EquipBtn;
        private GameObject UpTwisPanel;
        private GameObject TwisPanel;
        private GameObject EquipPanel;
        
        public override void Init()
        {
            _data = ConfigManager.LoadConfig<TwistAnConfig>("Twist/TwistData");
            UpTwisBtn = Get<Button>("Mask/Back/SwitchTable/UpTwistBtn");
            TwisBtn = Get<Button>("Mask/Back/SwitchTable/TwistBtn");
            EquipBtn = Get<Button>("Mask/Back/SwitchTable/EquipBtn");
            UpTwisPanel = Get("Mask/Back/TablePanel/UpTwistPanel");
            TwisPanel = Get("Mask/Back/TablePanel/TwistPanel");
            EquipPanel = Get("Mask/Back/TablePanel/EquipPanel");
            SwitchTable(TwisType.PILCK_UP);
            Bind(UpTwisBtn,()=>SwitchTable(TwisType.PILCK_UP),"UI_click");
            Bind(TwisBtn,()=>SwitchTable(TwisType.白金扭蛋),"UI_click");
            Bind(EquipBtn,()=>SwitchTable(TwisType.普通扭蛋),"UI_click");
        }



        public void SwitchTable(TwisType _type)
        {
            UpTwisBtn.GetComponent<Image>().color = _type == TwisType.PILCK_UP ? Color.white : new Color(1, 1, 1, 0);
            UpTwisPanel.gameObject.SetActive(_type == TwisType.PILCK_UP);
            
            TwisBtn.GetComponent<Image>().color = _type == TwisType.白金扭蛋 ? Color.white : new Color(1, 1, 1, 0);
            TwisPanel.gameObject.SetActive(_type == TwisType.白金扭蛋);
            
            EquipBtn.GetComponent<Image>().color = _type == TwisType.普通扭蛋 ? Color.white : new Color(1, 1, 1, 0);
            EquipPanel.gameObject.SetActive(_type == TwisType.普通扭蛋);
        }
    }
}

