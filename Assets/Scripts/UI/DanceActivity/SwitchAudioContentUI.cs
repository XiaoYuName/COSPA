using System.Collections;
using System.Collections.Generic;
using ARPG.BasePool;
using ARPG.Config;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class SwitchAudioContentUI : UIBase
    {
        private UGUIVideoPlay VideoPlay;
        private Image CoverImage;
        private TextMeshProUGUI ConverName;
        private Button LeftBtn;
        private Button RightBtn;
        private List<DanceData> CurrentData;
        private int CurretnIndex;
        public override void Init()
        {
            VideoPlay = Get<UGUIVideoPlay>("BK_VideoUI");
            VideoPlay.Init();
            CoverImage = Get<Image>("CoverBaseItem/Farme/icon");
            ConverName = Get<TextMeshProUGUI>("Name");
            LeftBtn = Get<Button>("Left");
            RightBtn = Get<Button>("Right");
            Bind(RightBtn,()=>SetCurrentSlotUI(false),"OnChick");
            Bind(LeftBtn,()=>SetCurrentSlotUI(true),"OnChick");
            CurretnIndex = 0;
        }

        public void InitData(List<DanceData> danceDatas)
        {
            CurrentData = danceDatas;
        }

        public void SetCurrentIndexUI()
        {
            SetIndexUI(CurretnIndex);
        }

        private void SetIndexUI(int index)
        {
            DanceData danceData = CurrentData[index];
            CoverImage.sprite = danceData.icon;
            ConverName.text = danceData.description;
            AudioManager.Instance.PlayAudio(danceData.AudioID);
            VideoPlay.Play(danceData.VideoID,true,true);
        }
        
        private void SetCurrentSlotUI(bool isAdd)
        {
            if (!isAdd)
            {
                if (CurretnIndex != 0)
                    CurretnIndex--;
                else
                    return;
            }
            else
            {
                if (CurretnIndex < CurrentData.Count-1)
                    CurretnIndex++;
                else
                    return;
            }
            SetIndexUI(CurretnIndex);
        }

    }
}

