using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public  class Settings
    {
        public static Color ActiveColor = new Color(0.984f, 0.796f, 0.909f, 1f);
        
        public static Color NotActiveColor = new Color(0.46f,0.49f,0.60f,1);

        public const int MaxSelectAmount = 1;

        public const float isDownTime = 1.25f;

        public static readonly Vector3 zeroView = new Vector3(0.5F, 0.25F, 0);


        /// <summary>
        /// 宝石ID
        /// </summary>
        public const string GemsthoneID = "91001";
        
        /// <summary>
        /// 玛那ID
        /// </summary>
        public const string ManaID = "94001";
    }
}

