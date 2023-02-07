using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.GameSave;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 任务管理器
    /// </summary>
    public class TaskManager : MonoSingleton<TaskManager>,ISaveable
    {
        private TaskConfig _config;
        private Dictionary<string, TaskBag> GameTask = new Dictionary<string, TaskBag>();
        protected override void Awake()
        {
            base.Awake();
            _config = ConfigManager.LoadConfig<TaskConfig>("Task/SystemTask");
            MessageAction.newUser += newUser;
        }

        
        
        /// <summary>
        /// 根据配置创建对应映射字典与任务背包
        /// </summary>
        public void CreatTaskDictionary()
        {
            GameTask.Clear();
            List<TaskData> datas = _config.TaskDataList;
            foreach (var Task in datas)
            {
                TaskBag taskBag = new TaskBag
                {
                    currentAmount = 0,
                    TaskState = TaskState.未完成,
                    SaveTime = DateTime.Now,
                };
                InitGameTask(Task.TagUID,taskBag);
            }
        }

        private void InitGameTask(string ID,TaskBag bag)
        {
            if(!GameTask.ContainsKey(ID))
                GameTask.Add(ID,bag);
            GameTask[ID] = bag;
        }

        //--------------------------------存储--------------------------//
        private void newUser(User user)
        {
            CreatTaskDictionary();
        }

        public string GUID => "TaskManager";

        public void Start()
        {
            ISaveable _saveable = this;
            _saveable.RegisterSaveable();
        }

        public GameSaveData GenerateSaveData()
        {
            GameSaveData newSave = new GameSaveData()
            {
                SaveTask = GameTask,
            };
            return newSave;
        }

        public void RestoreData(GameSaveData GameSave)
        {
            Dictionary<string, TaskBag> Task = GameSave.SaveTask;
            if (Task != null)
            {
                GameTask = Task;
            }
        }
    }

    /// <summary>
    /// Task背包:存储游戏服务使用--保存任务进度,与任务状态
    /// </summary>
    public class TaskBag
    {
        /// <summary>
        /// 当前任务进度
        /// </summary>
        public int currentAmount;
        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskState TaskState;
        /// <summary>
        /// 保存时间
        /// </summary>
        public DateTime SaveTime;
    }
}

