using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG
{
    public class AttackButton : UIBase
    {
        private Button AttackBtn;
        private Button SkillBtn_1;
        private Button SkillBtn_2;
        private Button SkillBtn_3;

        public override void Init()
        {
            AttackBtn = Get<Button>("Button_Attack");
            SkillBtn_1 = Get<Button>("Button_Skill_01");
            SkillBtn_2 = Get<Button>("Button_Skill_02");
            SkillBtn_3 = Get<Button>("Button_Skill_03");
        }

        public void SetUI(SkillType type,SkillItem item)
        {
            switch (type)
            {
                case SkillType.Attack:
                    AttackBtn.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
                    break;
                case SkillType.Skill_01:
                    SkillBtn_1.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
                    break;
                case SkillType.Skill_02:
                    SkillBtn_2.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
                    break;
                case SkillType.Skill_03:
                    SkillBtn_3.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
                    break;
                case SkillType.Passive:
                    break;
            }
        }

        public void InitBindButton(Action attack, Action skill_1, Action skill_2, Action skill_3)
        {
            Open();
            Bind(AttackBtn,attack,"OnChick");
            Bind(SkillBtn_1,skill_1,"OnChick");
            Bind(SkillBtn_2,skill_2,"OnChick");
            Bind(SkillBtn_3,skill_3,"OnChick");
        }
    }
}

