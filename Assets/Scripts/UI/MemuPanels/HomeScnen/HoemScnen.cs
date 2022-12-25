using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using UnityEngine;

public class HoemScnen : UIBase
{
    private MoneyUI _moneyUI;
    public override void Init()
    {
        _moneyUI = Get<MoneyUI>("UIMask/MoneyUI");
        _moneyUI.Init();
    }
}
