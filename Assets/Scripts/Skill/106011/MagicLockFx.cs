using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLockFx : MonoBehaviour
{
    private ParticleSystem BoomParent;
    private ParticleSystem loop_1;
    private ParticleSystem loop_2;
    private void Awake()
    {
        BoomParent = transform.GetChild(0).transform.GetChild(0).GetComponent<ParticleSystem>();
        loop_1 = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        loop_2 = transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>();
    }

    public void Boom()
    {
        BoomParent.TriggerSubEmitter(0);
        StartCoroutine(WaitClose());
    }

    public IEnumerator WaitClose()
    {
        loop_1.Stop(false,ParticleSystemStopBehavior.StopEmitting);
        loop_2.Stop(false,ParticleSystemStopBehavior.StopEmitting);
        yield return new WaitForSeconds(BoomParent.main.duration);
        gameObject.SetActive(false);
    }
}
