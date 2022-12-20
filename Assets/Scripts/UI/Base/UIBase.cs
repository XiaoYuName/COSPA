using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        
        
        /// <summary>
        /// 获取子物体对象
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public GameObject Get(string path)
        {
            return transform.Find(path).gameObject;
        }

        /// <summary>
        /// 获取自身子物体组件
        /// </summary>
        /// <param name="path">路径</param>
        /// <typeparam name="T">组件</typeparam>
        /// <returns></returns>
        public T Get<T>(string path) where T: Component
        {
            try
            {
                return transform.Find(path).GetComponent<T>();
            }
            catch (Exception)
            {
                Debug.LogError("Paht :" +path + "路径不存在");
                throw;
            }
            
        }
        
        /// <summary>
        /// 绑定一个Button 
        /// </summary>
        /// <param name="button">Button对象</param>
        /// <param name="func">绑定事件</param>
        /// <param name="audioname">Audio 音效名称</param>
        public virtual void Bind(Button button, Action func,string audioname)
        {
            button.onClick.RemoveAllListeners();
            UnityAction action = () =>
            {
                func?.Invoke();
                AudioManager.Instance.PlayAudio(audioname);
            };
            button.onClick.AddListener(action);
        }
    }
}

