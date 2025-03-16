using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionHolder", menuName = "ScriptableObjects/ActionHolder", order = 1)]
public class ActionHolder : ScriptableObject
{
    List<Action> callbacks;

    private void OnEnable()
    {
        callbacks = new();
    }

    public Action Subscribe(Action callback)
    {
        callbacks.Add(callback);
        return () => callbacks.Remove(callback);
    }

    public void Invoke()
    {
        foreach (var callback in callbacks) callback.Invoke();
    }
}
