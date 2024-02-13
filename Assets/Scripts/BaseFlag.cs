using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseFlag : MonoBehaviour
{
    public bool IsReserve { get; private set; }
    
    public event Action BaseFlagActivated;

    private void OnEnable() => BaseFlagActivated?.Invoke();

    public void Reserve() => IsReserve = true;

    public void Clear()
    {
        gameObject.SetActive(false);
        IsReserve = false;
    }

    public void MovePosition(Vector3 newPosition)
    {
        gameObject.SetActive(true);
        transform.position = newPosition;
    }
}
