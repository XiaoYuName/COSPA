using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ARPG;
using ARPG.Pool;
using ARPG.UI;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
   private Pool[] Pools;
   private List<Pool> ConfigPools = new List<Pool>();
   private PoolConfig _poolConfig;

   /// <summary>
   /// 对象池字典
   /// </summary>
   private static Dictionary<GameObject, Pool> _dictionary;
   private void Start()
   {
      InitPoolsItem();
      Init(Pools);
   }

   private void InitPoolsItem()
   {
      _poolConfig = PoolConfig.GetConfig<PoolConfig>("Pool/PoolConfig");
      foreach (var poolItem in _poolConfig.BaseDatas)
      {
         Pool pool = new Pool();
         pool.prefab = UISystem.Instance.GetPrefab(poolItem.ID);
         pool.count = poolItem.InitAmount;
         ConfigPools.Add(pool);
      }
      Pools = ConfigPools.ToArray();
   }


   private void Init(Pool[] pools)
   {
      _dictionary = new Dictionary<GameObject, Pool>();
      foreach (var item in pools)
      {
         #if UNITY_EDITOR  //预编译条件判断,
         if (_dictionary.ContainsKey(item.Prefab))
         {
            Debug.LogError("重复预制体,请检查队列中的Prefab :" + item.Prefab.name);
            continue;
         } //如果有相同的键,则跳过这个循环
         #endif
         _dictionary.Add(item.Prefab,item);
         Transform go = new GameObject("Pool :" + item.Prefab.name).transform;
         go.parent = transform;
         item.Init(go);
      }
   }

   /// <summary>
   /// 释放一个对象
   /// </summary>
   /// <param name="prefab"></param>
   /// <returns></returns>
   public static GameObject Release(GameObject prefab)
   {
#if UNITY_EDITOR
      if (!_dictionary.ContainsKey(prefab))
      {
         Debug.LogError("字典中没有该对象的池,请检查");
      }
#endif
      return _dictionary[prefab].PreParedObject();
   }
   /// <summary>
   /// 根据预制体释放一个对象(创建)
   /// </summary>
   /// <param name="prefab">预制体</param>
   /// <param name="Position">位置</param>
   /// <returns></returns>
   public static GameObject Release(GameObject prefab,Vector3 Position)
   {
      #if UNITY_EDITOR
      if (!_dictionary.ContainsKey(prefab))
      {
         Debug.LogError("字典中没有该对象的池,请检查");
      }
      #endif
      return _dictionary[prefab].PreParedObject(Position);;
   }
   /// <summary>
   /// 根据预制体释放一个对象(创建)
   /// </summary>
   /// <param name="prefab">预制体</param>
   /// <param name="Position">位置</param>
   /// <param name="rotation">旋转</param>
   /// <returns></returns>
   public static GameObject Release(GameObject prefab,Vector3 Position,Quaternion rotation)
   {
      #if UNITY_EDITOR
      if (!_dictionary.ContainsKey(prefab))
      {
         Debug.LogError("字典中没有该对象的池,请检查");
      }
      #endif

      return  _dictionary[prefab].PreParedObject(Position,rotation);
   }
   /// <summary>
   /// 根据预制体释放一个对象(创建)
   /// </summary>
   /// <param name="prefab">预制体</param>
   /// <param name="Position">位置</param>
   /// <param name="rotation">旋转</param>
   /// <param name="Scale">缩放</param>
   /// <returns></returns>
   public static GameObject Release(GameObject prefab,Vector3 Position,Quaternion rotation,Vector3 Scale)
   {
      #if UNITY_EDITOR
      if (!_dictionary.ContainsKey(prefab))
      {
         Debug.LogError("字典中没有该对象的池,请检查");
      }
      #endif
      return  _dictionary[prefab].PreParedObject(Position,rotation,Scale);
   }
   /// <summary>
   /// 根据预制体释放一个对象(创建)
   /// </summary>
   /// <param name="prefab">预制体</param>
   /// <param name="Position">位置</param>
   /// <param name="rotation">旋转</param>
   /// <param name="Scale">缩放</param>
   /// <param name="parent">父级</param>
   /// <returns></returns>
   public static GameObject Release(GameObject prefab,Vector3 Position,Quaternion rotation,Vector3 Scale,Transform parent)
   {
      #if UNITY_EDITOR
      if (!_dictionary.ContainsKey(prefab))
      {
         Debug.LogError("字典中没有该对象的池,请检查");
      }
      #endif
      return  _dictionary[prefab].PreParedObject(Position,rotation,Scale,parent);
   }
}
