using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.UI
{
    /// <summary>
    /// 所有UI的抽象基类:
    ///     定义了初始化,打开,和关闭的函数方法,以便外部调用
    /// </summary>
    public abstract class  UIBase : MonoBehaviour
    {
        /// <summary>
        /// 初始化方法,一般不需要手动调用
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// 通用UI打开方法,提供重写
        /// </summary>
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 通用UI关闭方法,提供重写
        /// </summary>
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}

