using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class PlotPanel : UIBase
    {
        public Button OpenSwicthMapPanel;
        public override void Init()
        {
            OpenSwicthMapPanel = Get<Button>("UIMask/Right/MianPlot");
        }
    }
}

