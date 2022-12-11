using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池设计
/// </summary>
[System.Serializable]
public class Pool
{
    public GameObject Prefab => prefab;
    /// <summary>
    /// 对象预制体
    /// </summary>
    [SerializeField]
    public GameObject prefab;
    /// <summary>
    /// 数量
    /// </summary>
    [SerializeField]
    public int count = 1;

    private Transform parent;
    
    /// <summary>
    /// 队列
    /// </summary>
    private Queue<GameObject> _queue;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(Transform Parent)
    {
        _queue = new Queue<GameObject>(); //实例化队列
        this.parent = Parent;
        for (int i = 0; i < count; i++)
        {
            _queue.Enqueue(Copy()); // 循环添加池对象
        }
    }

    /// <summary>
    /// 复制Prefab 对象
    /// </summary>
    /// <returns></returns>
    private GameObject Copy()
    {
       var temp = Object.Instantiate(prefab,parent);
       temp.SetActive(false);
       return temp;
    }

    /// <summary>
    /// 获取一个可用对象
    /// </summary>
    /// <returns></returns>
    private GameObject AvailableObject()
    {
        GameObject temp = null;

        if (_queue.Count > 0 && !_queue.Peek().activeSelf)  //如果队列为空,则创建一个
        {
            temp = _queue.Dequeue();
        }
        else
        {
            temp = Copy();
        }

        _queue.Enqueue(temp);
        return temp;
    }

    /// <summary>
    /// 启用一个对象
    /// </summary>
    /// <returns></returns>
    public GameObject PreParedObject()
    {
        GameObject temp = AvailableObject();
        temp.SetActive(true);
        return temp;
    }
    /// <summary>
    /// 启用一个对象
    /// </summary>
    /// <param name="position">世界空间位置</param>
    /// <returns></returns>
    public GameObject PreParedObject(Vector3 position)
    {
        GameObject temp = AvailableObject();
        temp.transform.position = position;
        temp.SetActive(true);
        return temp;
    }
    /// <summary>
    /// 启用一个对象
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="rotation">旋转(四元数)</param>
    /// <returns></returns>
    public GameObject PreParedObject(Vector3 position,Quaternion rotation)
    {
        GameObject temp = AvailableObject();
        temp.transform.position = position;
        temp.transform.rotation = rotation;
        temp.SetActive(true);
        return temp;
    }
    /// <summary>
    /// 启用一个对象
    /// </summary>
    /// <param name="position">设置位置</param>
    /// <param name="rotation">设置旋转</param>
    /// <param name="Scale">设置缩放</param>
    /// <returns></returns>
    public GameObject PreParedObject(Vector3 position,Quaternion rotation,Vector3 Scale)
    {
        GameObject temp = AvailableObject();
        temp.transform.position = position;
        temp.transform.rotation = rotation;
        temp.transform.localScale = Scale;
        temp.SetActive(true);
        return temp;
    }
    /// <summary>
    /// 启用一个对象
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="rotation">旋转</param>
    /// <param name="parent">父级</param>
    /// <returns></returns>
    public GameObject PreParedObject(Vector3 position,Quaternion rotation,Transform parent)
    {
        GameObject temp = AvailableObject();
        temp.transform.position = position;
        temp.transform.rotation = rotation;
        temp.transform.parent = parent;
        temp.SetActive(true);
        return temp;
    }
    /// <summary>
    /// 启用一个对象
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="rotation">旋转</param>
    /// <param name="Scale">缩放</param>
    /// <param name="parent">父级</param>
    /// <returns></returns>
    public GameObject PreParedObject(Vector3 position,Quaternion rotation,Vector3 Scale,Transform parent)
    {
        GameObject temp = AvailableObject();
        temp.transform.position = position;
        temp.transform.rotation = rotation;
        temp.transform.localScale = Scale;
        temp.transform.parent = parent;
        temp.SetActive(true);
        return temp;
    }


}
