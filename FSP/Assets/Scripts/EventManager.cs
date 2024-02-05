using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Int3Event : UnityEvent<int, int, int> { }
public class StringEvent : UnityEvent<string> { }

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else 
        {
            Destroy(this);
        }
    }

    public Int3Event UpdateBulletsEvent = new Int3Event();
    public StringEvent UpdateRechargingEvent = new StringEvent();
    public UnityEvent NewWeaponEvent = new UnityEvent();

}
