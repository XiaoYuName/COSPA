using System.Collections;
using System.Collections.Generic;
using ARPG.Pool;
using UnityEngine;

namespace ARPG.BasePool
{
    public class AvVideoPool : BasePool<UIAvVideoItem>
    {
        protected override void Awake()
        {
            base.Awake();
            Init();
        }
    }
}

