using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private RectTransform content;
        private SlotUI _SlotUI;
        private List<SlotUI> SlotList;
        private ItemToolTip _toolTip;
        /// <summary>
        /// 当前界面角色
        /// </summary>
        private CharacterBag currentCharacterBag;
        
        /// <summary>
        /// 装备列表
        /// </summary>
        private EquipHeloUI[] _equipHeloUis;
        
        
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,"OutChick");
            NameText = Get<TextMeshProUGUI>("UIMask/Left/Name/NameText");
            SpineController = Get<SkeletonGraphic>("UIMask/Left/Spine");
            content = Get<RectTransform>("UIMask/Right/Scroll Rect/Content");
            SpineController.gameObject.SetActive(false);
            CharacterInfoUI = GetComponentInChildren<CharacterInfoUI>();
            _toolTip = GetComponentInChildren<ItemToolTip>(true);
            _SlotUI = UISystem.Instance.GetPrefab<SlotUI>("SlotUI");
            SlotList = new List<SlotUI>();
            _toolTip.Init();
            _toolTip.Close();
            _equipHeloUis = GetComponentsInChildren<EquipHeloUI>();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="data"></param>
        public void InitData(CharacterBag data)
        {
            CharacterConfigInfo character = InventoryManager.Instance.GetCharacter(data.ID);
            currentCharacterBag = data;
            NameText.text = character.CharacterName;
            SpineController.skeletonDataAsset = character.SpineAsset;
            SpineController.AnimationState.ClearTracks();
            SpineController.Initialize(true);
            SpineController.AnimationState.SetAnimation(0, character.SpineIdleName, true);
            SpineController.gameObject.SetActive(true);
            CharacterInfoUI.InitData(data);
            CreateEquipHelo(data);
            CreateSlotUI();
        }
        
        /// <summary>
        /// 创建背包Item
        /// </summary>
        private void CreateSlotUI()
        {
            UIHelper.Clear(content);
            foreach (var Bag in InventoryManager.Instance.GetItemAllBag())
            {
               

               Item item = InventoryManager.Instance.GetItem(Bag.ID);
               if(item.Type == ItemType.材料)continue;
               SlotUI Obj =  Instantiate(_SlotUI, content);
               Obj.Init();
               Obj.InitData(Bag);
            }
        }

        /// <summary>
        /// 創建自身装备
        /// </summary>
        private void CreateEquipHelo(CharacterBag data)
        {
            for (int i = 0; i < data.equipHelos.Length; i++)
            {
                if (data.equipHelos[i].ItemType == _equipHeloUis[i].type)
                {
                    _equipHeloUis[i].InitData(data.equipHelos[i]);
                }
            }
        }

        /// <summary>
        /// 装备一件装备
        /// </summary>
        /// <param name="data">数据文件</param>
        public void UpdateEquipHolo(ItemBag data)
        {
            int Code =currentCharacterBag.SetEquipHelo(data);
            switch (Code)
            {
                case 1:
                    UISystem.Instance.ShowPopWindows("提示","玩家等级不足","确定");
                    break;
                case 2:
                    break;
                case 3:
                    CharacterConfigInfo character = InventoryManager.Instance.GetCharacter(currentCharacterBag.ID);
                    PlaySpineAnimation(character.EquipAnimName);
                    //2.通知刷新各个界面UI
                    //2.1 刷新背包
                    CreateSlotUI();
                    //2.2 刷新装备栏装备
                    CreateEquipHelo(currentCharacterBag);
                    break;
            }
        }

        /// <summary>
        /// 点击提示卸下弹窗后的回调函数
        /// </summary>
        public void PlayCode()
        {
            CharacterConfigInfo character = InventoryManager.Instance.GetCharacter(currentCharacterBag.ID);
            PlaySpineAnimation(character.EquipAnimName);
            //2.通知刷新各个界面UI
            //2.1 刷新背包
            CreateSlotUI();
            //2.2 刷新装备栏装备
            CreateEquipHelo(currentCharacterBag);
        }

        public void ShowItemToolTip(ItemBag bag)
        {
            _toolTip.InitData(bag);
        }


        /// <summary>
        /// 播放当前角色Spine动画
        /// </summary>
        /// <param name="playName">动画名称</param>
        private void PlaySpineAnimation(string playName)
        {
            SpineController.AnimationState.SetAnimation(0, playName, false);
            CharacterConfigInfo character = InventoryManager.Instance.GetCharacter(currentCharacterBag.ID);
            SpineController.AnimationState.AddAnimation(0,character.SpineIdleName,true,1.333f);
        }

        public override void Close()
        {
            MainPanel.Instance.RemoveTableChild("CharacterEquipPanel");
            FadeManager.Instance.PlayFade(1,base.Close,1);
            _toolTip.Close();
        }
    }
}

