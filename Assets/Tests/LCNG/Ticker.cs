using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
//public enum TimerMode
//{
//    Loop,
//    NonLoop
//}
//Simple Timer class, it could get better as the game design gets more complex
//Code smell, caller lifespan strong dependency. Do third class nexor to safe communicate?...
//(SOLVED: with mandatory calls with caller as parameter)
[Serializable]
public class Ticker
{
    //private MonoBehaviour caller;
    public UnityEvent @event; // -> Use unityEvent?
    /// <summary>
    /// Time in miliseconds before action being called
    /// </summary>
    public float time = 1000.0f;
    //public TimerMode mode;
    private Coroutine coroutine;
    //public Ticker(UnityAction action, float time = 1000.0f)
    //{
    //    this.action = action;
    //    this.time = time;
    //    //this.mode = mode;
    //}
    public void Start(MonoBehaviour caller)
    {
        coroutine = caller.StartCoroutine(Tick(caller));
    }
    public void Stop(MonoBehaviour caller)
    {
        if (coroutine != null)
            caller.StopCoroutine(coroutine);
    }
    public void Restart(MonoBehaviour caller)
    {
        Stop(caller);
        Start(caller);
    }
    public void RestartWithAction(MonoBehaviour caller)
    {
        @event?.Invoke();
        Restart(caller);
    }
    private IEnumerator Tick(MonoBehaviour caller)
    {
        do
        {
            yield return new WaitForSeconds(time * 0.001f);
            if (caller) @event?.Invoke();
            else break;
        }
        while (true);
        yield return null;
    }
}