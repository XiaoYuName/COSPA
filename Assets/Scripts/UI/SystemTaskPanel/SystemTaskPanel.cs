using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ARPG.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class SystemTaskPanel : UIBase
    {
        private Button CloseBtn;

        private Dictionary<string, TaskItemUI> UITaskDic = new Dictionary<string, TaskItemUI>();
        #region SwitchTable
        private Button DayTaskBtn;
        private Button CommonBtn;
        private Button LimitBtn;
        private Button TitleBtn;
        private RectTransform DayTaskConent;
        private RectTransform CommonConent;
        private RectTransform LimitContent;
        private RectTransform TitleContent;
        #endregion
        
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,"OutChick");
            DayTaskConent = Get<RectTransform>("UIMask/Panel/DayContent/Viewport/Content");
            CommonConent = Get<RectTransform>("UIMask/Panel/CommonContent/Viewport/Content");
            LimitContent = Get<RectTransform>("UIMask/Panel/LimitContent/Viewport/Content");
            TitleContent = Get<RectTransform>("UIMask/Panel/TitleContent/Viewport/Content");
            DayTaskBtn = Get<Button>("UIMask/SwitchTable/DayTask");
            CommonBtn = Get<Button>("UIMask/SwitchTable/CommonBtn");
            LimitBtn = Get<Button>("UIMask/SwitchTable/LimitBtn");
            TitleBtn = Get<Button>("UIMask/SwitchTable/TitleBtn");
            Bind(DayTaskBtn,()=>SwitchTableContent(TaskTableMode.每日),"OnChick");
            Bind(CommonBtn,()=>SwitchTableContent(TaskTableMode.普通),"OnChick");
            Bind(LimitBtn,()=>SwitchTableContent(TaskTableMode.限定),"OnChick");
            Bind(TitleBtn,()=>SwitchTableContent(TaskTableMode.称号),"OnChick");
            SwitchTableContent(TaskTableMode.每日);
        }

        public void CreatTaskItemUI(Dictionary<string,TaskBag> GameTask)
        {
            UIHelper.Clear(DayTaskConent);
            UIHelper.Clear(CommonConent);
            UIHelper.Clear(LimitContent);
            UIHelper.Clear(TitleContent);
            for (int i = 0; i < GameTask.Count; i++)
            {
                (string ID, TaskBag taskBag) = GameTask.ElementAt(i);
                CreatTaskUI(ID,taskBag);
            }
        }

        
        public void RefTaskItemUI(string ID, TaskBag data)
        {
            if (UITaskDic.ContainsKey(ID))
            {
                UITaskDic[ID].RefTaskUI(data);
            }
        }


        /// <summary>
        /// 切换界面的显示状态
        /// </summary>
        /// <param name="tableMode"></param>
        private void SwitchTableContent(TaskTableMode tableMode)
        {
            DayTaskBtn.GetComponent<Image>().color =
                tableMode == TaskTableMode.每日 ? Color.white : new Color(1, 1, 1, 0);
            CommonBtn.GetComponent<Image>().color =
                tableMode == TaskTableMode.普通 ? Color.white : new Color(1, 1, 1, 0);
            LimitBtn.GetComponent<Image>().color =
                tableMode == TaskTableMode.限定 ? Color.white : new Color(1, 1, 1, 0);
            TitleBtn.GetComponent<Image>().color =
                tableMode == TaskTableMode.称号 ? Color.white : new Color(1, 1, 1, 0);
            DayTaskConent.parent.parent.gameObject.SetActive(tableMode == TaskTableMode.每日);
            CommonConent.parent.parent.gameObject.SetActive(tableMode == TaskTableMode.普通);
            LimitContent.parent.parent.gameObject.SetActive(tableMode == TaskTableMode.限定);
            TitleContent.parent.parent.gameObject.SetActive(tableMode == TaskTableMode.称号);
        }

        
        private void CreatTaskUI(string ID,TaskBag taskBag)
        {
            TaskData data = TaskManager.Instance.GetTaskData(ID);
            RectTransform content = data.Mode switch
            {
                TaskTableMode.每日 => DayTaskConent,
                TaskTableMode.普通 => CommonConent,
                TaskTableMode.限定 => LimitContent,
                TaskTableMode.称号 => TitleContent,
                _ => DayTaskConent,
            };
            TaskItemUI itemUI = UISystem.Instance.InstanceUI<TaskItemUI>("TaskItemUI",content);
            itemUI.IniData(data,taskBag);
            AddTaskDic(ID,itemUI);
        }
        
        private void AddTaskDic(string ID, TaskItemUI itemUI)
        {
            if(!UITaskDic.ContainsKey(ID))
                UITaskDic.Add(ID,itemUI);
            UITaskDic[ID] = itemUI;
        }

        public override void Close()
        {
            if(MainPanel.IsInitialized)
                MainPanel.Instance.RemoveTableChild("SystemTaskPanel");
            base.Close();
        }
    }
}

