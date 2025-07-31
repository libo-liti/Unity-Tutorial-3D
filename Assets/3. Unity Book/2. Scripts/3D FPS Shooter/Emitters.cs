using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Emitters : MonoBehaviour
{
    public PlayableDirector timeline;
    public SignalReceiver receiver;
    public SignalAsset signal;


    public void OnTimelineSpeed(float speed)
    {
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(speed);
    }

    public void SetSignalEvent()
    {
        UnityEvent eventContainer = new UnityEvent();
        
        eventContainer.AddListener(() => OnTimelineSpeed(0.2f));
        receiver.AddReaction(signal, eventContainer);
    }
}
