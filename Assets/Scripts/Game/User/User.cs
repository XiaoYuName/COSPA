using System;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 保存了用户信息:
    ///     包括但不限于 UID:    CreateTime: 保存时间     isNewUser : 是否进行过新手引导
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户UID
        /// </summary>
        public int UID;
        /// <summary>
        /// 保存时间
        /// </summary>
        public DateTime SaveTime;
        /// <summary>
        /// 玛那数量
        /// </summary>
        public int MonaAmount;
        /// <summary>
        /// 宝石数量
        /// </summary>
        public int GemsthoneAmount;

        /// <summary>
        /// 创建一个用户
        /// </summary>
        /// <param name="UID">UID</param>
        /// <param name="SaveTime">创建时间</param>
        /// <param name="MonaAmount">玛那数量</param>
        /// <param name="GemsthoneAmount">宝石数量</param>
        public User(int UID,DateTime SaveTime,int MonaAmount,int GemsthoneAmount)
        {
            this.UID = UID;
            this.SaveTime = SaveTime;
            this.MonaAmount = MonaAmount;
            this.GemsthoneAmount = GemsthoneAmount;
        }
    }
}