using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Pool
{
    [CreateAssetMenu(fileName = "AudioConfig",menuName = "ARPG/Pool/PoolConfig")]
    public class PoolConfig : Config<PoolItem>
    {
        
    }
    
    [System.Serializable]
    public class PoolItem:ConfigData
    {
        [Header("初始数量")]
        public int InitAmount;
    }
}

