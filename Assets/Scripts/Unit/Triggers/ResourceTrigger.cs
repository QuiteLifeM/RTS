using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTrigger : MonoBehaviour
{
    public event Action<ResourceCell> ResourceCellFound;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ResourceCell resourceCell))
        {
            ResourceCellFound?.Invoke(resourceCell);
        }
    }
}
