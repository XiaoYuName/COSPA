using ARPG;
using UnityEngine;

public class AnimationMsg : MonoBehaviour
{
    
    public void SpineEvent(string EventName)
    {
        MessageManager.Instance.Send(C2C.EventMsg,EventName);
    }

    public void EnemySpineEvent(string EventName)
    {
        MessageManager.Instance.Send(C2C.BOSSEventMsg,EventName);
    }
}
