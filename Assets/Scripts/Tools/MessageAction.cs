using System;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI.Config;
using UnityEngine;

namespace ARPG
{
    public static  class MessageAction
    {
        #region 场景切换

        public static event Action<string, Vector3,CharacterBag,RegionLine,RegionItem> StartGameScene;

        /// <summary>
        /// 首次进入战斗场景
        /// </summary>
        /// <param name="scneneName">场景名称</param>
        /// <param name="pos">生成位置</param>
        /// <param name="Character">玩家配置</param>
        /// <param name="regionLine">主线</param>
        /// <param name="regionItem">敌人配置</param>
        public static void OnStartGameScene(string scneneName, Vector3 pos,CharacterBag Character,RegionLine regionLine,RegionItem regionItem)
        {
            StartGameScene?.Invoke(scneneName,pos,Character,regionLine,regionItem);
        }


        public static event Action<string, RegionQuitData> QuitAttackScene;

        public static void OnQuitAttackScnen(string sceneName, RegionQuitData data)
        {
            QuitAttackScene?.Invoke(sceneName,data);
        }


        public static event Action<string, Vector3> TransitionEvent;
        
        public static void OnTransitionEvent(string name, Vector3 pos)
        {
            TransitionEvent?.Invoke(name, pos);
        }
        
        /// <summary>
        /// 场景卸载前回调
        /// </summary>
        public static event Action BeforScenenUnloadEvent;
        /// <summary>
        /// 场景卸载前回调
        /// </summary>
        public static void OnBeforScenenUnloadEvent()
        {
            BeforScenenUnloadEvent?.Invoke();
        }
        
        /// <summary>
        /// 新场景加载后回调
        /// </summary>
        public static event Action AfterScenenLoadEvent;
        /// <summary>
        /// 新场景加载后回调
        /// </summary>
        public static void OnAfterScenenLoadEvent()
        {
            AfterScenenLoadEvent?.Invoke();
        }
        
        /// <summary>
        /// 移动玩家坐标
        /// </summary>
        public static event Action<Vector3> MovToPosint;

        ///移动玩家坐标
        public static void OnMovToPosint(Vector3 obj)
        {
            MovToPosint?.Invoke(obj);
        }
        
        #endregion


        #region UI刷新

        /// <summary>
        /// 刷新货币
        /// </summary>
        public static event Action<ItemBag, ItemBag> UpdataeMoney;
        
        /// <summary>
        /// 刷新货币事件
        /// </summary>
        /// <param name="Gemsthone">宝石</param>
        /// <param name="Mana">玛那</param>
        public static void OnUpdataeMoney(ItemBag Gemsthone, ItemBag Mana)
        {
            UpdataeMoney?.Invoke(Gemsthone, Mana);
        }

        /// <summary>
        /// 刷新角色背包
        /// </summary>
        public static event Action<CharacterBag> UpCharacterBag;
        /// <summary>
        /// 刷新角色背包
        /// </summary>
        public static void OnUpCharacterBag(CharacterBag obj)
        {
            UpCharacterBag?.Invoke(obj);
        }

        /// <summary>
        /// 通关后,刷新当前进度
        /// </summary>
        public static event Action RefRegionPress;
        public static void OnRefRegionPress()
        {
            RefRegionPress?.Invoke();
        }
        
        /// <summary>
        /// 通关后刷新进度字典
        /// </summary>
        public static event Action SetUpRegionPress;
        
        public static void OnSetUpRegionPress()
        {
            SetUpRegionPress?.Invoke();
        }
        

        /// <summary>
        /// 创建新用户事件
        /// </summary>
        public static event Action<User> newUser;
        
        /// <summary>
        /// 创建新用户事件
        /// </summary>
        /// <param name="CreatNewUser">创建的用户</param>
        public static void OnNewUser(User CreatNewUser)
        {
            newUser?.Invoke(CreatNewUser);
        }


        /// <summary>
        /// 刷新背包道具
        /// </summary>
        public static event Action<List<ItemBag>> RefreshItemBag;
        
        /// <summary>
        /// 刷新背包道具
        /// </summary>
        /// <param name="ListBags"></param>
        public static void OnRefreshItemBag(List<ItemBag> ListBags)
        {
            RefreshItemBag?.Invoke(ListBags);
        }


        /// <summary>
        /// 刷新角色背包状态
        /// </summary>
        public static event Action<List<CharacterBag>> RefreshCharacterBag;

        /// <summary>
        /// 刷新角色背包状态
        /// </summary>
        /// <param name="ListBags"></param>
        public static void OnRefreshCharacterBag(List<CharacterBag> ListBags)
        {
            RefreshCharacterBag?.Invoke(ListBags);
        }
        #endregion


        
    }
}

