using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI.Config;
using UnityEngine;

namespace ARPG.GameSave
{
    /// <summary>
    /// GameSave 框架核心
    /// </summary>
    public class GameSaveData
    {
        //1.GameSave 将保存所有要用于存储的类型
        public UserBagConfig UserActivityIni;  //存储User活动 表配置

        public GameSaveData()
        {
            UserActivityIni = default;
        }
    }
}

