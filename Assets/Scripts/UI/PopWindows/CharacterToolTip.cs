using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class CharacterToolTip : UIBase
    {
        /// <summary>
        /// 立绘
        /// </summary>
        private Image icon;
        /// <summary>
        /// 名字
        /// </summary>
        private TextMeshProUGUI CharacterName;
        /// <summary>
        /// 等级
        /// </summary>
        private PropValue LevelValue;
        /// <summary>
        /// 当前星级
        /// </summary>
        private StarContent Star;
        /// <summary>
        /// 职业
        /// </summary>
        private TextMeshProUGUI ZhiYe;

        private MemuTableType[] TableType;
        private MemuTableContent[] TableContents;
        
        private Button CloseBtn;
        
        private CharacterBag currentBag;

        private Button MaxSiezBtn;
        private Button SwitchiconBtn;
        private Button SwitchWidthBtn;
        private MaxToolTip MaxToolTip;
        private MaxType _type;

        public override void Init()
        {
            icon = Get<Image>("UIMask/Back/Info/Mask/icon");
            CharacterName = Get<TextMeshProUGUI>("UIMask/Back/Info/Panel/Row1/Image/Name");
            LevelValue = Get<PropValue>("UIMask/Back/Info/Panel/Row2/State_Value");
            Star = Get<StarContent>("UIMask/Back/Info/Panel/Row2/StarContent");
            ZhiYe = Get<TextMeshProUGUI>("UIMask/Back/Info/Panel/Row1/ZhiYe/Name");
            CloseBtn = Get<Button>("UIMask/Back/CloseBtn");
            LevelValue.Init();
            Star.Init();
            TableType = transform.GetComponentsInChildren<MemuTableType>();
            TableContents = transform.GetComponentsInChildren<MemuTableContent>();
            Bind(CloseBtn,Close,"OutChick");
            foreach (var tableItem in TableType)
            {
                tableItem.Init();
                tableItem.BindOnClick(SwitchPanel);
            }
            foreach (var content in TableContents)
            {
                content.Init();
            }

            MaxSiezBtn = Get<Button>("UIMask/Back/Info/Mask/MaxSizeBtn");
            SwitchiconBtn = Get<Button>("UIMask/Back/Info/Mask/Switch_icon_Btn");
            
            MaxToolTip = Get<MaxToolTip>("UIMask/MaxToolTip");
            MaxToolTip.Init();
            _type = MaxType.Video;
            Bind(MaxSiezBtn,ShoMaxToolTip,UiAudioID.OnChick);
        }

        /// <summary>
        /// 显示角色详情弹窗
        /// </summary>
        public void ShowCharacterInfo(CharacterBag data)
        {
            CharacterConfigInfo info = InventoryManager.Instance.GetCharacter(data.ID);
            SwitchiconBtn.gameObject.SetActive(info.CharacterStarType == CharacterStarType.三星);
            
            currentBag = data;
            icon.sprite = info.GetAssets(currentBag.currentStar).OringIcon;
            CharacterName.text = info.CharacterName;
            LevelValue.Show("等级",data.Level.ToString());
            Star.Show(data.currentStar);
            ZhiYe.text = info.battle.ToString();
            CreateStateValue();
            InitRow1Table(info);
            InitRow2Table(info);
            SwitchPanel(MemuTableMode.属性);
        }

        private void CreateStateValue()
        {
            CharacterState currentCharacterState = currentBag.CurrentCharacterState;
            RectTransform StateContent = null;
            for (int i = 0; i < TableContents.Length; i++)
            {
                if (TableContents[i].tableType == MemuTableMode.属性)
                {
                    StateContent = TableContents[i].GetContent("State_View/Viewport/Content");
                    break;
                }
            }
            UIHelper.Clear(StateContent);
            if (StateContent == null) return;
            var Power = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            Power.Init();
            Power.Show("力量",currentCharacterState.Power.ToString());
            
            var Intelligence = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            Intelligence.Init();
            Intelligence.Show("智力",currentCharacterState.Intelligence.ToString());
            
            var vit = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            vit.Init();
            vit.Show("体力",currentCharacterState.Vit.ToString());
            
            var Agility = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            Agility.Init();
            Agility.Show("敏捷",currentCharacterState.Agility.ToString());
            
            var PhysicsAttack = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            PhysicsAttack.Init();
            PhysicsAttack.Show("物理攻击",currentCharacterState.PhysicsAttack.ToString());
            
            var MagicAttack = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            MagicAttack.Init();
            MagicAttack.Show("魔法攻击",currentCharacterState.MagicAttack.ToString());
            
            var HP = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            HP.Init();
            HP.Show("HP",currentCharacterState.HP.ToString());
            
            var PhysicsDefense = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            PhysicsDefense.Init();
            PhysicsDefense.Show("物理防御",currentCharacterState.PhysicsDefense.ToString());
            
            var Defense = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            Defense.Init();
            Defense.Show("魔法防御",currentCharacterState.MagicDefense.ToString());
            
            var AttackSpeed = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            AttackSpeed.Init();
            AttackSpeed.Show("攻击速度",(int)currentCharacterState.AttackSpeed+"%");
            
            var ReleaseSpeed = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            ReleaseSpeed.Init();
            ReleaseSpeed.Show("释放速度",(int)currentCharacterState.ReleaseSpeed+"%");
            
            var MovSpeed = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            MovSpeed.Init();
            MovSpeed.Show("移动速度",(int)currentCharacterState.MovSpeed+"%");
            
            
            var Cirtical = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            Cirtical.Init();
            Cirtical.Show("暴击几率",(int)currentCharacterState.Cirtical*100+"%");
            
            var CirticalAttack = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            CirticalAttack.Init();
            CirticalAttack.Show("暴击伤害",(int)currentCharacterState.CirticalAttack*100+"%");
            
            var SkillAttack = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            SkillAttack.Init();
            SkillAttack.Show("技能攻击力",currentCharacterState.SkillAttack+"%");
            
            var Bloodintake = UISystem.Instance.InstanceUI<PropValue>("StateBig_Value", StateContent);
            Bloodintake.Init();
            Bloodintake.Show("吸血量",currentCharacterState.Bloodintake+"%");

        }

        private void InitRow1Table(CharacterConfigInfo info)
        {
            TextMeshProUGUI DesText = null;
            for (int i = 0; i < TableContents.Length; i++)
            {
                if (TableContents[i].tableType == MemuTableMode.属性)
                {
                    DesText = TableContents[i].GetChild<TextMeshProUGUI>("ModeType");
                    break;
                }
            }
            if(DesText == null)return;
            DesText.text =GameSystem.Instance.GetBattleDescription(info.battle);
        }
        
        private void InitRow2Table(CharacterConfigInfo info)
        {
            TextMeshProUGUI DesText = null;
            for (int i = 0; i < TableContents.Length; i++)
            {
                if (TableContents[i].tableType == MemuTableMode.故事)
                {
                    DesText = TableContents[i].GetChild<TextMeshProUGUI>("DesText");
                    break;
                }
            }
            if(DesText == null)return;
            DesText.text = info.des;
        }
        
        private void SwitchPanel(MemuTableMode _type)
        {
            if (TableType.Any(t => t.tableType == _type) && TableType.Any(t => t.tableType == _type))
            {
                for (int i = 0; i < TableType.Length; i++)
                {
                    TableType[i].OnClick(TableType[i].tableType == _type);
                }

                for (int i = 0; i < TableContents.Length; i++)
                {
                    TableContents[i].gameObject.SetActive(TableContents[i].tableType == _type);
                }
            }
        }

        private void ShoMaxToolTip()
        {
            CharacterConfigInfo info = InventoryManager.Instance.GetCharacter(currentBag.ID);
            MaxToolTip.ShowMax(_type,currentBag.currentStar,info);
        }
        
        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("CharacterToolTip");
        }
    }
}

