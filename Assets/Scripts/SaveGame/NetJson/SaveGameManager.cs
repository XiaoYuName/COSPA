using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace ARPG.GameSave
{
    /// <summary>
    /// 数据存储管理类
    /// </summary>
    public class SaveGameManager: MonoSingleton<SaveGameManager>
    {
        private List<ISaveable> Saveables = new List<ISaveable>();
        private static string JsonSavePath;

        protected override void Awake()
        {
            base.Awake();
            JsonSavePath = Application.persistentDataPath;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.O))
            {
                Save(1);
            }
        }


        /// <summary>
        /// 注册函数将自身要存储的信息注册到SaveablesList中
        /// </summary>
        /// <param name="saveable"></param>
        public void RegisterSaveable(ISaveable saveable)
        {
            if (!Saveables.Contains(saveable))
            {
                Saveables.Add(saveable);
            }
        }

        /// <summary>
        /// 保存用户进度
        /// </summary>
        /// <param name="UID">用户唯一标识符 UID</param>
        public void Save(int UID)
        {
            UserSlotData User = new UserSlotData();
            foreach (var SaveItem in Saveables)
            {
                User.UserDatas.Add(SaveItem.GUID, SaveItem.GenerateSaveData());
            }

            var path = JsonSavePath + "/Sava_GameData"+ "/User" + UID + ".scriptable";
            var JsonData = JsonConvert.SerializeObject(User, Formatting.Indented);
            if (!Directory.Exists(JsonSavePath+ "/Sava_GameData"))
            {
                Directory.CreateDirectory(JsonSavePath+ "/Sava_GameData");
            }
            File.WriteAllText(path, JsonData);
        }

        /// <summary>
        /// 加载用户进度
        /// </summary>
        /// <param name="UID">用户唯一标识符 UID</param>
        /// <returns></returns>
        public void Load(int UID)
        {
            var path = JsonSavePath + "/Sava_GameData"+ "/User" + UID + ".scriptable";
            if (File.Exists(path))
            {
                var JsonData = File.ReadAllText(path);
                UserSlotData slotData =  JsonConvert.DeserializeObject<UserSlotData>(JsonData);
                foreach (var SaveItem in Saveables)
                {
                    SaveItem.RestoreData(slotData.UserDatas.ContainsKey(SaveItem.GUID)
                        ? slotData.UserDatas[SaveItem.GUID]
                        : new GameSaveData());
                }
            }
        }
    }
}


