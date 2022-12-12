using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Pool;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Video;

public class VideoPool : BasePool<UIVideoItem>
{
    protected override void Awake()
    {
        base.Awake();
        Init();
    }
}
