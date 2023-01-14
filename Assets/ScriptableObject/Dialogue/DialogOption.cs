namespace ARPG.UI.Config
{
    /// <summary>
    /// 单独对话中的反馈选项列表
    /// </summary>
    [System.Serializable]
    public class DialogOption
    {
        /// <summary>
        /// 反馈的选项
        /// </summary>
        public string OpentionText;

        public DialogueFarmeMode Mode;
        
        /// <summary>
        /// 跳到下个对话的ID
        /// </summary>
        public int targetPirecID;
    }
}

