using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Config
{
    [CreateAssetMenu(fileName = "FadeConfig",menuName = "ARPG/UI/FadeConfig")]
    public class FadeConfig : ScriptableObject
    {
        [Header("过长动画漫画列表")]
        public List<Sprite> FadeSprites = new List<Sprite>();

        
        /// <summary>
        /// 获取随机一个过程小漫画
        /// </summary>
        /// <returns></returns>
        public Sprite GetRandomSprite()
        {
            return FadeSprites[Random.Range(0, FadeSprites.Count)];
        }
    }
}

