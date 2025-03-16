using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataHolder", menuName = "ScriptableObjects/PendulumData", order = 1)]
public class PendulumData : ScriptableObject
{
    List<Action<Vector2>> positionSubs = new();

    public Vector2 Center { get; set; }

    Vector2 position = Vector2.zero;            // in the x-y plane
    public Vector2 Position
    {
        get { return position; }
        set
        {
            position = value;
            foreach (Action<Vector2> subscriber in positionSubs) subscriber.Invoke(value);
        }
    }

    void OnEnable()
    {
        position = Vector2.zero;
    }

    public Action SubscribeToPosition(Action<Vector2> callback)
    {
        positionSubs.Add(callback);
        return () => positionSubs.Remove(callback);
    }
}
