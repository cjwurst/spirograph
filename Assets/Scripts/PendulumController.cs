using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PendulumController : MonoBehaviour
{
    [SerializeField] PendulumData dataHolder;
    [SerializeField] PendulumParameters parameters;

    [Space(10), SerializeField] ActionHolder resetHolder;

    Vector2 deltaPos = new(0f, 0f);

    void Start()
    {
        GetComponent<SpriteRenderer>().color = parameters.BackgroundColor;
        transform.localScale = new Vector3(parameters.Scale, parameters.Scale, 1f);
        resetHolder.Subscribe(ResetBob);
        ResetBob();
    }

    void ResetBob()
    {
        dataHolder.Center = transform.position;
        dataHolder.Position = Vector2.zero;
        deltaPos = Vector2.zero;
    }

    void Update()
    {
        var pos = dataHolder.Position;
        var dir = -1f*pos.normalized;
        var r = pos.magnitude;
        var accel = parameters.Gravity * Mathf.Cos(Mathf.Asin(r)) * dir;
        deltaPos += Time.deltaTime*accel;
        deltaPos *= parameters.DampingCoeff;
        dataHolder.Position += Time.deltaTime * deltaPos * parameters.SpeedCoeff;

        if (r > 0.5f)
        {
            if (pos.x == 0)
            {
                dataHolder.Position = new Vector2(0f, Mathf.Sign(pos.y));
                deltaPos.y *= -1f;
            }
            if (pos.y == 0)
            {
                dataHolder.Position = new Vector2(Mathf.Sign(pos.x), 0f);
                deltaPos.x *= -1f;
            }

            pos = dataHolder.Position;
            var theta = Mathf.Atan(pos.y / pos.x);
            var cos = Mathf.Cos(theta + Mathf.PI / 2f);
            var sin = Mathf.Sin(theta + Mathf.PI / 2f);
            deltaPos = new Vector2(deltaPos.x * cos - deltaPos.y * sin, deltaPos.x * sin + deltaPos.y * cos);
            dataHolder.Position = 0.5f*dataHolder.Position.normalized;
        }
    }

    void OnMouseUpAsButton()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dataHolder.Position = mousePos/parameters.Scale;
    }
}
