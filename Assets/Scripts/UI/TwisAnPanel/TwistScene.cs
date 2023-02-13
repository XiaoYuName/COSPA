using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.BasePool;
using ARPG.Config;
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
        private Image ShowIcon;

        private MediaPlayer BK_Video;
        private Animation Name_3Star;
        private Image Name_3Image;


        private RectTransform HeadContent;
        private GameObject CorotineBtns;
        
        public override void Init()
        {
            FadeImage = Get<Image>("UIMask/Fade");
            MediaPlayer = Get<MediaPlayer>("UIMask/Viedo");
            RewordTistPanel = Get("UIMask/RewordTwist");
            TwistContent = Get<RectTransform>("UIMask/RewordTwist/Gird2_Bk/gird2");
            SwitchTwistAnim = Get<Animation>("UIMask/Tewwn/SwitchTwistAgg");
            ShowPanAnim = Get<Animation>("UIMask/Tewwn/show/show");
            ShowIcon = Get<Image>("UIMask/Tewwn/show/show/Card_mask");
            BK_Video = Get<MediaPlayer>("UIMask/Tewwn/BK_Video");
            Name_3Star = Get<Animation>("UIMask/Tewwn/BK_Video/name_3star");
            Name_3Image = Get<Image>("UIMask/Tewwn/BK_Video/name_3star/nnnn");
            HeadContent = Get<RectTransform>("UIMask/RewordTwist/HeadContent");
            CorotineBtns = Get("UIMask/RewordTwist/CorotineBtn");
        }

        /// <summary>
        /// 开启扭蛋流程
        /// </summary>
        /// <param name="Amount">扭蛋次数</param>
        /// <param name="data">类型数据</param>
        /// <param name="twisType">扭蛋类型</param>
        public void OpenTwisScene(int Amount,TwistDouble data,TwisType twisType)
        {
            TwistAmount = Amount;
            currentdata = data;
            FadeImage.color = new Color(0, 0, 0, 0);
            this._type = twisType;
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
            CorotineBtns.gameObject.SetActive(false);
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
            TwistContent.gameObject.SetActive(true);
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
                yield return new WaitForSeconds(SwitchTwistAnim.clip.length-0.35f);
                SwitchTwistAnim.gameObject.SetActive(false);
                SwitchTwistAnim.Stop();
                
                Debug.Log("Tween动画一阶段结束");
                CharacterConfigInfo characterData = InventoryManager.Instance.GetCharacter(characterList[i]);
                string SpriteID = characterData.CharacterStarType switch
                {
                    CharacterStarType.一星 => "StarType_Two", //TODO: 暂无找到一星卡面Sprite 先用二星替代
                    CharacterStarType.二星 => "StarType_Three",
                    CharacterStarType.三星 => "StarType_Three",
                    _ => "StarType_Two"
                };
                ShowIcon.sprite = GameSystem.Instance.GetSprite(SpriteID);
                ShowPanAnim.gameObject.SetActive(true);
                ShowPanAnim.Play();
                Debug.Log("二阶动画时长 : "+ShowPanAnim.clip.length);
                yield return new WaitForSeconds(ShowPanAnim.clip.length-1.2F);
                MediaReference reference = VideoManager.Instance.GetVideo(characterData.twistAssets.PropAgAndaVideoID);
                //根据星级来进行开启不同的流程状态
                BK_Video.gameObject.SetActive(true);
                BK_Video.OpenMedia(reference);
                BK_Video.Play();
                yield return new WaitForSeconds(0.5f);
                
                float Videotime = Convert.ToSingle(BK_Video.Info.GetDuration());
                yield return new WaitForSeconds(Videotime-Settings.TwistTweenTime);//加上偏移时间
                MediaReference VideoAssets = VideoManager.Instance.GetVideo(characterData.twistAssets.VideoID);
                BK_Video.OpenMedia(VideoAssets);
                BK_Video.Play();
                
                Name_3Star.gameObject.SetActive(true);
                Name_3Image.sprite = characterData.twistAssets.NameImage;
                Name_3Star.Play();
                yield return new WaitForSeconds(Name_3Star.clip.length);
                
                //TODO: 测试代码协程
                yield return new WaitForSeconds(5);
                Name_3Star.gameObject.SetActive(false);
                Name_3Star.Stop();
                Debug.Log("三阶动画结束");
            }
            yield return new WaitForSeconds(1.25f);
            BK_Video.gameObject.SetActive(false);
            RewordTistPanel.gameObject.SetActive(true);
            TwistContent.gameObject.SetActive(false);
            BK_Video.Stop();
            
            UIHelper.Clear(HeadContent);
            for (int i = 0; i < TwistAmount; i++)
            {
               HeadFx fx =  UISystem.Instance.InstanceUI<HeadFx>("HeadFx",HeadContent);
               fx.InitData(characterList[i]);
               yield return new WaitForSeconds(0.25f);
            }
            CorotineBtns.gameObject.SetActive(true);
            Debug.Log("四阶动画结束");
            
        }
    }
}

