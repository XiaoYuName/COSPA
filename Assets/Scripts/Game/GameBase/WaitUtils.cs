using System;
using System.Collections;
using ARPG;
using UnityEngine;

public class WaitUtils
{
    /// <summary>
    /// 等待一段时间执行
    /// </summary>
    /// <param name="mono">mono</param>
    /// <param name="time">等待时间</param>
    /// <param name="onWaitEnd">时间结束事件</param>
    private static Coroutine WaitTime(MonoBehaviour mono, float time, Action onWaitEnd)
    {
        return mono.StartCoroutine(WaitTime(time, onWaitEnd));
    }

    /// <summary>
    /// 等待一段时间执行
    /// </summary>
    /// <param name="time">等待时间s</param>
    /// <param name="onWaitEnd">时间结束回调</param>
    /// <returns></returns>
    public static Coroutine WaitTimeDo(float time, Action onWaitEnd)
    {
        return WaitTime(GameManager.Instance, time, onWaitEnd);
    }

    private static IEnumerator WaitTime(float waitTime, Action onWaitEnd)
    {
        yield return new WaitForSeconds(waitTime);
        onWaitEnd?.Invoke();
    }

    public static void StopWaitTimeDo(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            GameManager.Instance.StopCoroutine(coroutine);
        }
    }
}