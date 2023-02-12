using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.BasePool;
using ARPG.Config;
using RenderHeads.Media.AVProVideo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class UpTwisPanel : UIBase
    {
        private GameObject UIMask;
        private TextMeshProUGUI TitleName;
        private TextMeshProUGUI description;
        private Button HelpBtn;
        private Button SingleBtn; //限定单次
        private Text SingBtnAmount;
        
        private Button OneBtn; //单次
        private Text OneAmount;
        private Button TenBtn; //十连
        private Text TenAmount;

        private Button ExchangeBtn;//交换列表Btn
        private MediaPlayer VideoPlay; //--当前播放视频


        private TwistData CurrentInfo;
        private TwistDouble CurrentDouble;
        private Text GemsthoneAmount;
        
        private MediaPlayer Video;

        public override void Init()
        {
            UIMask = transform.GetChild(0).gameObject;
            Video = Get<MediaPlayer>("UIMask/VideoMask/VideoPlay");
            TitleName = Get<TextMeshProUGUI>("UIMask/Back/TitleString");
            description = Get<TextMeshProUGUI>("UIMask/Back/description");
            HelpBtn = Get<Button>("UIMask/Back/HelpBtn");
            Bind(HelpBtn,HelpOnClick,UiAudioID.OnChick);
            SingleBtn = Get<Button>("UIMask/Back/DayBtn");
            SingBtnAmount = Get<Text>("UIMask/Back/DayBtn/mask/icon/Amount");
            OneBtn = Get<Button>("UIMask/Back/OneBtn");
            OneAmount = Get<Text>("UIMask/Back/OneBtn/icon/Amount");
            TenBtn = Get<Button>("UIMask/Back/TenBtn");
            TenAmount = Get<Text>("UIMask/Back/TenBtn/icon/Amount");
            ExchangeBtn = Get<Button>("UIMask/Back/ExchangeBtn");
            GemsthoneAmount = Get<Text>("UIMask/Back/chiyoushu/fufei/Amount");
            Bind(SingleBtn,SingOnClick,UiAudioID.OnChick);
            Bind(OneBtn,OneClick,UiAudioID.OnChick);
            Bind(TenBtn,TenClick,UiAudioID.OnChick);
            Bind(ExchangeBtn,ExchangeOnClick,UiAudioID.OnChick);
            MessageAction.UpdataeMoney += SetMoneyUI;
        }

        public void InitData(TwistData data,TwistDouble Double)
        {
            CurrentInfo = data;
            TitleName.text = data.TitleString;
            description.text = data.description;
            SingBtnAmount.text = data.SinglentAmount.ToString();
            OneAmount.text = data.OneTwisAmount.ToString();
            TenAmount.text = data.TenTwisAmount.ToString();
            GemsthoneAmount.text = InventoryManager.Instance.GetItemBag(Settings.GemsthoneID).count.ToString();
            Video.OpenMedia(Double.Video);
            Video.Play();
        }

        /// <summary>
        /// 设置/刷新货币
        /// </summary>
        /// <param name="Gemsthone"></param>
        /// <param name="Mana"></param>
        public void SetMoneyUI(ItemBag Gemsthone, ItemBag Mana)
        {
            GemsthoneAmount.text = Gemsthone.count.ToString();
        }

        public override void Close()
        {
            UIMask.gameObject.SetActive(false);
        }

        public override void Open()
        {
            UIMask.gameObject.SetActive(true);
        }

        private void HelpOnClick()
        {
            if (CurrentInfo == null) return;
            UISystem.Instance.ShowPopWindows("交换",CurrentInfo.Helpdescription,"关闭");
        }

        private void ExchangeOnClick()
        {
            UISystem.Instance.ShowPopWindows("提示","暂未开放",null);
        }

        private void SingOnClick()
        {
            string des = "消耗" + CurrentInfo.SinglentAmount + "宝石,招募1次 确定吗?";
            UISystem.Instance.ShowPopConsume("附奖扭蛋",des,GoldType.宝石,CurrentInfo.SinglentAmount,
                "所持角色交换Pt","99999",1, delegate
                {
                    InventoryManager.Instance.DeleteItemBag(Settings.GemsthoneID,CurrentInfo.SinglentAmount);
                    OpenTwisScnen(1);
                });
        }

        private void OneClick()
        {
            string des = "消耗" + CurrentInfo.OneTwisAmount + "宝石,招募1次 确定吗?";
            UISystem.Instance.ShowPopConsume("附奖扭蛋",des,GoldType.宝石,CurrentInfo.OneTwisAmount,
                "所持角色交换Pt","99999",1,delegate
                {
                    InventoryManager.Instance.DeleteItemBag(Settings.GemsthoneID,CurrentInfo.OneTwisAmount);
                    OpenTwisScnen(1);
                });
        }

        private void TenClick()
        {
            string des = "消耗" + CurrentInfo.TenTwisAmount + "宝石,招募1次 确定吗?";
            UISystem.Instance.ShowPopConsume("附奖扭蛋",des,GoldType.宝石,CurrentInfo.TenTwisAmount,
                "所持角色交换Pt","99999",1,delegate
                {
                    InventoryManager.Instance.DeleteItemBag(Settings.GemsthoneID,CurrentInfo.TenTwisAmount);
                    OpenTwisScnen(10);
                });
        }

        /// <summary>
        /// 开始进入抽奖
        /// </summary>
        /// <param name="count"></param>
        private void OpenTwisScnen(int count)
        {
            Debug.LogError("开始进入扭蛋 : 次数为 "+count);
        }


        private void OnDestroy()
        {
            MessageAction.UpdataeMoney -= SetMoneyUI;
        }
    }
}

