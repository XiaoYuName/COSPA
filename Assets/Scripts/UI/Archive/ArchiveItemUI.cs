using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ARPG.GameSave;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class ArchiveItemUI : UIBase
    {
        private TextMeshProUGUI ManaAmount;
        private TextMeshProUGUI GemsthoneAmount;
        private TextMeshProUGUI DateTimeText;
        private Button LoadButton;
        private User currentUser;
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
            ManaAmount.text = user.MonaAmount.ToString();
            GemsthoneAmount.text = user.GemsthoneAmount.ToString();
            DateTimeText.text = user.SaveTime.ToString(CultureInfo.InvariantCulture);
        }

        private void LoadGameScnen()
        {
            SaveGameManager.Instance.Load(currentUser.UID);
            InventoryManager.Instance.SetCurrentUser(currentUser);
            MessageAction.OnTransitionEvent("GameScnen",Vector3.zero);
        }
    }
}

