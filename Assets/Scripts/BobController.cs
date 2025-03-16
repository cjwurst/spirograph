using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BobController : MonoBehaviour
{
    [SerializeField] PendulumData dataHolder;
    [SerializeField] PendulumParameters parameters;

    Action unsub;

    void Start()
    {
        unsub = dataHolder.SubscribeToPosition(p => UpdatePosition(p));

        GetComponent<SpriteRenderer>().color = parameters.BobColor;
    }

    void UpdatePosition(Vector2 pos)
    {
        pos *= parameters.Scale;
        pos += dataHolder.Center;
        transform.position = new Vector3(pos.x, pos.y, -1f);
    }

    void OnApplicationQuit()
    {
        unsub.Invoke();
    }
}
