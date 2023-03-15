using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 引力黑洞粒子
/// </summary>
public class GravitationalBlackHoleFx : MonoBehaviour
{
    private ParticleSystem Fx;
    private IDamage attack;
    private SkillItem Skilldata;

    private void Awake()
    {
        Fx = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    public void Play(IDamage attack,SkillItem data)
    {
        this.attack = attack;
        Skilldata = data;
        targets.Clear();
        StartCoroutine(WaitClose());
        StartCoroutine(MovTarget(data));
    }

    public IEnumerator MovTarget(SkillItem item)
    {
        float OffetsX = transform.rotation.eulerAngles.y < 180 ? item.RadiusOffset.x : -item.RadiusOffset.x;
        Vector3 target = new Vector3(transform.position.x + OffetsX, transform.position.y + item.RadiusOffset.y, 0);
        while (Vector2.Distance(transform.position,target) >0.25f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target,
                item.Duration * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator WaitClose()
    {
        yield return new WaitForSeconds(Fx.main.duration);

        for (int i = 0; i < targets.Count; i++)
        {
            GameManager.Instance.OptionDamage(attack,targets[i],Skilldata,transform.position);
        }
        AudioManager.Instance.PlayAudio("GravitationalBlackHoleFx");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private List<Enemy> targets = new List<Enemy>();

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(!targets.Contains(col.GetComponent<Enemy>()))
            targets.Add(col.GetComponentInParent<Enemy>());
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        col.transform.parent.transform.position = transform.position;
    }
}
