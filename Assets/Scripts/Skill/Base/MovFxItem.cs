using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 移动类攻击怪物特效:
///         给定一个目标点：移动到该目标
/// </summary>
public class MovFxItem : MonoBehaviour
{

    private Action endAction;
    private IDamage attack;
    private SkillItem Item;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 朝向目标点位置移动
    /// </summary>
    /// <param name="enemy">攻击者</param>
    /// <param name="target">目标点</param>
    /// <param name="skillItem">技能数据</param>
    /// <param name="action">结束后回调</param>
    public void StarMovTarget(Enemy enemy,Vector3 target,SkillItem skillItem,Action action)
    {
        //1.获得目标方向
        endAction = action;
        this.attack = enemy;
        this.Item = skillItem;

        StartCoroutine(MovToTarget(enemy,target,endAction));
    }

    public IEnumerator MovToTarget(Enemy damage,Vector3 dir,Action action)
    {
        float time = 5;
        rb.AddForce(dir*15,ForceMode2D.Impulse);
        while (gameObject.activeSelf && time > 0)
        {
            time -= Time.deltaTime;
            // transform.Translate( transform.right*15*Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
        action?.Invoke();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.activeSelf && col.gameObject!= null && col.gameObject.CompareTag($"Character"))
        {
            var hitPoint = col.bounds.ClosestPoint(transform.position);
            GameManager.Instance.OptionDamage(attack,col.GetComponentInParent<Character>(),Item,hitPoint);
            endAction?.Invoke();
            gameObject.SetActive(false);
            endAction = null;
        }
    }
}
