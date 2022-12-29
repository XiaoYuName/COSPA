using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovenemt : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D rb;
    private SpriteRenderer _renderer;

    public float speed;
    private Vector2 targetPos;
    private static readonly int RbX = Animator.StringToHash("rbX");
    private static readonly int Attack = Animator.StringToHash("attack");
    private static readonly int Strenght = Shader.PropertyToID("Strenght");

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        InputChick();
        
        AppleAnimator();
    }

    /// <summary>
    /// 按键检测
    /// </summary>
    private void InputChick()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            _anim.SetTrigger(Attack);
        }

        targetPos.x = Input.GetAxisRaw("Horizontal");
        
        
    }

    /// <summary>
    /// 应用动画播放
    /// </summary>
    private void AppleAnimator()
    {
        _anim.SetFloat(RbX,Mathf.Abs(targetPos.x));
    }
    

    private void FixedUpdate()
    {
        Movenemt();
       
    }

    /// <summary>
    /// 移动
    /// </summary>
    private void Movenemt()
    {
        if (targetPos.x != 0)
        {
            rb.velocity = new Vector2(targetPos.x * speed * Time.fixedDeltaTime, rb.velocity.y);
            PosRoatation();
        }
    }
    

    /// <summary>
    /// 转向
    /// </summary>
    private void PosRoatation()
    {
        transform.rotation = Quaternion.Euler(0, targetPos.x < 0 ? 180 : 0, 0);
    }



    #region Attack Event

    /// <summary>
    /// 攻击事件
    /// </summary>
    private void AttackDestruction()
    {
        Debug.Log("执行");
        SceenDestruction.Instance.Destruction();
       

    }

   public IEnumerator AttackLith()
    {
        float t = 1;
        while (t < 0)
        {
            t -= Time.deltaTime;
            _renderer.material.SetFloat(Strenght,t);
            yield return null;
        }
        
    }



    private void StopTime()
    {
        Time.timeScale = 0;
        StartCoroutine(AttackLith());
    }
    

    private void ResetTime()
    {
        Time.timeScale = 1;
    }


    #endregion
}
