using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ARPG.GameSave;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class ArchiveItemUI : UIBase,IPointerDownHandler,IPointerUpHandler
    {
        private TextMeshProUGUI ManaAmount;
        private TextMeshProUGUI GemsthoneAmount;
        private TextMeshProUGUI DateTimeText;
        private Button LoadButton;
        private User currentUser;
        
        //IPointDown
        private float DownTime;
        private Coroutine addDownTimeCoroutine;
        
        public override void Init()
        {
            ManaAmount = Get<TextMeshProUGUI>("MoneyUI/Content/Mana/Amount");
            GemsthoneAmount = Get<TextMeshProUGUI>("MoneyUI/Content/Gemsthone/Amount");
            DateTimeText = Get<TextMeshProUGUI>("DownTime");
            LoadButton = GetComponent<Button>();
            Bind(LoadButton,LoadGameScnen,"OnChick");
        }

        public void InitData(User user)
        {
            currentUser = user;
            ManaAmount.text = user.ManaAmount.ToString();
            GemsthoneAmount.text = user.GemsthoneAmount.ToString();
            DateTimeText.text = user.SaveTime.ToString();
        }

        private void LoadGameScnen()
        {
            InventoryManager.Instance.SetCurrentUser(currentUser);
            SaveGameManager.Instance.Load(currentUser.UID);
            UISystem.Instance.CloseUI("ArchiveUI");
            MessageAction.OnTransitionEvent("GameScnen",Vector3.zero);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DownTime = 0;
            Action action = delegate
            {
                UISystem.Instance.ShowPopDialogue("删除记忆","确定要删除记忆吗,记忆删除后将不可进恢复","关闭","确定",null, delegate
                {
                    SaveGameManager.Instance.Delete(currentUser.UID);
                    ArchiveUI archiveUI = UISystem.Instance.GetUI<ArchiveUI>("ArchiveUI");
                    archiveUI.DeleteData(currentUser);
                });
            };
            addDownTimeCoroutine ??= StartCoroutine(AddDownTime(action));
        }

        private IEnumerator AddDownTime(Action action)
        {
            while (DownTime <= 0.75f)
            {
                DownTime += Time.deltaTime;
                yield return null;
            }
            action?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (addDownTimeCoroutine != null)
            {
                StopCoroutine(addDownTimeCoroutine);
                addDownTimeCoroutine = null;
                DownTime = 0;
            }
        }
    }
}

