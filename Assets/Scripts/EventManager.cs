using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action _Jump;
    public static event Action _Drop;
    public static event Action _Grab;
    public static event Action _Right;
    public static event Action _Left;

    public void jump()
    {
        _Jump?.Invoke();
    }

    public void drop()
    {
        _Drop?.Invoke();
    }

    public void grab()
    {
        _Grab?.Invoke();
    }

    public void right()
    {
        _Right?.Invoke();
    }

    public void left()
    {
        _Left?.Invoke();
    }
}
