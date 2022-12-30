using System.Collections.Generic;

namespace ARPG.GameSave
{
    /// <summary>
    /// 用户数据:保存了用户的所有数据
    /// </summary>
    public class UserSlotData
    {
        /// <summary>
        /// 用户进度字典,存储了游戏进度中各个组件需要进行存储的GameSave ,string 是各个组件的GUID
        /// </summary>
        public Dictionary<string, GameSaveData> UserDatas = new Dictionary<string, GameSaveData>();
    }
}

