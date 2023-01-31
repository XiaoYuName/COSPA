using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using UnityEngine;

namespace ARPG
{
    public interface IBuffLogic
    {
        IDamage GetIDamage();

        BuffStateUI GetStateUI();
    }
}

