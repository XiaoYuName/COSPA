
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ARPG.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ARPG
{
    public class DamageTextItem : UIBase
    {
        protected TextMeshPro TMP;
        protected Rigidbody2D rb;
        private bool isInit;

        public void Start()
        {
            Init();
        }

        public override void Init()
        {
            TMP = GetComponent<TextMeshPro>();
            rb = GetComponent<Rigidbody2D>();
            isInit = true;
        }
        
        /// <summary>
        /// 显示伤害数字
        /// </summary>
        /// <param name="type">伤害类型</param>
        /// <param name="isCirct">是否暴击</param>
        /// <param name="damage">伤害值</param>
        public void Show(DamageType type,bool isCirct,string damage)
        {
            if(!isInit) Init();

            TMP.text = TextAnimaSettings.GetDamageText(type, isCirct, damage);
            rb.AddForce(new Vector2(0,Random.Range(5,8)),ForceMode2D.Impulse);
            transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1.2f);
            WaitTimeAsync();
        }

        /// <summary>
        /// 异步的等待一段时间
        /// </summary>
        public async void WaitTimeAsync()
        {
            var t = Task.Run(async () =>
            {
                await Task.Delay(1200);
            });
            await t;
            if(gameObject != null)
                gameObject.SetActive(false);
        }
        
    }
}

