using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.UI.Config;
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
            NextBtn = Get<Button>("UIMask/BG/Next");
            LeftSpine = Get<SkeletonGraphic>("UIMask/BG/LeftDialogueSpine");
            RightSpine = Get<SkeletonGraphic>("UIMask/BG/RightDialogueSpine");
            NextBtn.onClick.AddListener(NextDialogue);
        }

    
        //TODO：删除测试代码
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.G))
            {
                StarPlayDialogueUI("Star1");
            }
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
            Play(data.Get(ID).Pieces[index]);
        }
        
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
        }


        private void NextDialogue()
        {
            if (!isDialogue || currentData == null) return;
            index++;
            if (index > currentData.Pieces.Count)
            {
                index = 0;
                currentData = null;
                Close();
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

