using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using UnityEngine;

namespace ARPG.UI
{
    public class RegionGridScene : UIBase
    {
        private RegionItemUI[] _regionItemUi;
        public override void Init()
        {
            _regionItemUi = transform.GetComponentsInChildren<RegionItemUI>();
        }


        public void InitData(RegionLine line)
        {
            for (int i = 0; i < _regionItemUi.Length; i++)
            {
                _regionItemUi[i].Init();
                _regionItemUi[i].SetNotData();
            }
            
            
            for (int i = 0; i < line.RegionItemList.Count; i++)
            {
                if (i >= _regionItemUi.Length) return;
               
                _regionItemUi[i].InitData(line.RegionName,line.RegionItemList[i]);
            }
        }
    }
}

