using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class CharacterInfo : UIBase
    {
        private Image icon;
        private Transform StarContent;
        private TextMeshProUGUI Level;
        private Button btn;
        [HideInInspector]public string CharacterID;
        public override void Init()
        {
            icon = Get<Image>("Mask/icon");
            StarContent = Get("StarContent").transform;
            Level = Get<TextMeshProUGUI>("Faram/Level");
            btn = GetComponent<Button>();
            btn.onClick.AddListener(()=>UISystem.Instance.ShowPopWindows("提示", "暂未开放", "退出"));

        }

        public void InitData(CharacterBag info)
        {
            CharacterConfigInfo character = InventoryManager.Instance.GetCharacter(info.ID);
            CharacterID = info.ID;
            icon.sprite = character.CharacterPanelIcon;
            Level.text = "等级 "+info.Level;
            SetInfoStar(info.currentStar);
        }

        /// <summary>
        /// 设置显示的星级
        /// </summary>
        /// <param name="star"></param>
        private void SetInfoStar(int star)
        {
            for (int j = 0; j < StarContent.childCount; j++)
            {
                GameObject child = StarContent.GetChild(j).gameObject;
                child.transform.GetChild(0).gameObject.SetActive(j <= star - 1);
            }
        }

    }
}

