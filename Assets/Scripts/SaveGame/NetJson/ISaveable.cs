using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.GameSave
{
    /// <summary>
    /// 存储数据的接口,所有要存储数据的类都将要实现该接口
    /// </summary>
    public interface ISaveable
    {
        string GUID { get; }

        /// <summary>
        /// 注册自身
        /// </summary>
        void RegisterSaveable()
        {
            SaveGameManager.Instance.RegisterSaveable(this);
        }

        /// <summary>
        /// 存储数据
        /// </summary>
        /// <returns>GameSavaData 保存了所有要存储的数据</returns>
        GameSaveData GenerateSaveData();

        void RestoreData(GameSaveData GameSave);
    }
}

