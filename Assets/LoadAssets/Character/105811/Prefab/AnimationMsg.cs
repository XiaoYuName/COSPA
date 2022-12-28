using ARPG;
using UnityEngine;

public class AnimationMsg : MonoBehaviour
{
    
    public void SpineEvent(string EventName)
    {
        MessageManager.Instance.Send(C2S.EventMsg,EventName);
    }
}
