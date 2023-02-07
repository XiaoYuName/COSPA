using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ARPG.Config;
using ARPG.GameSave;
using ARPG.UI;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace ARPG
{
    /// <summary>
    /// 任务管理器
    /// </summary>
    public class TaskManager : MonoSingleton<TaskManager>,ISaveable
    {
        private TaskConfig _config;
        private Dictionary<string,TaskBag> GameTask = new Dictionary<string,TaskBag>();
        
        public int Timer;//累计在线时间
        private bool isStopTimer;
        
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
                AddGameTask(Task.TagUID,taskBag);
            }
        }

        /// <summary>
        /// 添加一个任务
        /// </summary>
        /// <param name="data">任务数据</param>
        /// <param name="bag">背包</param>
        private void AddGameTask(string data,TaskBag bag)
        {
            if (!GameTask.ContainsKey(data))
                GameTask[data] = bag;
            GameTask[data] = bag;
        }

        /// <summary>
        /// 触发Task 任务
        /// </summary>
        /// <param name="trigger">触发器类型</param>
        /// <param name="value">触发器值</param>
        private void TriggerTask(TaskTrigger trigger, int value)
        {
            for (int i = 0; i < GameTask.Count; i++)
            {
                (string ID,TaskBag bag) = GameTask.ElementAt(i);
                TaskData data = _config.GetTaskData(ID);
                if(data._TaskTrigger != trigger) continue;
                if(bag.TaskState == TaskState.未完成)
                    GameTask[ID].currentAmount += value;
                if (GameTask[ID].currentAmount >= data.RewordAmount)
                {
                    GameTask[ID].TaskState = TaskState.待领取;
                }
                RefTaskPanelUI(ID,GameTask[ID]);
            }
        }

        /// <summary>
        /// 初始化PanelTaskUI的任务显示
        /// </summary>
        private void InitTaskPanelUI()
        {
            SystemTaskPanel TaskPanel = UISystem.Instance.GetUI<SystemTaskPanel>("SystemTaskPanel",false);
            TaskPanel.CreatTaskItemUI(GameTask);
        }

        private void RefTaskPanelUI(string ID,TaskBag taskBag)
        {
            SystemTaskPanel TaskPanel = UISystem.Instance.GetUI<SystemTaskPanel>("SystemTaskPanel");
            TaskPanel.RefTaskItemUI(ID,taskBag);
        }


        /// <summary>
        /// 根据任务数据ID 获取任务数据
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>返回任务数据,如果找不到则返回空</returns>
        public TaskData GetTaskData(string ID)
        {
            return _config.GetTaskData(ID);
        }

        /// <summary>
        /// 根据任务数据ID，返回对应的任务背包数据
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>返回任务背包，如果找不到，则返回空</returns>
        public TaskBag GetTaskBag(string ID)
        {
            return GameTask.ContainsKey(ID) ? GameTask[ID] : null;
        }

        private CancellationTokenSource _tokenSource;

        /// <summary>
        /// 开始计算累计在线时间
        /// </summary>
        private async void StarTimer()
        {
            if (isStopTimer) return;
            try
            {
                Task timerTask = Task.Delay(TimeSpan.FromMinutes(1),_tokenSource.Token);
                await timerTask;
                Timer++;
                StarTimer();
                TriggerTask(TaskTrigger.在线, 1);
            }
            catch (Exception e)
            {
                Debug.Log("任务计时器:Timer 被强行停止"+e.Message);
            }
        }

        /// <summary>
        /// 停止计算累计在线时间
        /// </summary>
        public void StopTimer()
        {
            _tokenSource?.Cancel();
        }

        protected override void OnDestroy()
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            }

            base.OnDestroy();
            
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
                GameTask.Clear();
                GameTask = Task;
                _tokenSource = new CancellationTokenSource();
                StarTimer();
                InitTaskPanelUI();
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

