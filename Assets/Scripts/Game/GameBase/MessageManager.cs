using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace ARPG
{
    public class MessageManager: Singleton<MessageManager>
    {
        private Dictionary<int, IMessageData> MessageTable = new Dictionary<int, IMessageData>();

        public MessageManager()
        {
            Init();
        }

        private void Init()
        {
            MessageTable = new Dictionary<int, IMessageData>();
        }

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="Key">消息Key</param>
        /// <param name="action">回调委托</param>
        /// <typeparam name="T">传输数据</typeparam>
        public void Register<T>(C2C Key, UnityAction<T> action)
        {
            if (MessageTable.TryGetValue((int)Key, out var previousActon))
            {
                if (previousActon is MessageData<T> messageData)
                {
                    messageData.MessageEvents += action;
                }
            }
            else
            {
                MessageTable.Add((int)Key,new MessageData<T>(action));
            }
        }

        /// <summary>
        /// 取消注册消息
        /// </summary>
        /// <param name="Key">消息Key</param>
        /// <param name="action">注册时的回调函数</param>
        /// <typeparam name="T">数据类型</typeparam>
        public void URegister<T>(C2C Key, UnityAction<T> action)
        {
            if (MessageTable.TryGetValue((int)Key, out var previousAction))
            {
                if (previousAction is MessageData<T> messageData)
                {
                    messageData.MessageEvents -= action;
                }
            }
        }
        
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Key">Key</param>
        /// <param name="data">数据</param>
        /// <typeparam name="T">数据类型</typeparam>
        public void Send<T>(C2C Key, T data)
        {
            if (MessageTable.TryGetValue((int)Key, out var previousAction))
            {
                (previousAction as MessageData<T>)?.MessageEvents.Invoke(data);
            }
        }

        /// <summary>
        /// 清空所有表项
        /// </summary>
        public void Clear()
        {
            MessageTable.Clear();
        }
    }
}

