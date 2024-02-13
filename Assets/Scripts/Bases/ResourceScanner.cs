using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _radius;

    [SerializeField] private LayerMask _layerMask;

    private readonly Collider[] _overlappedColliders = new Collider[20];
    private List<ResourceCell> _resourceCells = new List<ResourceCell>();

    public event Action<ResourceCell> ResourceCellFound;

    public void Scan()
    {
        Vector3 center = transform.position;
        Physics.OverlapSphereNonAlloc(center, _radius, _overlappedColliders, _layerMask);

        foreach (var collider in _overlappedColliders)
        {
            if (collider == null)
            {
                continue;
            }
            
            if (collider.gameObject.TryGetComponent(out ResourceCell resourceCell))
            {
                if (resourceCell.IsEmpty == false && resourceCell.IsReserved == false)
                {
                    ResourceCellFound?.Invoke(resourceCell);
                    return;
                }
            }
        }
    }
}