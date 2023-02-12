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

        private Animation SwitchTwistAnim;

        private Animation ShowPanAnim;
        private Image ShowBG;
        private Image ShowIcon;
        
        public override void Init()
        {
            FadeImage = Get<Image>("UIMask/Fade");
            MediaPlayer = Get<MediaPlayer>("UIMask/Viedo");
            RewordTistPanel = Get("UIMask/RewordTwist");
            TwistContent = Get<RectTransform>("UIMask/RewordTwist/Gird2_Bk/gird2");
            SwitchTwistAnim = Get<Animation>("UIMask/Tewwn/SwitchTwistAgg");
            ShowPanAnim = Get<Animation>("UIMask/Tewwn/show/show");
        }

        public void OpenTwisScene(int Amount,TwistDouble data,TwisType _type)
        {
            TwistAmount = Amount;
            currentdata = data;
            FadeImage.color = new Color(0, 0, 0, 0);
            this._type = _type;
            Open();
            ResetIni();
            StartCoroutine(OpenTwis(Amount));
        }

        public void ResetIni()
        {
            UIHelper.Clear(TwistContent);
            SwitchTwistAnim.gameObject.SetActive(false);
            SwitchTwistAnim.Stop();
            ShowPanAnim.gameObject.SetActive(false);
            ShowPanAnim.Stop();
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
            List<string> characterList = new List<string>();
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
                        characterList.Add(currentdata.characterID[UnityEngine.Random.Range(0,currentdata.characterID.Count)]);
                    }else if (value <= currentdata.CharacterDouble)
                    {
                        CardFx Fx=  UISystem.Instance.InstanceUI<CardFx>("CardFx",TwistContent);
                        Fx.IniData(2);
                        characterList.Add(currentdata.CharacterCradsID[UnityEngine.Random.Range(0,currentdata.CharacterCradsID.Count)]);
                    }
                    else
                    {
                        CardFx Fx=  UISystem.Instance.InstanceUI<CardFx>("CardFx",TwistContent);
                        Fx.IniData(1);
                        characterList.Add(currentdata.HandCrads[UnityEngine.Random.Range(0,currentdata.HandCrads.Count)]);
                    }
                }

                
                yield return new WaitForSeconds(0.25f);
            }

            yield return new WaitForSeconds(1.25f);
            for (int i = 0; i < TwistAmount; i++)
            {
                //播放Switch动画
                SwitchTwistAnim.gameObject.SetActive(true);
                SwitchTwistAnim.Play();
                yield return SwitchTwistAnim.clip.length;
                SwitchTwistAnim.gameObject.SetActive(false);
                SwitchTwistAnim.Stop();
                
                Debug.Log("Tween动画一阶段结束");
                //TODO:根据角色星级,切换对应星级Icon Image
                ShowPanAnim.gameObject.SetActive(true);
                ShowPanAnim.Play();
                yield return ShowPanAnim.clip.length;
                Debug.Log("Tween动画二阶段结束");
                ShowPanAnim.gameObject.SetActive(false);
                ShowPanAnim.Stop();
            }
        }
    }
}

