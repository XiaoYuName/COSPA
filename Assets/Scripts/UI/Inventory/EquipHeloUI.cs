using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class EquipHeloUI : UIBase
    {
        [Header("装备槽位类型")]
        public ItemType type;
        /// <summary>
        /// 默认未装备装备的图片
        /// </summary>
        private Image iconImage;

        private Sprite deftualIcon;
        /// <summary>
        /// 等级
        /// </summary>
        private TextMeshProUGUI Level;
        /// <summary>
        /// 强化
        /// </summary>
        private TextMeshProUGUI powor;
        
        [HideInInspector]public EquipHelo currentdata;
        public bool isEquip; //是否已经装备过装备了

        private void Awake()
        {
            Init();
        }

        public override void Init()
        {
            iconImage = GetComponent<Image>();
            deftualIcon = iconImage.sprite;
            Level = Get<TextMeshProUGUI>("Level");
            powor = Get<TextMeshProUGUI>("Powor");
        }

        public void InitData(EquipHelo data)
        {
            bool isNull = string.IsNullOrEmpty(data.item.ID);
            if (isNull)
            {
                iconImage.sprite = deftualIcon;
                currentdata = null;
            }
            else
            {
                currentdata = data;
                iconImage.sprite = GameSystem.Instance.GetSprite(data.item.spriteID);
                Level.text = "Lv "+data.item.level;
                powor.text = "+" + data.Powor;
            }
            isEquip = isNull;
            Level.gameObject.SetActive(!isNull);
            powor.gameObject.SetActive(!isNull);
        }
    }
}

