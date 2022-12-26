using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using DG.Tweening;
using UnityEngine;

namespace ARPG.UI
{
    public class RewordUI : UIBase
    {
        private RectTransform Content;
        private WaitForSeconds _seconds;
        public override void Init()
        {
            Content = Get<RectTransform>("RewordContent");
            _seconds = new WaitForSeconds(Settings.RewordTime);
        }

        public IEnumerator InitData(List<RewordItemBag> itemBags)
        {
             Content.transform.localScale = new Vector3(0, 0, 0);
             Open();
             UIHelper.Clear(Content);
             Content.DOScale(new Vector3(1, 1, 1), 0.35f).SetEase(Ease.OutSine);
             yield return new WaitForSeconds(1.25f);
             for (int i = 0; i < itemBags.Count; i++)
             {
                 InventoryManager.Instance.AddItem(itemBags[i].itemBag); //背包添加同步一阵内
                 CreateSlotUI(itemBags[i]);
                 yield return _seconds;
             }
            
        }


        private void CreateSlotUI(RewordItemBag itemBag)
        {
            RewordSlotUI Slot = UISystem.Instance.InstanceUI<RewordSlotUI>("RewordSlotUI",Content);
            Slot.InitData(itemBag);
        }
    }

}
