using ARPG.Config;
using TMPro;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class PlayerState : UIBase
    {
        private TextMeshProUGUI NameText;
        private Image Head;
        private Slider HP;
        private StarContent Content;
        private TextMeshProUGUI SliderText;
        private BuffStateUI StateUI;
        public override void Init()
        {
            NameText = Get<TextMeshProUGUI>("UIMask/Name");
            Head = Get<Image>("UIMask/Head");
            HP = Get<Slider>("UIMask/Slider");
            Content = Get<StarContent>("UIMask/StarContent");
            SliderText = Get<TextMeshProUGUI>("UIMask/Slider/SliderText");
            StateUI = Get<BuffStateUI>("UIMask/BUFFStateUI");
            StateUI.Init();
        }

        public void InitData(CharacterConfigInfo info,CharacterBag bag,CharacterState state)
        {
            if(!isOpen)
                Open();
            NameText.text = info.CharacterName;
            Head.sprite = info.Headicon;
            HP.maxValue = state.HP;
            HP.minValue = 0;
            HP.value = state.currentHp;
            Content.Init();
            Content.Show(bag.currentStar);
            SliderText.text = "HP: " + state.currentHp + "/" + state.HP;
        }

        public void UpdateState(CharacterState state)
        {
            HP.value = state.currentHp;
            SliderText.text = "HP: " + state.currentHp + "/" + state.HP;
        }

        public BuffStateUI GetBuffStateUI()
        {
            return StateUI;
        }

        public override void Close()
        {
            base.Close();
            StateUI.RemoveAll_UI();
        }
    }

}
