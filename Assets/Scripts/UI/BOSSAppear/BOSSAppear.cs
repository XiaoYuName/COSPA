using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.UI
{
    /// <summary>
    /// BOSS来袭提示
    /// </summary>
    public class BOSSAppear : UIBase
    {
        private Animator anim;
        public override void Init()
        {
            anim = GetComponent<Animator>();
        }

        public override void Open()
        {
            base.Open();
            anim.SetTrigger("isOpen");
        }
    }
}

