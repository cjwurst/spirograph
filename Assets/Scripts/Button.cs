using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] ActionHolder holder;

    void OnMouseUpAsButton()
    {
        holder.Invoke();
    }
}
