using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] PendulumParameters parameters;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = parameters.BackgroundColor;
    }
}
