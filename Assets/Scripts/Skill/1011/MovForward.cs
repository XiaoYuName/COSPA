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
        private Character Player;
        private SkillItem data;


        public void Init()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void PlayMovForward(Character character, SkillItem data)
        {
            Init();
            Player = character;
            this.data = data;
            StartCoroutine(Movenemt());
        }


        private IEnumerator Movenemt()
        {
            float time = 0;
            while (gameObject.activeSelf)
            {
                time += Time.deltaTime;
                if(time > 3f)
                    gameObject.SetActive(false);
                
                rb.velocity += (Vector2)transform.right*data.Duration*Time.deltaTime;
                yield return null;
            }
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Character"))
            {
                Debug.Log("检测到敌人,准备触发伤害");
                Enemy enmey = col.transform.GetComponentInParent<Enemy>();
                Vector3 boundPoint = col.bounds.ClosestPoint(transform.position);
                GameManager.Instance.OptionDamage(Player,enmey,data,boundPoint);
            }
        }
    }
}

