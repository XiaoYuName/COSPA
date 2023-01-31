using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace ARPG
{
    [CreateAssetMenu(fileName = "Buff",menuName = "ARPG/BUFFConfig")]
    public class BuffConfig : ScriptableObject
    {
        public List<BuffData> BuffDatas = new List<BuffData>();
    }

    [System.Serializable]
    public class BuffData
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string BuffName;
        /// <summary>
        /// 图标表ID
        /// </summary>
        public string SpriteID;
        
        /// <summary>
        /// 类型
        /// </summary>
        public BuffType buffType;

        [Range(0,1),Tooltip("触发概率")]
        public float random;
        
        /// <summary>
        /// 增益百分比
        /// </summary>
        public int buffPic;

        [Tooltip("BUFF类型,光环类的直到战斗结束才会消失")]
        public BuffBehaviourType behaviourType;

        [Tooltip("增益类型")]
        public StateMode PicMode;

        /// <summary>
        /// 特效预制体
        /// </summary>
        public List<GameObject> Pool;

        /// <summary>
        /// 持续时间
        /// </summary>
        public float continueTime;

        /// <summary>
        /// 最大层数
        /// </summary>
        public int maxLevel;
        
        /// <summary>
        /// BUFF层
        /// </summary>
        public BuffLayer layer;

        /// <summary>
        /// 每层增加的值
        /// </summary>
        public float valueBuff;

        /// <summary>
        /// Buff触发器
        /// </summary>
        public BuffTrigger buffTrigger;

        [ResizableTextArea,Tooltip("描述")]
        public string description;


        /// <summary>
        /// 将数据转换为IBuff实例
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public IBuff ToBuff(IBuffLogic character)
        {
            return new IBuff()
            {
                data = this,
                tag = character,
                curretnLevel = 0,
            };
        }
    }
}

