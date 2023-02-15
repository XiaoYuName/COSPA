using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class TaskItemUI : UIBase
    {
        private TextMeshProUGUI TypeTag;
        private TextMeshProUGUI TaskName;
        private TextMeshProUGUI SliderValue;
        private Slider Slider;
        private RectTransform Content;
        private Button GetRewordBtn;
        private TaskBag CurrentBag;
        private TaskData CurrentData;

        private TextMeshProUGUI BtnName;
        public override void Init()
        {
            TypeTag = Get<TextMeshProUGUI>("Type/NameText");
            TaskName = Get<TextMeshProUGUI>("Name");
            SliderValue = Get<TextMeshProUGUI>("Pressers/SliderValue");
            Slider = Get<Slider>("Pressers/Slider_001");
            Content = Get<RectTransform>("Panel/Scroll View/Viewport/Content");
            GetRewordBtn = Get<Button>("BindBtn");
            Bind(GetRewordBtn,GetReword,"OnChick");
            BtnName = Get<TextMeshProUGUI>("BindBtn/Text");
        }

        public void IniData(TaskData data,TaskBag taskBag)
        {
            TypeTag.text = data.TaskMode.ToString();
            CurrentBag = taskBag;
            CurrentData = data;
            TaskName.text = data.TaskName;
            SliderValue.text = taskBag.currentAmount + "/" + data.RewordAmount;
            Slider.minValue = 0;
            Slider.maxValue = data.RewordAmount;
            Slider.value = taskBag.currentAmount;
            UIHelper.Clear(Content);
            foreach (var Ibag in data.RewordItemList)
            {
                var SlotUI = UISystem.Instance.InstanceUI<MaterialSlotUI>("MaterialSlotUI",Content);
                SlotUI.InitData(Ibag);
            }
            
        }

        public void RefTaskUI(TaskBag taskBag)
        {
            CurrentBag = taskBag;
            SliderValue.text = CurrentBag.currentAmount + "/" + CurrentData.RewordAmount;
            Slider.value = taskBag.currentAmount;
            if (CurrentBag.TaskState == TaskState.待领取)
            {
                GetRewordBtn.interactable = true;
                BtnName.text = "领取";
                transform.SetAsFirstSibling();
            }
            else
            {
                if(CurrentBag.TaskState == TaskState.已领取)
                    transform.SetAsLastSibling();
                GetRewordBtn.interactable = false;
                BtnName.text = CurrentBag.TaskState.ToString();
            }
        }

        public void GetReword()
        {
            if (CurrentBag == null || CurrentData == null) return;
            if (CurrentBag.TaskState == TaskState.未完成)
            {
                UISystem.Instance.ShowTips("任务还未完成",1f);
                return;
            }
            if (CurrentBag.TaskState == TaskState.已领取)
            {
                UISystem.Instance.ShowTips("任务已领取",1f);
                return;
            }
            
            InventoryManager.Instance.AddItem(CurrentData.RewordItemList);
            UISystem.Instance.ShowReword(CurrentData.RewordItemList);
            CurrentBag.TaskState = TaskState.已领取;
            TaskManager.Instance.SetTaskState(CurrentData.TagUID,TaskState.已领取);
        }
    }
}

