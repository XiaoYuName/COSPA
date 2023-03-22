using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 喵斯快跑角色选择界面
    /// </summary>
    public class DanceCharacterContentUI : UIBase
    {
        private Button LeftBtn;
        private Button RightBtn;
        private TextMeshProUGUI CharacterName;
        private TextMeshProUGUI CharacterDes;
        private RectTransform DanceCharacterContent;
        private Scrollbar tweenScrollbar;
        private int SelectIndex;
        private List<DanceCharacterData> ListCharacter;

        public override void Init()
        {
            DanceCharacterContent = Get<RectTransform>("Center/Scroll View/Viewport/CharacterContent");
            RightBtn = Get<Button>("Center/Right");
            LeftBtn = Get<Button>("Center/Left");
            CharacterName = Get<TextMeshProUGUI>("Center/SelectCharacterName");
            CharacterDes = Get<TextMeshProUGUI>("Center/SelectCharacterDes");
            tweenScrollbar = Get<Scrollbar>("Center/Scroll View/Scrollbar");
            Bind(LeftBtn,()=>MovContent(false),UiAudioID.OnChick);
            Bind(RightBtn,()=>MovContent(true),UiAudioID.OutChick);
        }

        public void InitData(List<DanceCharacterData> CharacterList)
        {
            UIHelper.Clear(DanceCharacterContent);
            SelectIndex = 0;
            ListCharacter = CharacterList;
            for (int i = 0; i < CharacterList.Count; i++)
            {
                Character_DanceItemUI ItemUI =  UISystem.Instance.InstanceUI<Character_DanceItemUI>("Character_DanceItemUI",
                    DanceCharacterContent);
                ItemUI.IniData(CharacterList[i].spineAssets.MainShowSpine);
            }
        }

        public void MovContent(bool isRight)
        {
            if (!isRight)
            {
                if (SelectIndex != 0)
                    SelectIndex--;
                else
                    return;
            }
            else
            {
                if (SelectIndex < ListCharacter.Count-1)
                    SelectIndex++;
                else
                    return;
            }

            float Tween_value = 0;
            if (SelectIndex > 0)
            {
                Tween_value = (1 / SelectIndex);
            }
            

            DOTween.To(() => tweenScrollbar.value, x => 
                tweenScrollbar.value = x, Tween_value, 0.25f);
            SetSelectCharacter();
        }

        public void SetSelectCharacter()
        {
            CharacterName.text = ListCharacter[SelectIndex].CharacterName;
            CharacterDes.text = ListCharacter[SelectIndex].description;
        }
    }

}
