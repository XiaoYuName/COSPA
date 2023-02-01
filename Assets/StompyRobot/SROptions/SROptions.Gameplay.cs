using System.ComponentModel;
using ARPG;
using UnityEngine;

public partial class SROptions
{

    [Category("作弊器"), DisplayName("设置拥有角色星级")]
    public void SetCurretnStar()
    {
        InventoryManager.Instance.SetAllCharacterStar(SetStar);
    }

    public int setstar;
    [Category("作弊器"),NumberRange(1,6),DisplayName("星级")]
    public int SetStar
    {
        get { return setstar;}
        set { setstar = value; }
    }


}	
