using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class EventBus<T, TParam> where T : struct, System.Enum
{
    // 이벤트 버스, 인수 1개

    public readonly static IDictionary<T, UnityEvent<TParam>> events = new Dictionary<T, UnityEvent<TParam>>();

    public static void Subscribe(T type, UnityAction<TParam> action)
    {
        if (events.TryGetValue(type, out UnityEvent<TParam> thisEvent))
        {
            thisEvent.AddListener(action);
        }
        else
        {
            thisEvent = new UnityEvent<TParam>();
            events.Add(type, thisEvent);
            thisEvent.AddListener(action);
        }
    }

    public static void Unsubscribe(T type, UnityAction<TParam> action)
    {
        if (events.TryGetValue(type, out UnityEvent<TParam> thisEvent))
        {
            thisEvent.RemoveListener(action);
        }
    }

    public static void Publish(T type, TParam value)
    {
        if (events.TryGetValue(type, out UnityEvent<TParam> thisEvent))
        {
            thisEvent.Invoke(value);
        }
    }
}

public class EventBus<T> where T : struct, System.Enum
{
    // 이벤트 버스, 인수 없음

    public readonly static IDictionary<T, UnityEvent> events = new Dictionary<T, UnityEvent>();


    public static void Subscribe(T type, UnityAction action)
    {
        if (events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent.AddListener(action);
        }
        else
        {
            thisEvent = new UnityEvent();
            events.Add(type, thisEvent);
            thisEvent.AddListener(action);
        }
    }

    public static void UnSubscribe(T type, UnityAction action)
    {
        if (events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent.RemoveListener(action);
        }
    }


    public static void Publish(T type)
    {
        if (events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}

