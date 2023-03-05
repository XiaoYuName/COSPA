using ARPG;
using ARPG.UI;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegionItemUI : UIBase
{
    private Image icon;
    private StarContent StarContent;
    private TextMeshProUGUI MapIndex;
    private GameObject RegionMode;
    private Button OnClick;

    private RegionLine _regionLine;
    private RegionItem _RegionItem;
    private LookState _lookState;

    public override void Init()
    {
        icon = Get<Image>("Back/icon");
        StarContent = Get<StarContent>("Back/StarContent");
        MapIndex = Get<TextMeshProUGUI>("Back/MapIndex");
        RegionMode = Get("Back/NotType");
        OnClick = GetComponent<Button>();
        Bind(OnClick,OnClikc,UiAudioID.UI_click);
    }

    public void InitData(RegionLine regionLine,RegionItem data)
    {
        _regionLine = regionLine;
        _RegionItem = data;
        icon.sprite = data.backIcon;
        MapIndex.text = data.RegionItemName;
        var Save = InventoryManager.Instance.GetRegionData(_regionLine.RegionName, data.RegionItemName);
        if (Save == null)
        {
            StarContent.Show(0);
            _lookState = LookState.未开启;
            MapIndex.text = "???";
            return;
        }
        StarContent.Show(Save.Star);
        _lookState = Save.State;
        RegionMode.gameObject.SetActive(Save.State == LookState.未开启);
    }

    public void SetNotData()
    {
        StarContent.Show(0);
        _lookState = LookState.未开启;
        MapIndex.text = "???";
        _RegionItem = null;
    }

    public void SetUpState()
    {
        if (_regionLine == null || _RegionItem == null)
        {
            SetNotData();
            return;
        }
        var Save = InventoryManager.Instance.GetRegionData(_regionLine.RegionName, _RegionItem.RegionItemName);
        if (Save == null)
        {
            StarContent.Show(0);
            _lookState = LookState.未开启;
            MapIndex.text = "???";
            return;
        }
        StarContent.Show(Save.Star);
        _lookState = Save.State;
        RegionMode.gameObject.SetActive(Save.State == LookState.未开启);
    }


    private void OnClikc()
    {
        if (_regionLine == null || _RegionItem == null)
        {
            UISystem.Instance.ShowTips("暂未开放");
            return;
        }
        if (_lookState == LookState.未开启)
        {
            UISystem.Instance.ShowTips("请通过上个关卡");
            return;
        }
        MainPanel.Instance.AddTbaleChild("RegionToolTip");
        UISystem.Instance.OpenUI<RegionToolTip>("RegionToolTip",(ui)=>
            ui.InitData(_regionLine,_RegionItem));
    }


}
