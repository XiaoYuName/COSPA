using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.BasePool;
using ARPG.Config;
using ARPG.UI;
using DG.Tweening;
using RenderHeads.Media.AVProVideo;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = System.Random;

namespace ARPG
{
    public class TwistScene : UIBase
    {
        private Animator FadeImage;
        private UGUIVideoPlay MediaPlayer;
        private GameObject RewordTistPanel;
        private RectTransform TwistContent;
        private int TwistAmount;
        private TwistDouble currentdata;
        private TwisType _type;
        private TwistData _currentTwistData;
        private TwistMode _mode;

        private Animation SwitchTwistAnim;

        private Animation ShowPanAnim;
        private Animation ShowPanAnim_ef2;
        private Image ShowIcon;
        private Image ShowIcon_ef2;

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
        private SkeletonGraphic SkeletonGraphic;
        private ParticleSystem Boom_Fx;
        
        /// <summary>
        /// 是否正在扭蛋
        /// </summary>
        public bool isTwist;
        

        public override void Init()
        {
            FadeImage = Get<Animator>("UIMask/Fade");
            MediaPlayer = Get<UGUIVideoPlay>("UIMask/Viedo");
            MediaPlayer.Init();
            RewordTistPanel = Get("UIMask/RewordTwist");
            TwistContent = Get<RectTransform>("UIMask/RewordTwist/Gird2_Bk/gird2");
            SwitchTwistAnim = Get<Animation>("UIMask/Tewwn/SwitchTwistAgg");
            ShowPanAnim = Get<Animation>("UIMask/Tewwn/show/show");
            ShowPanAnim_ef2 = Get<Animation>("UIMask/Tewwn/show/show_ef_2");
            ShowIcon = Get<Image>("UIMask/Tewwn/show/show/Card_mask");
            ShowIcon_ef2 = Get<Image>("UIMask/Tewwn/show/show_ef_2/Card_mask");
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
            SkeletonGraphic = Get<SkeletonGraphic>("UIMask/Tewwn/show_ef/CharacterSpine");
            Boom_Fx = Get<ParticleSystem>("UIMask/Tewwn/show_ef/BOOM_Fx");
            Bind(CloseBtn,Close,UiAudioID.OutChick);
            Bind(CorotineBtn,CorotineTwist,UiAudioID.UI_click);
        }

        /// <summary>
        /// 开启扭蛋流程
        /// </summary>
        /// <param name="Amount">扭蛋次数</param>
        /// <param name="data">类型数据</param>
        /// <param name="twisType">扭蛋类型</param>
        public void OpenTwisScene(int Amount,TwistMode mode,TwistData CurrentInfo,TwistDouble data,TwisType twisType)
        {
            TwistAmount = Amount;
            _currentTwistData = CurrentInfo;
            currentdata = data;
            this._type = twisType;
            _mode = mode;
            Open();
            ResetIni();
            StartCoroutine(OpenTwis(Amount));
        }

        /// <summary>
        /// 重置函数
        /// </summary>
        public void ResetIni()
        {
            UIHelper.Clear(TwistContent);
            UIHelper.Clear(HeadContent);
            SwitchTwistAnim.gameObject.SetActive(false);
            SwitchTwistAnim.Stop();
            ShowPanAnim.gameObject.SetActive(false);
            ShowPanAnim_ef2.gameObject.SetActive(false);
            ShowPanAnim.Stop();
            CorotineBtns.gameObject.SetActive(false);
            RewordTistPanel.gameObject.SetActive(false);
            ShowEf.gameObject.SetActive(false);
            Name1Image.gameObject.SetActive(true);
            Name2Image.gameObject.SetActive(true);
            ShowIcon_ef2.gameObject.SetActive(true);
            ShowIcon.gameObject.SetActive(true);
        }

        private IEnumerator OpenTwis(int amount)
        {
            if (isTwist) yield break;
            isTwist = true;
            FadeImage.SetTrigger("Fade");
            MediaPlayer.gameObject.SetActive(true);
            VideoClip clip = _type == TwisType.PILCK_UP
                ? VideoManager.Instance.Get("Twist")
                : VideoManager.Instance.Get("TwistSp");
            MediaPlayer.StarPlay(clip,true);
            AudioManager.Instance.PlayAudio("TwistTween");
            AudioManager.Instance.PlayAudio(amount < 10?"TwistOne":"TwistTen");
            yield return new WaitForSeconds(Convert.ToSingle(clip.length));
            MediaPlayer.Close();
            StartCoroutine(CreatRewordScnen());
            
        }
        
        private IEnumerator CreatRewordScnen()
        {
            RewordTistPanel.gameObject.SetActive(true);
            TwistContent.gameObject.SetActive(true);
            List<string> characterList = new List<string>();
            for (int i = 0; i < TwistAmount; i++)
            {
                if (_type == TwisType.PILCK_UP)
                {
                    Random random = new Random();
                    double value= random.NextDouble();
                    if (value <= currentdata.UpDouble)
                    {
                        CardFx Fx=  UISystem.Instance.InstanceUI<CardFx>("CardFx",TwistContent);
                        Fx.IniData(3);
                        characterList.Add(currentdata.characterID[UnityEngine.Random.Range(0,currentdata.characterID.Count)]);
                        yield return new WaitForSeconds(0.25f);
                        continue;
                    }
                    value= random.NextDouble();
                    if (value <= currentdata.CharacterDouble)
                    {
                        CardFx Fx=  UISystem.Instance.InstanceUI<CardFx>("CardFx",TwistContent);
                        Fx.IniData(2);
                        characterList.Add(currentdata.CharacterCradsID[UnityEngine.Random.Range(0,currentdata.CharacterCradsID.Count)]);
                        yield return new WaitForSeconds(0.25f);
                        continue;
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
                AudioManager.Instance.PlayAudio("Twist_Loading");
                yield return new WaitForSeconds(SwitchTwistAnim.clip.length-0.35f);
                SwitchTwistAnim.gameObject.SetActive(false);
                SwitchTwistAnim.Stop();
                
                Debug.Log("Tween动画一阶段结束");
                CharacterConfigInfo characterData = InventoryManager.Instance.GetCharacter(characterList[i]);
                string SpriteID = characterData.CharacterStarType switch
                {
                    CharacterStarType.一星 => "StarType_One",
                    CharacterStarType.二星 => "StarType_Two",
                    CharacterStarType.三星 => "StarType_Three",
                    _ => "StarType_Two"
                };
                //根据星级来进行开启不同的流程状态
                if (characterData.CharacterStarType == CharacterStarType.三星)
                {
                    ShowPanAnim_ef2.gameObject.SetActive(false);
                    ShowIcon.sprite = GameSystem.Instance.GetSprite(SpriteID);
                    ShowPanAnim.gameObject.SetActive(true);
                    ShowPanAnim.Play();
                    Debug.Log("二阶动画时长 : "+ShowPanAnim.clip.length);
                    yield return new WaitForSeconds(ShowPanAnim.clip.length-1.2F);
                    VideoClip reference = VideoManager.Instance.Get(characterData.twistAssets.PropAgAndaVideoID);
                    VideoClip VideoAssets = VideoManager.Instance.Get(characterData.twistAssets.VideoID);
                    //-----------------------------------------------------------------------------------------------//
                    ShowEf.gameObject.SetActive(false);
                    BK_Video.gameObject.SetActive(true);
                    BK_Video.Play(reference,false);
                    AudioManager.Instance.PlayAudio(characterData.twistAssets.AudioHeadID);
                    yield return new WaitForSeconds(Convert.ToSingle(reference.length));
                    BK_Video.Play(VideoAssets,true);
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
                    ShowPanAnim.gameObject.SetActive(false);
                    ShowIcon_ef2.sprite = GameSystem.Instance.GetSprite(SpriteID);
                    ShowPanAnim_ef2.gameObject.SetActive(true);
                    ShowPanAnim_ef2.Play();
                    yield return new WaitForSeconds(1.2F);
                    //TODO: 增加一个粒子特效
                    Boom_Fx.gameObject.SetActive(true);
                    Boom_Fx.Play();
                    AudioManager.Instance.PlayAudio("ShowPanAnim");
                    SkeletonGraphic.skeletonDataAsset = characterData.TwistSpine;
                    SkeletonGraphic.initialSkinName = characterData.twistAssets.SpineSkinName.ToString();
                    SkeletonGraphic.Initialize(true);
                    SkeletonGraphic.AnimationState.SetAnimation(0, characterData.twistAssets
                        .SpineAnimationName.ToString(), true);
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
                        Name2Image.gameObject.SetActive(false);
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

            #region 关闭动画播放时的
            Name1Image.gameObject.SetActive(false);
            Name2Image.gameObject.SetActive(false);
            ShowEf.gameObject.SetActive(false);
            ShowIcon_ef2.gameObject.SetActive(false);
            ShowPanAnim_ef2.gameObject.SetActive(false);
            RewordTistPanel.gameObject.SetActive(true);
            TwistContent.gameObject.SetActive(false);
            #endregion
     
            
            UIHelper.Clear(HeadContent);
            for (int i = 0; i < TwistAmount; i++)
            {
                HeadFx fx =  UISystem.Instance.InstanceUI<HeadFx>("HeadFx",HeadContent);
                fx.InitData(characterList[i]);
                
                CharacterBag characterBag = InventoryManager.Instance.GetCharacterBag(characterList[i]);
                if (characterBag != null)
                {
                    InventoryManager.Instance.AddItem(new ItemBag(){ID = "90005",count = 10,power = 0});
                }
                else
                {
                    //添加该角色到背包
                    CharacterConfigInfo info = InventoryManager.Instance.GetCharacter(characterList[i]);
                    CharacterBag AddBag = new CharacterBag
                    {
                        currentStar = (int) info.CharacterStarType,
                        exp = 0,
                        ID = characterList[i]
                    };
                    InventoryManager.Instance.AddCharacter(AddBag);
                }

                yield return new WaitForSeconds(0.25f);
            }
            CorotineBtns.gameObject.SetActive(true);
            isTwist = false;
            Debug.Log("四阶动画结束");
        }

        /// <summary>
        /// 等待点击反馈再执行下一步
        /// </summary>
        /// <param name="SkipBtn">等待点击按钮的Button 组件</param>
        /// <returns></returns>
        private IEnumerator WaitSkip(Button SkipBtn)
        {
            isSkip = false; ;
            SkipBtn.onClick.RemoveAllListeners();
            SkipBtn.onClick.AddListener(()=>isSkip=true);
            while (!isSkip)
            {
                yield return null;
            }
        }

        public void CorotineTwist()
        {
            ItemBag GemsthoneBag = InventoryManager.Instance.GetItemBag(Settings.GemsthoneID);
            int Amount = _mode switch
            {
                TwistMode.限定一次 => _currentTwistData.SinglentAmount,
                TwistMode.单次 => _currentTwistData.OneTwisAmount,
                TwistMode.十连 => _currentTwistData.TenTwisAmount,
                _ => 99999,
            };
            if (GemsthoneBag.count - Amount < 0)
            {
                Close();
                UISystem.Instance.ShowPopWindows("提示","您的宝石不足,请补充后再次扭蛋吧!","确定");
                return;
            }
            InventoryManager.Instance.DeleteItemBag(GemsthoneBag.ID,Amount);
            OpenTwisScene(TwistAmount,_mode,_currentTwistData,currentdata, _type);
        }
        



    }
}

