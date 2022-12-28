using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
        private Button StateBtn; //属性
        private Image StateImage;
        private Button desBtn;   //故事
        private Image desImage;
        private Button AudiosBtn; //音乐
        private Image AudioImage;
        private RectTransform StateContent;
        private TextMeshProUGUI desText;
        private RectTransform AudioContent;
        private Button CloseBtn;

        
        private PropValue StatePrefab;
        private CharacterBag currentBag;
        private CharacterConfigInfo currentInfo;
        
        public override void Init()
        {
            icon = Get<Image>("UIMask/Back/Info/Mask/icon");
            CharacterName = Get<TextMeshProUGUI>("UIMask/Back/Info/Panel/Row1/Image/Name");
            LevelValue = Get<PropValue>("UIMask/Back/Info/Panel/Row2/PropValue");
            Star = Get<StarContent>("UIMask/Back/Info/Panel/Row2/StarContent");
            ZhiYe = Get<TextMeshProUGUI>("UIMask/Back/Info/Panel/Row2/ZhiYe/Name");
            StateBtn = Get<Button>("UIMask/Back/Info/Panel/Row3/StateBtn");
            StateImage = StateBtn.GetComponent<Image>();
            desBtn = Get<Button>("UIMask/Back/Info/Panel/Row3/DesBtn");
            desImage = desBtn.GetComponent<Image>();
            AudiosBtn = Get<Button>("UIMask/Back/Info/Panel/Row3/AudiosBtn");
            AudioImage = AudiosBtn.GetComponent<Image>();
            StateContent = Get<RectTransform>("UIMask/Back/Info/Panel/Panel/Content");
            desText = Get<TextMeshProUGUI>("UIMask/Back/Info/Panel/Panel/Des");
            AudioContent = Get<RectTransform>("UIMask/Back/Info/Panel/Panel/AudiosContent");
            CloseBtn = Get<Button>("UIMask/Back/CloseBtn");
            StatePrefab = UISystem.Instance.GetPrefab<PropValue>("StateValue");
            LevelValue.Init();
            Star.Init();
            Bind(CloseBtn,Close,"OutChick");
            Bind(StateBtn,()=>SwitchPanel(1),"UI_click");
            Bind(desBtn,()=>SwitchPanel(2),"UI_click");
            Bind(AudiosBtn,()=>SwitchPanel(3),"UI_click");
        }

        /// <summary>
        /// 显示角色详情弹窗
        /// </summary>
        public void ShowCharacterInfo(CharacterBag data)
        {
            CharacterConfigInfo info = InventoryManager.Instance.GetCharacter(data.ID);
            currentBag = data;
            currentInfo = info;
            icon.sprite = info.GetAssets(currentBag.currentStar).OringIcon;
            CharacterName.text = info.CharacterName;
            LevelValue.Show("等级",data.Level.ToString());
            Star.Show(data.currentStar);
            ZhiYe.text = info.battle.ToString();
            UIHelper.Clear(StateContent);
            CreateStateValue();
            desText.text = info.des;
            SwitchPanel(1);
        }

        private void CreateStateValue()
        {
            CharacterState currentCharacterState = currentBag.CurrentCharacterState;
            
            
            var PhysicsAttack = Instantiate(StatePrefab, StateContent);
            PhysicsAttack.Init();
            PhysicsAttack.Show("物理攻击",currentCharacterState.PhysicsAttack.ToString());
            
            var MagicAttack = Instantiate(StatePrefab, StateContent);
            MagicAttack.Init();
            MagicAttack.Show("魔法攻击",currentCharacterState.MagicAttack.ToString());
            
            var HP = Instantiate(StatePrefab, StateContent);
            HP.Init();
            HP.Show("生命值",currentCharacterState.HP.ToString());
            
            var AddHp = Instantiate(StatePrefab, StateContent);
            AddHp.Init();
            AddHp.Show("生命回复",currentCharacterState.AddHp.ToString());
            
            var Power = Instantiate(StatePrefab, StateContent);
            Power.Init();
            Power.Show("力量",currentCharacterState.Power.ToString());
            
            var Intelligence = Instantiate(StatePrefab, StateContent);
            Intelligence.Init();
            Intelligence.Show("智力",currentCharacterState.Intelligence.ToString());
            
            var Defense = Instantiate(StatePrefab, StateContent);
            Defense.Init();
            Defense.Show("防御",currentCharacterState.Defense.ToString());
            
            var AttackSpeed = Instantiate(StatePrefab, StateContent);
            AttackSpeed.Init();
            AttackSpeed.Show("攻击速度",currentCharacterState.AttackSpeed.ToString(CultureInfo.InvariantCulture));
            
            var Cirtical = Instantiate(StatePrefab, StateContent);
            Cirtical.Init();
            Cirtical.Show("暴击几率","%"+currentCharacterState.Cirtical *100);
            
            var CirticalAttack = Instantiate(StatePrefab, StateContent);
            CirticalAttack.Init();
            CirticalAttack.Show("暴击伤害","%"+currentCharacterState.CirticalAttack);
            
            var SkillAttack = Instantiate(StatePrefab, StateContent);
            SkillAttack.Init();
            SkillAttack.Show("技能攻击力","%"+currentCharacterState.SkillAttack);
            
        }
        
        private void SwitchPanel(int index)
        {
            switch (index)
            {
                case 1:
                    StateImage.color = new Color(1, 1, 1, 1);
                    desImage.color = new Color(1, 1, 1, 0);
                    AudioImage.color = new Color(1, 1, 1, 0);
                    StateContent.gameObject.SetActive(true);
                    desText.gameObject.SetActive(false);
                    AudioContent.gameObject.SetActive(false);
                    break;
                case 2:
                    StateImage.color = new Color(1, 1, 1, 0);
                    desImage.color = new Color(1, 1, 1, 1);
                    AudioImage.color = new Color(1, 1, 1, 0);
                    StateContent.gameObject.SetActive(false);
                    desText.gameObject.SetActive(true);
                    AudioContent.gameObject.SetActive(false);
                    break;
                case 3:
                    StateImage.color = new Color(1, 1, 1, 0);
                    desImage.color = new Color(1, 1, 1, 0);
                    AudioImage.color = new Color(1, 1, 1, 1);
                    StateContent.gameObject.SetActive(false);
                    desText.gameObject.SetActive(false);
                    AudioContent.gameObject.SetActive(true);
                    break;
            }
        }

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("CharacterToolTip");
        }
    }
}

