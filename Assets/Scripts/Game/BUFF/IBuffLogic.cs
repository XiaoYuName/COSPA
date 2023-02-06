using System;
using ARPG.UI;
using UnityEngine;

namespace ARPG
{
    public interface IBuffLogic
    {
        IDamage GetIDamage();

        BuffStateUI GetStateUI();

        void AddBuff(BuffData data);

        void AddBuffEvent(EndTrigger trigger, IBuff IBuff, Action action);
    }
}

