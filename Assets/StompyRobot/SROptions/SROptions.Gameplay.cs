using System;
using System.ComponentModel;
using ARPG;
using ARPG.Config;
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

    public string gmID;

    public int amount;

    public int pawor;
    
    [Category("AddItem"),DisplayName("物品ID")]
    public string GMID
    {
        get { return gmID;}
        set { gmID = value; }
    }

    [Category("AddItem"),NumberRange(1,99999),DisplayName("数量")]
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    [Category("AddItem"),NumberRange(1,99999),DisplayName("强化等级")]
    public int Pawor
    {
        get { return pawor; }
        set { pawor = value; }
    }

    [Category("AddItem"), DisplayName("添加物品到背包")]
    public void AddGMItem()
    {
        
        if (!String.IsNullOrEmpty(gmID)&& InventoryManager.Instance.isItem(gmID))
        {
            if (pawor < 0)
            {
                pawor = 0;
            }

            ItemBag Bag = new ItemBag()
            {
                ID = gmID,
                count = amount,
                power = pawor,
            };
            InventoryManager.Instance.AddItem(Bag);
        }
        else
        {
            Debug.Log("GM:没有该Item的定义");
        }
    }
    
    
    
}	
