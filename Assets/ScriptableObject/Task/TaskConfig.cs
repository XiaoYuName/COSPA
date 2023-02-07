using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Config
{
    [CreateAssetMenu(fileName = "SystemTask",menuName = "ARPG/Task/TaskConfig")]
    public class TaskConfig : ScriptableObject
    {
        public List<TaskData> TaskDataList = new List<TaskData>();

        public TaskData GetTaskData(string ID)
        {
            return TaskDataList.Find(id => id.TagUID == ID);
        }
    }

    [System.Serializable]
    public class TaskData
    {
        [Header("任务配置")]
        [Tooltip("任务需求描述")]
        public string TaskName;
        [Tooltip("任务标示UID:")]
        public string TagUID;
        /// <summary>
        /// 分页
        /// </summary>
        public TaskTableMode Mode;
        /// <summary>
        /// 标签
        /// </summary>
        public TaskMode TaskMode;
        /// <summary>
        /// 刷新规则
        /// </summary>
        public TaskRefType RefType;

        [Header("需求配置")]
        public int RewordAmount;
        
        [Tooltip("任务触发器")]
        public TaskTrigger _TaskTrigger;

        /// <summary>
        /// 奖励列表
        /// </summary>
        [Header("奖励配置")]
        public List<ItemBag> RewordItemList = new List<ItemBag>();
    }
}

