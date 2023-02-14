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
using UnityEngine.Video;
using Random = System.Random;

namespace ARPG
{
    public class TwistScene : UIBase
    {
        private Image FadeImage;
        private UGUIVideoPlay MediaPlayer;
        private GameObject RewordTistPanel;
        private RectTransform TwistContent;
        private int TwistAmount;
        private TwistDouble currentdata;
        private TwisType _type;

        private Animation SwitchTwistAnim;

        private Animation ShowPanAnim;
        private Image ShowIcon;

        private UGUIVideoPlay BK_Video;
        private Animation Name_3Star;
        private Image Name_3Image;


        private RectTransform HeadContent;
        private GameObject CorotineBtns;

        private bool isSkip;
        private Button CloseBtn;
        private Button CorotineBtn;
        
        //-----2星一下角色------//
        private Animation ShowEf;
        private Image BK;
        private Button Blur;
        private Animation Name2Image;
        private Animation Name1Image;
        
        
        public override void Init()
        {
            FadeImage = Get<Image>("UIMask/Fade");
            MediaPlayer = Get<UGUIVideoPlay>("UIMask/Viedo");
            MediaPlayer.Init();
            RewordTistPanel = Get("UIMask/RewordTwist");
            TwistContent = Get<RectTransform>("UIMask/RewordTwist/Gird2_Bk/gird2");
            SwitchTwistAnim = Get<Animation>("UIMask/Tewwn/SwitchTwistAgg");
            ShowPanAnim = Get<Animation>("UIMask/Tewwn/show/show");
            ShowIcon = Get<Image>("UIMask/Tewwn/show/show/Card_mask");
            BK_Video = Get<UGUIVideoPlay>("UIMask/Tewwn/BK_Video");
            BK_Video.Init();
            Name_3Star = Get<Animation>("UIMask/Tewwn/BK_Video/name_3star");
            Name_3Image = Get<Image>("UIMask/Tewwn/BK_Video/name_3star/nnnn");
            HeadContent = Get<RectTransform>("UIMask/RewordTwist/HeadContent");
            CorotineBtns = Get("UIMask/RewordTwist/CorotineBtn");
            CloseBtn = Get<Button>("UIMask/RewordTwist/CorotineBtn/CloseBtn");
            CorotineBtn = Get<Button>("UIMask/RewordTwist/CorotineBtn/FuncBtn");
            ShowEf = Get<Animation>("UIMask/Tewwn/show_ef");
            BK = Get<Image>("UIMask/Tewwn/show_ef/BK");
            Blur = Get<Button>("UIMask/Tewwn/show_ef/Blur");
            Name2Image = Get<Animation>("UIMask/Tewwn/show_ef/name_2star");
            Name1Image = Get<Animation>("UIMask/Tewwn/show_ef/name_1star");
            
            
            Bind(CloseBtn,Close,UiAudioID.OutChick);
            Bind(CorotineBtn,CorotineTwist,UiAudioID.UI_click);
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
            UIHelper.Clear(HeadContent);
            SwitchTwistAnim.gameObject.SetActive(false);
            SwitchTwistAnim.Stop();
            ShowPanAnim.gameObject.SetActive(false);
            ShowPanAnim.Stop();
            CorotineBtns.gameObject.SetActive(false);
            RewordTistPanel.gameObject.SetActive(false);
            ShowEf.gameObject.SetActive(false);
        }

        public IEnumerator OpenTwis(int amount)
        {
            yield return FadeImage.DOFade(1, 0.25f);
            MediaPlayer.gameObject.SetActive(true);
            VideoClip clip = _type == TwisType.PILCK_UP
                ? VideoManager.Instance.Get("Twist")
                : VideoManager.Instance.Get("TwistSp");
            
            MediaPlayer.StarPlay(clip,true);
            FadeImage.DOFade(0,0.1F);
            AudioManager.Instance.PlayAudio(amount < 10?"TwistOne":"TwistTen");
            yield return new WaitForSeconds(Convert.ToSingle(clip.length));
            MediaPlayer.Close();
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
                    CharacterStarType.二星 => "StarType_Two",
                    CharacterStarType.三星 => "StarType_Three",
                    _ => "StarType_Two"
                };
                ShowIcon.sprite = GameSystem.Instance.GetSprite(SpriteID);
                ShowPanAnim.gameObject.SetActive(true);
                ShowPanAnim.Play();
                Debug.Log("二阶动画时长 : "+ShowPanAnim.clip.length);
                yield return new WaitForSeconds(ShowPanAnim.clip.length-1.2F);
                VideoClip reference = VideoManager.Instance.Get(characterData.twistAssets.PropAgAndaVideoID);
                VideoClip VideoAssets = VideoManager.Instance.Get(characterData.twistAssets.VideoID);
                //根据星级来进行开启不同的流程状态
                if (characterData.CharacterStarType == CharacterStarType.三星)
                {
                    ShowEf.gameObject.SetActive(false);
                    BK_Video.gameObject.SetActive(true);
                    BK_Video.Play(reference);
                    yield return new WaitForSeconds(Convert.ToSingle(reference.length));
                    BK_Video.Play(VideoAssets);
                    Name_3Star.gameObject.SetActive(true);
                    Name_3Image.sprite = characterData.twistAssets.NameImage;
                    Name_3Star.Play();
                    yield return new WaitForSeconds(Name_3Star.clip.length);
                
                    //等待完成点击相应操作
                    yield return WaitSkip(BK_Video.GetComponent<Button>());
                    Name_3Star.gameObject.SetActive(false);
                    Name_3Star.Stop();
                    Debug.Log("三阶动画结束");
                }
                else
                {
                    BK_Video.Close();
                    Name1Image.gameObject.SetActive(false);
                    Name1Image.gameObject.SetActive(false);
                    ShowEf.gameObject.SetActive(true);
                    ShowEf.Play();
                    BK.sprite = characterData.twistAssets.BKImage;
                    Blur.GetComponent<Image>().sprite = characterData.twistAssets.BKImage;
                    yield return new WaitForSeconds(0.1f);
                    if (characterData.CharacterStarType == CharacterStarType.二星)
                    {
                        Name1Image.gameObject.SetActive(false);
                        Name2Image.transform.Find("name").GetComponent<Image>().sprite =
                            characterData.twistAssets.NameImage;
                        Name2Image.gameObject.SetActive(true);
                        Name2Image.Play();
                        yield return new WaitForSeconds(Name2Image.clip.length);

                        yield return WaitSkip(Blur);
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.5f);
                        Name1Image.gameObject.SetActive(false);
                        Name1Image.transform.Find("name").GetComponent<Image>().sprite =
                            characterData.twistAssets.NameImage;
                        Name1Image.gameObject.SetActive(true);
                        Name1Image.Play();
                        yield return new WaitForSeconds(Name1Image.clip.length);

                        yield return WaitSkip(Blur);
                    }
                }


            }
            BK_Video.Close();
            RewordTistPanel.gameObject.SetActive(true);
            TwistContent.gameObject.SetActive(false);
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


        public IEnumerator WaitSkip(Button SkipBtn)
        {
            isSkip = false;
            SkipBtn.onClick.RemoveAllListeners();
            SkipBtn.onClick.AddListener(()=>isSkip=true);
            while (!isSkip)
            {
                yield return null;
            }
        }

        public void CorotineTwist()
        {
            
        }
        



    }
}

