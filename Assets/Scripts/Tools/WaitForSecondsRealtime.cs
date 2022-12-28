using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 扩展的yiele 类，该类不受到TimeScale = 0 的影响
    /// </summary>
    public class WaitForSecondsRealtime : CustomYieldInstruction
    {
        private float waitTime;

        public override bool keepWaiting => Time.realtimeSinceStartup < waitTime;

        public WaitForSecondsRealtime(float time)
        {
            waitTime = Time.realtimeSinceStartup + time;
        }
    }
}

