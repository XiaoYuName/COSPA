using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class CharacterInfoUI : UIBase
    {
        private void Awake()
        {
            Init();
        }

        /// <summary>
        /// 头像
        /// </summary>
        private Image Headicon;
        /// <summary>
        /// 边框
        /// </summary>
        private Image Faram;
        
        /// <summary>
        /// 好感度
        /// </summary>
        private TextMeshProUGUI Favorability;
        /// <summary>
        /// 等级
        /// </summary>
        private TextMeshProUGUI Level;
        /// <summary>
        /// 战斗力
        /// </summary>
        private TextMeshProUGUI FightingCapacity;
        /// <summary>
        /// 当前星阶
        /// </summary>
        private TextMeshProUGUI Star;
        /// <summary>
        /// 自动装备/强化
        /// </summary>
        private Button AutoBtn;
        
        public override void Init()
        {
            Headicon = Get<Image>("Head");
            Faram = Get<Image>("Head/Faram");
            Favorability = Get<TextMeshProUGUI>("Favorability/Value");
            Level = Get<TextMeshProUGUI>("Level/Value");
            FightingCapacity = Get<TextMeshProUGUI>("FightingCapacity/Value");
            Star = Get<TextMeshProUGUI>("Star/Value");
            AutoBtn = Get<Button>("AotuButton");
            Bind(AutoBtn, 
                ()=>UISystem.Instance.ShowPopWindows("提示","正在开发","加油"),"OnChick");
        }

        public void InitData(CharacterBag data)
        {
            CharacterConfigInfo character = InventoryManager.Instance.GetCharacter(data.ID);
            Headicon.sprite = character.Headicon;
            ItemMode mode = character.CharacterStarType switch
            {
                CharacterStarType.三星 => ItemMode.普通,
                CharacterStarType.四星 => ItemMode.稀有,
                CharacterStarType.五星 => ItemMode.神器,
                _ => ItemMode.普通,
            };
            Faram.sprite = InventoryManager.Instance.GetFaramIcon(mode);
            Favorability.text = data.Favorability.ToString();
            Level.text = data.Level.ToString();
            Star.text = data.currentStar.ToString();
            FightingCapacity.text = "100";//TODO: 战斗力暂时写死100
        }
    }
}

