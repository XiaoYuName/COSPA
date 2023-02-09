using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
using UnityEngine;

public class SetUICamear : MonoBehaviour
{
    private Canvas Canvas;
    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
        Canvas.worldCamera = UICamear.Instance.GetUICamera;
    }
}
