using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;

public interface IDamage
{
    /// <summary>
    /// 获取自身属性状态
    /// </summary>
    /// <returns></returns>
    CharacterState GetState();

    /// <summary>
    /// 获取自身位置坐标:主要用于跟踪技能位置的锁定
    /// </summary>
    /// <returns></returns>
    Vector3 GetPoint();
    
    /// <summary>
    /// 受伤
    /// </summary>
    /// <param name="Damage">受到的伤害值</param>
    void IDamage(int Damage);

    /// <summary>
    /// 回复血量
    /// </summary>
    /// <param name="Reply"></param>
    void IReply(int Reply);
}
