using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class CreatArchiveUI : UIBase
    {
        private Button CreatArchiveBtn;
        public override void Init()
        {
            CreatArchiveBtn = GetComponent<Button>();
            Bind(CreatArchiveBtn, delegate
            {
                UISystem.Instance.ShowPopDialogue("提示","是否要创建新的记忆","取消","创建",null
                , () =>
                {
                    void Func(ArchiveUI ui)
                    {
                        ui.CreatUser();
                    }
                    UISystem.Instance.OpenUI<ArchiveUI>("ArchiveUI", Func);
                });
            }, "UI_click");
        }

    }
}

