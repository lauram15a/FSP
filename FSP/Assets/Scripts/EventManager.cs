using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Int3Event : UnityEvent<int, int, int> { }

public class EventManager : MonoBehaviour
{
    #region Singleton

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

    #endregion

    public Int3Event UpdateBulletsEvent = new Int3Event();
    public UnityEvent NewGunEvent = new UnityEvent();

}
