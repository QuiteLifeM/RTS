using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycaster : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private BaseFlag _baseFlag;
    
    private Camera _camera;
    private Base _currentBase;

    private void Start() => _camera = Camera.main;

    private void Update() => CreateRaycast();

    private void CreateRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                if (hit.collider.TryGetComponent(out Plane plane))
                {
                    _currentBase.MoveFlag(hit.point);
                }
                
                if (hit.collider.gameObject.TryGetComponent(out Base newBase))
                {
                    newBase.SetIsTouch();
                    _currentBase = newBase;
                }
                // Debug.Log("точка коллизии" + hit.point + hit.collider.gameObject.name);
            }
        }
    }
}