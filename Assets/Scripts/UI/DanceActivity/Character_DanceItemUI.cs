using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace ARPG.UI
{
    public class Character_DanceItemUI : UIBase
    {
        private SkeletonGraphic _skeletonGraphic;
        public override void Init()
        {
            _skeletonGraphic = Get<SkeletonGraphic>("CharacterSpine");
        }

        public void IniData(SkeletonDataAsset SpineAssets)
        {
            _skeletonGraphic.skeletonDataAsset = SpineAssets;
            _skeletonGraphic.Initialize(true);
            UIHelper.PlaySpineAnimation(_skeletonGraphic,"BgmStandby",true);
        }
    }
}

