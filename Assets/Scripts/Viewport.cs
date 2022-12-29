using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewport : Singleton<Viewport>
{
    private float MinX;
    private float MaxX;
    private float MinY;
    private float MaxY;

    private void Start()
    {
        if (Camera.main is { })
        {
            Vector2 leftButton = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
            Vector2 rightButton = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
            MinX = leftButton.x;
            MinY = leftButton.y;
            MaxX = rightButton.x;
            MaxY = rightButton.y;
        }
    }

    /// <summary>
    /// 将移动位置限制在窗口位置,不能超出
    /// </summary>
    /// <param name="pos">位置</param>
    /// <param name="PaddingX">偏移X</param>
    /// <param name="PaddingY">偏移Y</param>
    /// <returns></returns>
    public Vector3 PlayerMovenemt(Vector3 pos,float PaddingX,float PaddingY)
    {
        Vector3 Positon = Vector3.zero;
        Positon.x = Mathf.Clamp(pos.x, MinX + PaddingX, MaxX  - PaddingX);
        Positon.y = Mathf.Clamp(pos.y, MinY+PaddingY, MaxY - PaddingY);
        return Positon;
    }
}
