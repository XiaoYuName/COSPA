
using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class OverrideCanvas : MonoBehaviour
    {
        [SortingLayer]
        public string SortingLayer;

        public int OrderinLayer;
        private Canvas Canvas;

        private void Awake()
        {
            Canvas = GetComponent<Canvas>();
            Canvas.overrideSorting = true;
            Canvas.sortingLayerName = SortingLayer;
            Canvas.sortingOrder = OrderinLayer;
        }
    }
}

