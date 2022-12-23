using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class _FxItem : MonoBehaviour
    {
        private CircleCollider2D Collider2D;
        protected Character currentPlayer;
        private SkillItem currentdata;
        private void Awake()
        {
            Collider2D = GetComponent<CircleCollider2D>();
            Collider2D.isTrigger = true;
        }
        
        /// <summary>
        /// 释放技能特效
        /// </summary>
        /// <param name="Player">释放者</param>
        /// <param name="data">技能数据</param>
        public void Play(Character Player,SkillItem data)
        {
            currentdata = data;
            this.currentPlayer = Player;
            Collider2D.radius = data.Radius;
            StartCoroutine(WaitDuration(data.Duration));
        }

        public IEnumerator WaitDuration(float tiem)
        {
            yield return new WaitForSeconds(tiem);
            gameObject.SetActive(false);
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (gameObject.activeSelf && col.gameObject.CompareTag($"Character"))
            {
                var hitPoint = col.bounds.ClosestPoint(transform.position);
                GameManager.Instance.OptionDamage(currentPlayer,col.GetComponent<Enemy>(),currentdata,hitPoint);
            }
        }
    }

}
