using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public new Transform transform => _transform;
    private Transform _transform;

    protected virtual void Awake()
    {
        TryGetComponent(out _transform);
    }
}