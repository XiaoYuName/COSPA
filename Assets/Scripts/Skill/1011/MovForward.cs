using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 向前飞行的脚本对象
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class MovForward : MonoBehaviour
    {
        private Rigidbody2D rb;

        private void PlayMovForward(Character character, SkillItem data)
        {
            
        }


        private IEnumerator Movenemt()
        {
            yield return null;
        }


        private void OnCollisionEnter2D(Collision2D col)
        {
            
        }
    }
}

