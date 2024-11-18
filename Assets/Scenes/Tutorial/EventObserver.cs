using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventObserver<T> : MonoBehaviour
{
    public T target;
    [SerializeField] private UnityEvent action;


    public abstract void AddEvent();
    public abstract void RemoveEvent();

    protected void Action() => action.Invoke();
}
