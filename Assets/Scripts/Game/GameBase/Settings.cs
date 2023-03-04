using System;
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
        /// <summary>
        /// Pop弹窗弹出动画时长
        /// </summary>
        public const float PopTweenTime = 0.15f;

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

        /// <summary>
        /// 扭蛋动画的偏移时间
        /// </summary>
        public const float TwistTweenTime = 1f;


        /// <summary>
        /// 判断是否是随机奖励地下城
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static bool isRandomRegion(string Name)
        {
            var strings = Enum.GetNames(typeof(RegionRandomType));
            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i] == Name)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 常用UI音效类型
    /// </summary>
    public static class UiAudioID
    {
        /// <summary>
        /// 确认
        /// </summary>
        public const string OnChick = "OnChick";
        /// <summary>
        /// 退出
        /// </summary>
        public const string OutChick = "OutChick";
        /// <summary>
        /// 单击
        /// </summary>
        public const string UI_click = "UI_click";
        /// <summary>
        /// 弹窗
        /// </summary>
        public const string PopWindows = "PopWindows";
        
        /// <summary>
        /// UI点击音效——2
        /// </summary>
        public const string UI_Bc_Click = "UI_Bc_Click";
    }
    
    
}

