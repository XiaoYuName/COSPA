using System.Collections;
using System.Collections.Generic;
using ARPG;
using UnityEngine;

/// <summary>
/// 引力黑洞粒子
/// </summary>
public class GravitationalBlackHoleFx : MonoBehaviour
{
    public void Play(SkillItem data)
    {
        StartCoroutine(MovTarget(data));
    }

    public IEnumerator MovTarget(SkillItem item)
    {
        Vector3 target = new Vector3(transform.position.x + item.RadiusOffset.x, transform.position.y
            + item.RadiusOffset.y, 0);
        while (Vector2.Distance(transform.position,target) >0.25f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target,
                item.Duration * Time.deltaTime);
            yield return null;
        }
    }
}
