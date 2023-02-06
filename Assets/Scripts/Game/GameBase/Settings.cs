using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;

namespace ARPG
{
    public  class Settings
    {
        public static Color ActiveColor = new Color(0.984f, 0.796f, 0.909f, 1f);
        
        public static Color NotActiveColor = new Color(0.46f,0.49f,0.60f,1);

        public const int MaxSelectAmount = 1;

        public const float isDownTime = 1.25f;

        /// <summary>
        /// 获取奖励界面每次生成的间隔时间
        /// </summary>
        /// 
        public const float RewordTime = 0.5f;

        /// <summary>
        /// ShowItem 动画时间
        /// </summary>
        public const float isShowItemTime = 0.25f;

        public static readonly Vector3 zeroView = new Vector3(0.5F, 0.25F, 0);

        //基础经验值: 经验值计算公式 :(基础经验值)*当前等级*(当前星级)
        public const int deftualExp = 30;


        /// <summary>
        /// 宝石ID
        /// </summary>
        public const string GemsthoneID = "91001";
        
        /// <summary>
        /// 玛那ID
        /// </summary>
        public const string ManaID = "94001";

        /// <summary>
        /// 经验值ID
        /// </summary>
        public const string ExpID = "92001";

        /// <summary>
        /// 强化石ID
        /// </summary>
        public const string PoworID = "22001";
        
        /// <summary>
        /// 计算成长值后的属性
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static CharacterState GetGrowthState(CharacterState state)
        {
            state.PhysicsAttack = (int)(state.PhysicsAttack *  (1+state.Growth));
            state.MagicAttack = (int)(state.MagicAttack *  (1+state.Growth));
            state.Defense = (int)(state.Defense *  (1+state.Growth));
            state.HP = (int)(state.HP *  (1+state.Growth));
            state.AddHp = (int)(state.AddHp *  (1+state.Growth));
            state.Cirtical = (int)(state.Cirtical *  (1+state.Growth));
            state.Power = (int)(state.Power *  (1+state.Growth));
            state.Intelligence = (int)(state.Intelligence *  (1+state.Growth));
            state.AttackSpeed = (state.AttackSpeed *  1+(state.Growth*0.125f));
            state.MovSpeed = (state.MovSpeed *  1+(state.Growth*0.125f));
            return state;
        }

        /// <summary>
        /// 强化的基础消耗材料
        /// </summary>
        public const int PaworDeftualAmount = 15;
    }
}

