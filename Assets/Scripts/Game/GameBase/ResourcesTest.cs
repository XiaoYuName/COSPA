using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading.Tasks;

public class ResourcesTest : MonoBehaviour
{
    protected TextMeshPro TMP;
    protected Rigidbody2D rb;

    private void Awake()
    {
        TMP = GetComponent<TextMeshPro>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void Show()
    {
        int value = Random.Range(0, 99999);
        DamageType type = (DamageType)Random.Range(1, 4);
        bool isCirct = Random.value < 0.5f;
        TMP.text = TextAnimaSettings.GetDamageText(type, isCirct, value.ToString());
        rb.AddForce(new Vector2(0,Random.Range(1,3)),ForceMode2D.Impulse);
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1.2f);
        WaitTime();
    }

    public async void WaitTime()
    {
        var t = Task.Run(async () =>
        {
            await Task.Delay(1200);
        });
        await t;
        if(gameObject != null)
            Destroy(gameObject);
    }

}
