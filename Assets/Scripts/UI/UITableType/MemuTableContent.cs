using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.UI
{
    public class MemuTableContent : UIBase
    {
        [Header("TableType")]
        public MemuTableMode tableType;

        private RectTransform Content;
        
        public override void Init()
        {
           
        }

        /// <summary>
        /// 获取Item Content 对象
        /// </summary>
        /// <returns></returns>
        public RectTransform GetContent(string path)
        {
            Content = Get<RectTransform>(path);
            return Content;
        }

        /// <summary>
        /// 获取子物体对象
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetChild<T>(string path) where T: Component
        {
            T t = Get<T>(path);
            return t;
        }
    }

}
