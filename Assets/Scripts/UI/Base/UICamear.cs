using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// UI摄像机渲染器
    /// </summary>
    public class UICamear : MonoSingleton<UICamear>
    {
        private Camera _UICamera;
        protected override void Awake()
        {
            base.Awake();
            _UICamera = GetComponent<Camera>();
        }

        public Camera GetUICamera => _UICamera;
    }
}

