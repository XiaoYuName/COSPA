using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
using UnityEngine;

/// <summary>
/// 魅惑子粒子
/// </summary>
public class Charm_Fx : MonoBehaviour
{
    private ParticleSystem charmParticleSystem;
    private IDamage attack;
    private SkillItem data;

    private void Awake()
    {
        charmParticleSystem = GetComponent<ParticleSystem>();
    }

    public void Init(IDamage attack, SkillItem data)
    {
        this.attack = attack;
        this.data = data;
    }


    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int number = charmParticleSystem.GetCollisionEvents(other, collisionEvents);
        Debug.Log("当前同时碰到了"+number+"个怪物");
        if (attack == null || data == null) return;
        if(number <= 0)return;


        for (int i = 0; i < number; i++)
        {
            IDamage target = collisionEvents[i].colliderComponent.transform.GetComponentInParent<IDamage>();
            if(target == null)continue;
            GameManager.Instance.OptionDamage(attack,target,data,target.GetPoint());
        }
        
    }
}
