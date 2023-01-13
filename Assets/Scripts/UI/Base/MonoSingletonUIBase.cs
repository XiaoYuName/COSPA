using System;
using ARPG;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 单例UIBase类脚本：
///         请注意,单利不可被UISystem脚本的Get之类方法所获取和调用
///         如果需要调用直接调用Instance方法取得该对象即可
/// </summary>
public class MonoSingletonUIBase : MonoSingleton<MonoSingletonUIBase>
{
    [HideInInspector] public bool isOpen;


    /// <summary>
    /// 通用UI打开方法,提供重写
    /// </summary>
    public virtual void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 通用UI关闭方法,提供重写
    /// </summary>
    public virtual void Close()
    {
        isOpen = false;
        gameObject.SetActive(false);
    }


    /// <summary>
    /// 获取子物体对象
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns></returns>
    protected GameObject Get(string path)
    {
        return transform.Find(path).gameObject;
    }

    /// <summary>
    /// 获取自身子物体组件
    /// </summary>
    /// <param name="path">路径</param>
    /// <typeparam name="T">组件</typeparam>
    /// <returns></returns>
    protected T Get<T>(string path) where T : Component
    {
        try
        {
            return transform.Find(path).GetComponent<T>();
        }
        catch (Exception)
        {
            Debug.LogError("Paht :" + path + "路径不存在");
            throw;
        }

    }

    /// <summary>
    /// 绑定一个Button 
    /// </summary>
    /// <param name="button">Button对象</param>
    /// <param name="func">绑定事件</param>
    /// <param name="audioname">Audio 音效名称</param>
    protected virtual void Bind(Button button, Action func, string audioname)
    {
        button.onClick.RemoveAllListeners();

        void UnityAction()
        {
            func?.Invoke();
            AudioManager.Instance.PlayAudio(audioname);
        }

        button.onClick.AddListener(UnityAction);
    }
}
