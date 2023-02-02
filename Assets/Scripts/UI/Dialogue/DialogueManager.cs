using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ARPG.BasePool;
using ARPG.UI;
using UnityEngine;
using ARPG.UI.Config;
using RenderHeads.Media.AVProVideo;
using Spine.Unity;
using TMPro;
using UnityEngine.UI;

namespace ARPG
{
    public class DialogueManager : MonoSingletonUIBase
    {
        /// <summary>
        /// 对话数据配置表
        /// </summary>
        private DialogConfig data;

        #region Conmponent

        /// <summary>
        /// 对话者名字
        /// </summary>
        private TextMeshProUGUI dialogueName;

        /// <summary>
        /// 对话内容
        /// </summary>
        private TextMeshProUGUI description;

        /// <summary>
        /// 左对话者Icon
        /// </summary>
        private SkeletonGraphic LeftSpine;

        /// <summary>
        /// 右对话者Icon
        /// </summary>
        private SkeletonGraphic RightSpine;

        /// <summary>
        /// 下一条对话内容
        /// </summary>
        private Button NextBtn;

        /// <summary>
        /// 当前对话数据
        /// </summary>
        private DialogData currentData;

        /// <summary>
        /// 对话面板控制器
        /// </summary>
        private OptionContent OptionContent;

        /// <summary>
        /// AV Pro 视频播放器
        /// </summary>
        private MediaPlayer VideoContent;

        private Image BG;

        private string currentVideoID;

        #endregion

        /// <summary>
        /// 当前是否在对话中
        /// </summary>
        private bool isDialogue;

        /// <summary>
        /// 当前对话index;
        /// </summary>
        private int index;

        protected override void Awake()
        {
            base.Awake();
            data = ConfigManager.LoadConfig<DialogConfig>("Dialogue/DialogData");
            isDialogue = false;
            dialogueName = Get<TextMeshProUGUI>("UIMask/BG/TitleBG/Name");
            description = Get<TextMeshProUGUI>("UIMask/BG/description");
            NextBtn = Get<Button>("UIMask");
            OptionContent = Get<OptionContent>("Opention");
            OptionContent.Init();
            LeftSpine = Get<SkeletonGraphic>("UIMask/BG/LeftDialogueSpine");
            RightSpine = Get<SkeletonGraphic>("UIMask/BG/RightDialogueSpine");
            VideoContent = Get<MediaPlayer>("UIMask/AVPro Video");
            BG = Get<Image>("UIMask/MainBG");
            NextBtn.onClick.AddListener(NextDialogue);
        }

        
        /// <summary>
        /// 开启一条对话
        /// </summary>
        /// <param name="ID">对应配置表ID</param>
        public void StarPlayDialogueUI(string ID)
        {
            if (isDialogue) return;
            Open();
            index = 0;
            currentData = data.Get(ID);
            Play(data.Get(ID).Pieces[index]);
        }
        
        /// <summary>
        /// 开始显示对话
        /// </summary>
        /// <param name="Piece">对话信息</param>
        private void Play(DialogPiece Piece)
        {
            dialogueName.text = Piece.dialogName;
            description.text = Piece.dialogText;
            if (Piece.dialogueSpine == null && Piece.SpineAnimationName != SpineDialogueAnimation.Not)
            {
                LeftSpine.gameObject.SetActive(false);
                RightSpine.gameObject.SetActive(false);
            }
            else
            {
                if (Piece.isLeftSpine)
                {
                    LeftSpine.skeletonDataAsset = Piece.dialogueSpine;
                    LeftSpine.AnimationState.ClearTracks();
                    if(Piece.SpineSkinName != SpineDialogueSkin.not)
                        LeftSpine.initialSkinName = Piece.SpineSkinName.ToString();
                    LeftSpine.Initialize(true);
                    
                    LeftSpine.gameObject.SetActive(true);
                    LeftSpine.AnimationState.SetAnimation(0, Piece.SpineAnimationName.ToString(), Piece.isLoop);
                    RightSpine.gameObject.SetActive(false);
                }
                else
                {
                    RightSpine.skeletonDataAsset = Piece.dialogueSpine;
                    RightSpine.AnimationState.ClearTracks();
                    if(Piece.SpineSkinName != SpineDialogueSkin.not)
                        RightSpine.initialSkinName = Piece.SpineSkinName.ToString();
                    RightSpine.Initialize(true);
                    RightSpine.gameObject.SetActive(true);
                    RightSpine.AnimationState.SetAnimation(0, Piece.SpineAnimationName.ToString(), Piece.isLoop);
                    LeftSpine.gameObject.SetActive(false);
                }
            }
            //播放对应语音音效
            if (!String.IsNullOrEmpty(Piece.AudioID))
            {
                AudioManager.Instance.PlayAudio(Piece.AudioID);
            }

            if (Piece.isVideoOrBG)
            {
                if (!string.IsNullOrEmpty(Piece.VideoID))
                {
                    VideoContent.gameObject.SetActive(true);
                    BG.gameObject.SetActive(false);
                    if (Piece.isNewVideo && currentVideoID != Piece.VideoID)
                    {
                        currentVideoID = Piece.VideoID;
                        VideoContent.OpenMedia(VideoManager.Instance.GetVideo(Piece.VideoID));
                    }
                }else if (Piece.BG != null)
                {
                    VideoContent.gameObject.SetActive(false);
                    BG.gameObject.SetActive(true);

                    BG.sprite = Piece.BG;
                }
            }
            else
            {
                VideoContent.gameObject.SetActive(false);
                BG.gameObject.SetActive(false);
            }

            if (Piece.Options is {Count: > 0})
            {
                OptionContent.InitData(Piece.Options);
                OptionContent.Open();
            }
            else
            {
                OptionContent.Close();
            }
        }
        

        /// <summary>
        /// 继续下一条对话
        /// </summary>
        private void NextDialogue()
        {
            if (isDialogue || currentData == null) return;
            index++;
            if (index >= currentData.Pieces.Count)
            {
                index = 0;
                currentData = null;
                Close();
                OptionContent.Close();
                return;
            }
            Play(currentData.Pieces[index]);
        }

        /// <summary>
        /// 跳转到当前目标对话
        /// </summary>
        /// <param name="targetID"></param>
        public void ToTargetDialogue(int targetID)
        {
            if (isDialogue || currentData == null) return;
            if (targetID < 0) //表示没有下一个对话了,直接关闭该对话
            {
                index = 0;
                currentData = null;
                Close();
                OptionContent.Close();
                return;
            }

            if (currentData.Pieces.Any(a => a.dialogID == targetID))
            {
                //将index设置为该index
                for (int i = 0; i < currentData.Pieces.Count; i++)
                {
                    if (currentData.Pieces[i].dialogID == targetID)
                    {
                        index = i;
                        break;
                    }
                }
                DialogPiece piece =  currentData.Pieces.Find(a => a.dialogID == targetID);
                Play(piece);
            }
           
        }

        public override void Close()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        public override void Open()
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    } 
}

