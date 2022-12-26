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
        private Enemy currentEnemy;
        protected bool isEnemy; //是否是Enemy释放的
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
            isEnemy = false;
            currentEnemy = null;
            currentdata = data;
            this.currentPlayer = Player;
            Collider2D.radius = data.Radius;
            if(data.RadiusOffset != Vector2.zero)
                Collider2D.offset = data.RadiusOffset;
            
            StartCoroutine(WaitDuration(data.Duration));
        }

        /// <summary>
        /// 释放技能特效
        /// </summary>
        /// <param name="enemy">怪物</param>
        /// <param name="data">技能数据</param>
        public void Play(Enemy enemy, SkillItem data)
        {
            isEnemy = true;
            currentPlayer = null;
            currentdata = data;
            currentEnemy = enemy;
            Collider2D.radius = data.Radius;
            if(data.RadiusOffset != Vector2.zero)
                Collider2D.offset = data.RadiusOffset;
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
                
                if(!isEnemy)
                    GameManager.Instance.OptionDamage(currentPlayer,col.GetComponent<Enemy>(),currentdata,hitPoint);
                else
                    GameManager.Instance.OptionDamage(currentEnemy,col.transform.parent.GetComponent<Character>(),currentdata,hitPoint);
            }
        }
    }

}
