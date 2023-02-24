using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.UI;
using UnityEngine;

public class SwitchTableUIContent : UIBase
{
    [Header("TableType")]
    public EquipTableType tableType;

    private RectTransform Content;
    
    public override void Init()
    {
        Content = Get<RectTransform>("Mask/Scroll Rect/Content");
    }

    /// <summary>
    /// 获取Item Content 对象
    /// </summary>
    /// <returns></returns>
    public RectTransform GetContent()
    {
        return Content;
    }
}
