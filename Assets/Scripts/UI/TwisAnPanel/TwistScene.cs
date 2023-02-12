using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using DG.Tweening;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace ARPG
{
    public class TwistScene : UIBase
    {
        private Image FadeImage;
        private MediaPlayer MediaPlayer;
        private GameObject RewordTistPanel;
        private RectTransform TwistContent;
        private int TwistAmount;
        private TwistDouble currentdata;
        private TwisType _type;
        
        public override void Init()
        {
            FadeImage = Get<Image>("UIMask/Fade");
            MediaPlayer = Get<MediaPlayer>("UIMask/Viedo");
            RewordTistPanel = Get("UIMask/RewordTwist");
            TwistContent = Get<RectTransform>("UIMask/RewordTwist/Gird2_Bk/gird2");
        }

        public void OpenTwisScene(int Amount,TwistDouble data,TwisType _type)
        {
            TwistAmount = Amount;
            currentdata = data;
            FadeImage.color = new Color(0, 0, 0, 0);
            this._type = _type;
            Open();
            UIHelper.Clear(TwistContent);
            StartCoroutine(OpenTwis(Amount));
        }

        public IEnumerator OpenTwis(int amount)
        {
            yield return FadeImage.DOFade(1, 0.25f);
            MediaPlayer.gameObject.SetActive(true);
            MediaPlayer.Play();
            FadeImage.DOFade(0,0.1F);
            MediaPlayer.Events.AddListener(WaitVideoEnd);
            AudioManager.Instance.PlayAudio(amount < 10?"TwistOne":"TwistTen");
        }

        public void WaitVideoEnd(MediaPlayer player, MediaPlayerEvent.EventType t1, ErrorCode code)
        {
            if (player != MediaPlayer) return;
            if (t1 != MediaPlayerEvent.EventType.FinishedPlaying) return;
            MediaPlayer.Stop();
            MediaPlayer.gameObject.SetActive(false);
            StartCoroutine(CreatRewordScnen());
        }

        public IEnumerator CreatRewordScnen()
        {
            RewordTistPanel.gameObject.SetActive(true);
            for (int i = 0; i < TwistAmount; i++)
            {
                Random random = new Random();
                double value= random.NextDouble();
                if (_type == TwisType.PILCK_UP)
                {
                    if (value <= currentdata.UpDouble)
                    {
                        CardFx Fx=  UISystem.Instance.InstanceUI<CardFx>("CardFx",TwistContent);
                        Fx.IniData(3);
                        
                    }else if (value <= currentdata.CharacterDouble)
                    {
                        CardFx Fx=  UISystem.Instance.InstanceUI<CardFx>("CardFx",TwistContent);
                        Fx.IniData(2);
                    }
                    else
                    {
                        CardFx Fx=  UISystem.Instance.InstanceUI<CardFx>("CardFx",TwistContent);
                        Fx.IniData(1);
                    }
                }

                
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}

