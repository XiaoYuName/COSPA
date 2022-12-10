using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 配置表总管理器
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        /// 获取查找Config
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadConfig<T>(string name) where T : ScriptableObject
        {
            string path = name;
            T config = Resources.Load<T>(path);
            if (config == null)
            {
                Debug.LogError("配置表 : " + name + "在Resources 中找不到");
            }
            return config;
        }
        
    }
    
    /// <summary>
    /// 抽象Config类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Config<C> :ScriptableObject where C : ConfigData
    {
        public List<C> BaseDatas = new List<C>();
        
        /// <summary>
        /// 获取表中内容
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual C Get(string ID)
        {
            return BaseDatas.Find(t => t.ID == ID);
        }

        /// <summary>
        /// 获取配置表
        /// </summary>
        /// <param name="path">路径</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public static T GetConfig<T> (string path) where T:Config<C>
        {
            return ConfigManager.LoadConfig<T>(path);
        }
    }

    /// <summary>
    /// 抽象配置数据类,实现该接口必须返回该条数据的唯一ID值
    /// </summary>
    public abstract class ConfigData
    {
        public string ID;
        public virtual string Get()
        {
            return ID;
        }
    }
    
    
    /* 接口写法, 但是在Unity 编辑器内,由于不好看到内容,便没有采纳,
    public interface ConfigData
    {
        public string id { get; set; }
    }
    */
}

