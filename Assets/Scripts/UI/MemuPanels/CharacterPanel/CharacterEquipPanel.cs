using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 角色装备强化界面
    /// </summary>
    public class CharacterEquipPanel : UIBase
    {
        private Button CloseBtn;
        private TextMeshProUGUI NameText;
        private SkeletonGraphic SpineController; //Spine 控制器
        private CharacterInfoUI CharacterInfoUI;
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,"OutChick");
            NameText = Get<TextMeshProUGUI>("UIMask/Left/Name/NameText");
            SpineController = Get<SkeletonGraphic>("UIMask/Left/Spine");
            SpineController.gameObject.SetActive(false);
            CharacterInfoUI = GetComponentInChildren<CharacterInfoUI>();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="data"></param>
        public void InitData(CharacterBag data)
        {
            CharacterConfigInfo character = InventoryManager.Instance.GetCharacter(data.ID);
            NameText.text = character.CharacterName;
            SpineController.skeletonDataAsset = character.SpineAsset;
            SpineController.AnimationState.SetAnimation(0, character.SpineIdleName, true);
            SpineController.gameObject.SetActive(true);
            CharacterInfoUI.InitData(data);
        }


        public override void Close()
        {
            MainPanel.Instance.RemoveTableChild("CharacterEquipPanel");
            FadeManager.Instance.PlayFade(1,base.Close,1);
        }
    }
}

