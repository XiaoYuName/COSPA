using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace ARPG.UI
{
    public class PropValue : UIBase
    {
        private TextMeshProUGUI State;
        private TextMeshProUGUI Value;
        public override void Init()
        {
            State = Get<TextMeshProUGUI>("Name/NameText");
            Value = Get<TextMeshProUGUI>("Value");
        }

        public void Show(string state, string value)
        {
            State.text = state;
            Value.text = value;
        }
    }

}
