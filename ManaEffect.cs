using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManaEffect : MonoBehaviour
{
    public event Action<GameObject> onReach;
    public Vector3 toPos = Vector3.zero;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, toPos, 15 * Time.deltaTime);
        if (Vector3.Distance(transform.position, toPos) < 1f)
        {
            onReach?.Invoke(gameObject);
        }
    }
}
