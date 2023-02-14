using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class HeadFx : UIBase
    {
        private Image icon;
        private GameObject StarContent;
        private Animation Ef_move2;
        private Animation StarTween;
        private Animation NewTween;
        public override void Init()
        {
            icon = GetComponent<Image>();
            StarContent = Get("Star");
            Ef_move2 = Get<Animation>("Ef_move2");
            StarTween = Get<Animation>("3Star");
            NewTween = Get<Animation>("new");

        }

        public void InitData(string characterID)
        {
            CharacterConfigInfo configInfo = InventoryManager.Instance.GetCharacter(characterID);
            icon.sprite = configInfo.Headicon;
            SetStarContent((int)configInfo.CharacterStarType);
            Ef_move2.gameObject.SetActive(true);
            if ((int)configInfo.CharacterStarType >= 3)
            {
                StarTween.gameObject.SetActive(true);
                StarTween.Play();
            }
            NewTween.gameObject.SetActive(true);
            NewTween.Play();
        }

        private void SetStarContent(int Star)
        {
            for (int i = 0; i < StarContent.transform.childCount-1; i++)
            {
                StarContent.transform.GetChild(i).gameObject.SetActive(i < Star);
            }
        }
    }
}

