using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ARPG.BasePool
{
    /// <summary>
    /// 泛型单例对象池
    /// </summary>
    public class BasePool<T> : Singleton<BasePool<T>> where  T: Component 
    {
        private ObjectPool<T> Pool;
        [SerializeField,Header("预制体名称")]
        protected T Prefab;
        [SerializeField,Header("对象池默认容量")]
        private int defaultCapactity;
        [SerializeField,Header("对象池最大容量")]
        private int maxSize;
        
        public int ActiveCount => Pool.CountActive;

        public int InactiveCount => Pool.CountInactive;

        public int TotalCount => Pool.CountAll;
        
        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="collectionChekc">是否开启自动检测，默认开启</param>
        protected void Init(bool collectionChekc = true)=> Pool = new ObjectPool<T>(CreatVideo, OnGetVideoItem, OnReleaseVideoItem, OnDestoryVideoItem, collectionChekc,
            10,100);

        /// <summary>
        /// 创建对象元素所执行方法
        /// </summary>
        /// <returns></returns>
        protected virtual  T CreatVideo()
        {
            return Instantiate(Prefab, transform);
        }

        /// <summary>
        /// 表示调用Get函数时所调用的函数
        /// </summary>
        /// <param name="Obj"></param>
        protected virtual void OnGetVideoItem(T Obj)
        {
            Obj.gameObject.SetActive(true);
        }

        /// <summary>
        /// 表示回收对象池所需调用函数
        /// </summary>
        /// <param name="Obj"></param>
        protected virtual void OnReleaseVideoItem(T Obj)
        {
            Obj.gameObject.SetActive(false);
        }

        /// <summary>
        /// 表示销毁对象池对象所调用函数
        /// </summary>
        /// <param name="Obj"></param>
        protected virtual void OnDestoryVideoItem(T Obj)
        {
            Destroy(Obj.gameObject);
        }

        /// <summary>
        /// 获取对象池数据
        /// </summary>
        /// <returns></returns>
        public T Get() => Pool.Get();

        /// <summary>
        /// 释放对象池数据
        /// </summary>
        /// <param name="Obj"></param>
        public void Release(T Obj) => Pool.Release(Obj);

        /// <summary>
        /// 清空对象池数据
        /// </summary>
        public void Clear() => Pool.Clear();

        /// <summary>
        /// 设置预制体
        /// </summary>
        /// <param name="t"></param>
        public void SetPrefab(T t)
        {
            Prefab = t;
        }

        /// <summary>
        /// 设置预制体：
        ///     根据名称：请注意该预制体必须在UISystem 的PrefabConfig 配置表中
        /// </summary>
        /// <param name="Name">预制体表内名称</param>
        public void SetPrefab(string Name)
        {
            Prefab = UISystem.Instance.GetPrefab<T>(Name);
        }

    }
}

