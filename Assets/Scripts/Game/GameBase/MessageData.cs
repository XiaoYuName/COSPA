using System;
using UnityEngine.Events;

namespace ARPG
{
    public interface IMessageData
    {
        
    }


    /// <summary>
    /// 消息的数据封装
    /// </summary>
    public class MessageData<T> :IMessageData
    {
        public UnityAction<T> MessageEvents;

        public MessageData(UnityAction<T> action)
        {
            MessageEvents += action;
        }
    }
}