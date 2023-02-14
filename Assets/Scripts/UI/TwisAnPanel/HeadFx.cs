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
        private Animation ItemTween;
        public override void Init()
        {
            icon = GetComponent<Image>();
            StarContent = Get("Star");
            Ef_move2 = Get<Animation>("Ef_move2");
            StarTween = Get<Animation>("3Star");
            NewTween = Get<Animation>("new");
            ItemTween = Get<Animation>("s");

        }

        public void InitData(string characterID)
        {
            CharacterConfigInfo configInfo = InventoryManager.Instance.GetCharacter(characterID);
            icon.sprite = configInfo.Headicon;
            AudioManager.Instance.PlayAudio("HeadFx");
            SetStarContent((int)configInfo.CharacterStarType);
            Ef_move2.gameObject.SetActive(true);
            StartCoroutine(WaitAnimation(characterID));
            if ((int)configInfo.CharacterStarType >= 3)
            {
                StarTween.gameObject.SetActive(true);
                StarTween.Play();
            }
        }

        private IEnumerator WaitAnimation(string ID)
        {
            yield return new WaitForSeconds(Ef_move2.clip.length);
            CharacterBag characterBag = InventoryManager.Instance.GetCharacterBag(ID);
            if (characterBag != null)
            {
                //已有该角色,转换为秘宝
                ItemTween.gameObject.SetActive(true);
                ItemTween.Play();
            }
            else
            {
                //添加该角色到背包
                NewTween.gameObject.SetActive(true);
                NewTween.Play();
            }
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

