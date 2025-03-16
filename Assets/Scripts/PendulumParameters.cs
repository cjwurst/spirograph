using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Parameters", menuName = "ScriptableObjects/PendulumParameters", order = 1)]
public class PendulumParameters : ScriptableObject
{
    [SerializeField, Range(0.5f, 10f)] float scale = 1f;
    public float Scale { get { return scale; } }

    [SerializeField] Color backgroundColor;
    public Color BackgroundColor { get { return backgroundColor; } }

    [SerializeField] Color bobColor;
    public Color BobColor { get { return bobColor; } }

    [Space(10), SerializeField] float gravity = 10f;           // acceleration due to gravity
    public float Gravity { get { return gravity; } }

    [Space(10), SerializeField, Range(0f, 1f)] float dampingCoeff = 1f;
    public float DampingCoeff { get { return dampingCoeff; } }

    [SerializeField, Range(0f, 1f)] float speedCoeff = 1f;
    public float SpeedCoeff { get { return speedCoeff; } }

    
}
