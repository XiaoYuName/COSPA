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
        public Dictionary<string, TaskBag> SaveTask;

        public  Dictionary<string, Dictionary<string, RegionProgress>> RegionSave =  new Dictionary<string, Dictionary<string, RegionProgress>>();
    }
}

