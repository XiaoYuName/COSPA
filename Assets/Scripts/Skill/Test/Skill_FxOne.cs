using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FxOne : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WaifTime());
    }

    IEnumerator WaifTime()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        anim.SetTrigger("isTrigger");
    }
}
