using System;
using System.Collections.Generic;
using System.IO;
using ARPG.GameSave;
using Newtonsoft.Json;
using UnityEngine;

namespace ARPG.UI
{
    public class ArchiveUI : UIBase,ISaveable
    {
        private List<User> Users = new List<User>();
        private RectTransform content;

        public override void Init()
        {
            content = Get<RectTransform>("UIMask/Scroll View/Viewport/Content");
            Users = LoadUsers();
            CreatArchiveUI();
        }

        private void CreatArchiveUI()
        {
            UIHelper.Clear(content);
            foreach (var user in Users)
            {
                ArchiveItemUI itemUI = UISystem.Instance.InstanceUI<ArchiveItemUI>("ArchiveItemUI"
                ,content);
                itemUI.InitData(user);
            }
            UISystem.Instance.InstanceUI<CreatArchiveUI>("CreatArchiveUI",content);
        }


        /// <summary>
        /// 保存用户数据
        /// </summary>
        private void SaveUsers()
        {
            var path = Application.persistentDataPath + "/Sava_GameData"+"/Users" + ".save";
            string json =  JsonConvert.SerializeObject(Users,Formatting.Indented);
            if (!Directory.Exists(Application.persistentDataPath+ "/Sava_GameData"))
            {
                Directory.CreateDirectory(Application.persistentDataPath+"/Sava_GameData");
            }
            File.WriteAllText(path,json);
        }

        /// <summary>
        /// 加载用户数据
        /// </summary>
        /// <returns></returns>
        private List<User> LoadUsers()
        {
            var path = Application.persistentDataPath + "/Sava_GameData"+"/Users" + ".save";
            if (!File.Exists(path)) return new List<User>();
            var JsonData = File.ReadAllText(path);
            List<User> SaveUsers =  JsonConvert.DeserializeObject<List<User>>(JsonData);
            return SaveUsers;
        }

        /// <summary>
        /// 创建User
        /// </summary>
        public void CreatUser()
        {
            User user = new User(Users.Count+1,DateTime.Now,0,0);
            Users.Add(user);
            MessageAction.OnNewUser(user);
            SaveUsers();
            SaveGameManager.Instance.Save(user.UID);
            CreatArchiveUI();
        }

        public string GUID => "ArchiveUI";

        public void Start()
        {
            ISaveable saveable = this;
            saveable.RegisterSaveable();
        }

        public GameSaveData GenerateSaveData()
        {
            SaveUsers();
            return new GameSaveData();
        }

        public void RestoreData(GameSaveData GameSave)
        {
            Users =LoadUsers();
            CreatArchiveUI();
        }
    }
}

