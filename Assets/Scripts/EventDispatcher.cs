using System;
using UnityEngine;

public class EventDispatcher : MonoBehaviour
{
    public static EventDispatcher instance;
    private Action<object>[] callbacks;
    public void Awake()
    {
        if (EventDispatcher.instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        EventDispatcher.instance = this;
        DontDestroyOnLoad(this);

        var count = Enum.GetNames(typeof(EventID)).Length;
        callbacks = new Action<object>[count];
    }

    // Register to listen for eventID, callback will be invoke when event with eventID be raise
    public void RegisterListener(EventID eventID, Action<object> callback)
    {
        callbacks[(int)eventID] += callback;
    }

    // Post event, this will notify all listener which register to listen for eventID
    public void PostEvent(EventID eventID, object param = null)
    {
        if (callbacks[(int)eventID] != null)
        {
            callbacks[(int)eventID](param);
        }
        else Debug.LogWarning(eventID + " null");
    }

    // Use for Unregister, not listen for an event anymore.
    public void RemoveListener(EventID eventID, Action<object> callback)
    {
        callbacks[(int)eventID] -= callback;
    }
}


/// An Extension class, declare some "shortcut" for using EventDispatcher
public static class EventDispatcherExtension
{
    /// Use for registering with EventDispatcher
    public static void RegisterListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
        EventDispatcher.instance.RegisterListener(eventID, callback);
    }
    /// Post event with param
    public static void PostEvent(this MonoBehaviour sender, EventID eventID, object param)
    {
        Debug.Log("sender: " + sender.name + " + " + eventID);
        EventDispatcher.instance.PostEvent(eventID, param);
    }

    /// Post event with no param (param = null)
    public static void PostEvent(this MonoBehaviour sender, EventID eventID)
    {
        Debug.Log("sender: " + sender.name + " + " + eventID);
        EventDispatcher.instance.PostEvent(eventID, null);
    }

    public static void RemoveListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
        EventDispatcher.instance.RemoveListener(eventID, callback);
    }

}
