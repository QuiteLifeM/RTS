using System;
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

    public void SetPosition(Vector3 newPosition)
    {
        gameObject.SetActive(true);
        transform.position = newPosition;
    }
}
