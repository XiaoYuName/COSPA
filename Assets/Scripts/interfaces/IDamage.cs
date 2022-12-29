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
