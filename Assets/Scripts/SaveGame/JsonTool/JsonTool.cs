using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class JsonTool
{
    /// <summary>
    /// 保存ScriptableObject 数据到本机磁盘
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="t">数据</param>
    /// <param name="DataName">保存的文件名</param>
    public static void SavaGame<T>(T t,string DataName) where T : ScriptableObject
    {

        string path = Application.persistentDataPath + "/Sava_GameData";
        //判断是否有该文件夹
        if (!Directory.Exists(path)) 
        {
            //如果没有 则创建一个文件夹
            Directory.CreateDirectory(path);
        }
        BinaryFormatter formatter = new BinaryFormatter(); //二进制转换

        FileStream file =  File.Create(path +"/"+ DataName); //创建文件   

        var json = JsonUtility.ToJson(t);  // 将ScriptableObject 转换未Json文件

        formatter.Serialize(file, json); //序列化Json 文件，转存到file 的文件中

        file.Close(); //关闭流读写器;
        Debug.Log("文件路径"+path);
    }
    
    /// <summary>
    /// 保存ScriptableObject 数据到本机磁盘
    /// </summary>
    /// <param name="t">数据类型</param>
    /// <param name="DataName">保存的文件名</param>
    /// <param name="persistentDataPath">保存路径</param>
    /// <typeparam name="T">T 必须继承自ScriptableObject</typeparam>
    public static void SavaGame<T>(T t,string DataName,string persistentDataPath) where T : ScriptableObject
    {

        string path = persistentDataPath + "/Sava_GameData";
        //判断是否有该文件夹
        if (!Directory.Exists(path)) 
        {
            //如果没有 则创建一个文件夹
            Directory.CreateDirectory(path);
        }
        BinaryFormatter formatter = new BinaryFormatter(); //二进制转换

        FileStream file =  File.Create(path +"/"+ DataName); //创建文件   

        var json = JsonUtility.ToJson(t);  // 将ScriptableObject 转换未Json文件

        formatter.Serialize(file, json); //序列化Json 文件，转存到file 的文件中

        file.Close(); //关闭流读写器;
        Debug.Log("文件路径"+path);
    }
    
    
    /// <summary>
    /// 保存ScriptableObject 数据
    /// </summary>
    /// <param name="t">配置数据</param>
    /// <param name="paht">保存路径</param>
    /// <param name="DataName">文件名称</param>
    /// <typeparam name="T"></typeparam>
    public static void SaveGame<T>(T t, string paht, string DataName) where T : ScriptableObject
    {
        string path = paht + "/Sava_GameData";
        //判断是否有该文件夹
        if (!Directory.Exists(path)) 
        {
            //如果没有 则创建一个文件夹
            Directory.CreateDirectory(path);
        }
        BinaryFormatter formatter = new BinaryFormatter(); //二进制转换

        FileStream file =  File.Create(path +"/"+ DataName); //创建文件   

        var json = JsonUtility.ToJson(t);  // 将ScriptableObject 转换未Json文件

        formatter.Serialize(file, json); //序列化Json 文件，转存到file 的文件中

        file.Close(); //关闭流读写器;
        Debug.Log("文件路径"+path);
    }

    /// <summary>
    ///  保存ScriptableObject 数据到本机磁盘 默认保存到Data.txt 中
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="t">数据</param>
    public static void SavaGame<T>(T t) where T : ScriptableObject
    {

        string path = Application.persistentDataPath + "/Sava_GameData";
        //判断是否有该文件夹
        if (!Directory.Exists(path))
        {
            //如果没有 则创建一个文件夹
            Directory.CreateDirectory(path);
        }
        BinaryFormatter formatter = new BinaryFormatter(); //二进制转换
        FileStream file = File.Create(path + "/" + "Data.txt"); //创建文件   

        var json = JsonUtility.ToJson(t);  // 将ScriptableObject 转换未Json文件

        formatter.Serialize(file, json); //序列化Json 文件，转存到file 的文件中

        file.Close(); //关闭流读写器;
    }

    /// <summary>
    /// 加载ScriptableObject 文件
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="DataName">文件名字，</param>
    /// <returns>返回Scriptable 类型的数据</returns>
    public static T LoadGame<T>(string DataName) where T : ScriptableObject
    {
        if (string.IsNullOrEmpty(DataName)) DataName = "Data.txt";
        string path = Application.persistentDataPath + "/Sava_GameData/" + DataName; //获取路径
        if (File.Exists(path)) //判断路径下是否有该文件
        {
            T game = ScriptableObject.CreateInstance<T>(); //创建一个 T 类型的ScriptableObject
           
            BinaryFormatter formatter = new BinaryFormatter();//二进制转换

            FileStream file = File.Open(path,FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file),game);// 反序列化二进制文件

            file.Close();//关闭流读写器
            return game;
        }
        else 
        {
            return null;
        }
    }

    /// <summary>
    /// 加载ScriptableObject 文件 默认为Data.txt
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <returns>类型的数据</returns>
    public static T LoadGame<T>() where T : ScriptableObject
    {
        string path = Application.persistentDataPath + "/Sava_GameData/" + "Data.txt"; //获取路径
        if (File.Exists(path)) //判断路径下是否有该文件
        {
            T game = ScriptableObject.CreateInstance<T>(); //创建一个 T 类型的ScriptableObject

            BinaryFormatter formatter = new BinaryFormatter();//二进制转换

            FileStream file = File.Open(path, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file), game);// 反序列化二进制文件

            file.Close();//关闭流读写器
            return game;
        }
        else
        {
            return null;
        }
    }
}
