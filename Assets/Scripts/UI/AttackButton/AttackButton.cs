using System;
using ARPG.Config;
using ARPG.UI;
using TMPro;
using UnityEngine.UI;

namespace ARPG
{
    public class AttackButton : UIBase
    {
        private Button AttackBtn;
        private Button SkillBtn_1;
        private Button SkillBtn_2;
        private Button SkillBtn_3;
        private Button SkillBtn_4;

        public override void Init()
        {
            AttackBtn = Get<Button>("Button_Attack");
            SkillBtn_1 = Get<Button>("Button_Skill_01");
            SkillBtn_2 = Get<Button>("Button_Skill_02");
            SkillBtn_3 = Get<Button>("Button_Skill_03");
            SkillBtn_4 = Get<Button>("Button_Skill_04");
        }
        
        /// <summary>
        /// 获取对应技能的CD显示组件
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="text">文本描述组件</param>
        /// <returns>Image 显示CDImage</returns>
        /// <exception cref="Exception">无法确定的技能类型</exception>
        public Image GetSkillCD(SkillType type,out TextMeshProUGUI text)
        {
            switch (type)
            {
                case SkillType.Skill_01:
                    text = Get<TextMeshProUGUI>("Button_Skill_01/CD/value");
                    return Get<Image>("Button_Skill_01/CD");
                case SkillType.Skill_02:
                    text = Get<TextMeshProUGUI>("Button_Skill_02/CD/value");
                    return Get<Image>("Button_Skill_02/CD");
                case SkillType.Skill_03:
                    text = Get<TextMeshProUGUI>("Button_Skill_03/CD/value");
                    return Get<Image>("Button_Skill_03/CD");
                case SkillType.Evolution:
                    text =Get<TextMeshProUGUI>("Button_Skill_04/CD/value");
                    return Get<Image>("Button_Skill_04/CD");
                default:
                    throw new Exception("没有对应技能Button组件");
            }
        }

        /// <summary>
        /// 设置对应技能的数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="item">技能数据,如果为null,则表示未解锁</param>
        public void SetUI(SkillType type,SkillItem item)
        {
            //因为0秒的CD不显示,所以强制为false
            SetSkillCd(type, null != item,false);
            switch (type)
            {
                case SkillType.Attack:
                    if (null == item)
                    {
                        AttackBtn.transform.Find("Icon").GetComponent<Image>().sprite = GameSystem.Instance.GetSprite("DeftualSkill");
                        AttackBtn.GetComponent<Image>().SetNativeSize();
                        break;
                    }
                    AttackBtn.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
                    AttackBtn.transform.Find("Icon").GetComponent<Image>().SetNativeSize();
                    break;
                case SkillType.Skill_01:
                    if (null == item)
                    {
                        SkillBtn_1.GetComponent<Image>().sprite = GameSystem.Instance.GetSprite("DeftualSkill");
                        SkillBtn_1.GetComponent<Image>().SetNativeSize();
                        SkillBtn_1.transform.Find("CD").GetComponent<Image>().fillAmount = 0;
                        SkillBtn_1.transform.Find("name").GetComponent<TextMeshProUGUI>().text = "未解锁";
                        break;
                    }
                    SkillBtn_1.GetComponent<Image>().sprite = item.icon;
                    SkillBtn_1.GetComponent<Image>().SetNativeSize();
                    SkillBtn_1.transform.Find("CD").GetComponent<Image>().fillAmount = 0;
                    SkillBtn_1.transform.Find("name").GetComponent<TextMeshProUGUI>().text = item.SkillName;
                    break;
                case SkillType.Skill_02:
                    if (null == item)
                    {
                        SkillBtn_2.GetComponent<Image>().sprite = GameSystem.Instance.GetSprite("DeftualSkill");
                        SkillBtn_2.GetComponent<Image>().SetNativeSize();
                        SkillBtn_2.transform.Find("CD").GetComponent<Image>().fillAmount = 0;
                        SkillBtn_2.transform.Find("name").GetComponent<TextMeshProUGUI>().text = "未解锁";
                        break;
                    }
                    SkillBtn_2.GetComponent<Image>().sprite = item.icon;
                    SkillBtn_2.GetComponent<Image>().SetNativeSize();
                    SkillBtn_2.transform.Find("CD").GetComponent<Image>().fillAmount = 0;
                    SkillBtn_2.transform.Find("name").GetComponent<TextMeshProUGUI>().text = item.SkillName;
                    break;
                case SkillType.Skill_03:
                    if (null == item)
                    {
                        SkillBtn_3.GetComponent<Image>().sprite = GameSystem.Instance.GetSprite("DeftualSkill");
                        SkillBtn_3.GetComponent<Image>().SetNativeSize();
                        SkillBtn_3.transform.Find("CD").GetComponent<Image>().fillAmount = 0;
                        SkillBtn_3.transform.Find("name").GetComponent<TextMeshProUGUI>().text = "未解锁";
                        break;
                    }
                    SkillBtn_3.GetComponent<Image>().sprite = item.icon;
                    SkillBtn_3.GetComponent<Image>().SetNativeSize();
                    SkillBtn_3.transform.Find("CD").GetComponent<Image>().fillAmount = 0;
                    SkillBtn_3.transform.Find("name").GetComponent<TextMeshProUGUI>().text = item.SkillName;
                    break;
                case SkillType.Evolution:
                    if (null == item)
                    {
                        SkillBtn_4.GetComponent<Image>().sprite = GameSystem.Instance.GetSprite("DeftualSkill");
                        SkillBtn_4.GetComponent<Image>().SetNativeSize();
                        SkillBtn_4.transform.Find("CD").GetComponent<Image>().fillAmount = 0;
                        SkillBtn_4.transform.Find("name").GetComponent<TextMeshProUGUI>().text = "未解锁";
                        break;
                    }
                    SkillBtn_4.GetComponent<Image>().sprite = item.icon;
                    SkillBtn_4.GetComponent<Image>().SetNativeSize();
                    SkillBtn_4.transform.Find("CD").GetComponent<Image>().fillAmount = 0;
                    SkillBtn_4.transform.Find("name").GetComponent<TextMeshProUGUI>().text = item.SkillName;
                    break;
            }
        }
        
        /// <summary>
        /// 设置技能UI是否显示
        /// </summary>
        /// <param name="type">技能类型</param>
        /// <param name="isActive">显示/隐藏</param>
        /// <exception cref="Exception">无法确定的类型</exception>
        public void SetSkillCd(SkillType type,bool isActive,bool ValueActive)
        {
            switch (type)
            {
                case SkillType.Attack:
                    break;
                case SkillType.Skill_01:
                    Get<TextMeshProUGUI>("Button_Skill_01/CD/value").gameObject.SetActive(ValueActive);
                    Get<Image>("Button_Skill_01/CD").gameObject.SetActive(isActive);
                    break;
                case SkillType.Skill_02:
                     Get<TextMeshProUGUI>("Button_Skill_02/CD/value").gameObject.SetActive(ValueActive);
                     Get<Image>("Button_Skill_02/CD").gameObject.SetActive(isActive);
                     break;
                case SkillType.Skill_03:
                    Get<TextMeshProUGUI>("Button_Skill_03/CD/value").gameObject.SetActive(ValueActive);
                    Get<Image>("Button_Skill_03/CD").gameObject.SetActive(isActive);
                    break;
                case SkillType.Evolution:
                    Get<TextMeshProUGUI>("Button_Skill_04/CD/value").gameObject.SetActive(ValueActive);
                    Get<Image>("Button_Skill_04/CD").gameObject.SetActive(isActive);
                    break;
                default:
                    throw new Exception("没有对应技能Button组件");
            }
        }

        /// <summary>
        /// 绑定技能按钮
        /// </summary>
        /// <param name="attack">普攻</param>
        /// <param name="skill_1">技能1</param>
        /// <param name="skill_2">技能2</param>
        /// <param name="skill_3">技能3</param>
        /// <param name="skill_4">技能4(觉醒技)</param>
        public void InitBindButton(Action attack, Action skill_1, Action skill_2, Action skill_3,Action skill_4)
        {
            Open();
            Bind(AttackBtn,attack,"");
            Bind(SkillBtn_1,skill_1,"");
            Bind(SkillBtn_2,skill_2,"");
            Bind(SkillBtn_3,skill_3,"");
            Bind(SkillBtn_4,skill_4,"");
        }
    }
}

