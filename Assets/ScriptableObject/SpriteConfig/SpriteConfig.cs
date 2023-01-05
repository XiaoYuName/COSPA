using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Config
{
    [CreateAssetMenu(fileName = "SpriteConfig",menuName = "ARPG/精灵配置表")]
    public class SpriteConfig : Config<SpriteItem>
    {
        
    }

    [System.Serializable]
    public class SpriteItem : ConfigData
    {
        public Sprite Sprite;
    }
}


