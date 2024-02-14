using System;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Transform _target;

    private void Update() => Move();

    public void SetTarget(Transform target) => _target = target;

    private void Move()
    {
        if (_target == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.position,
            _moveSpeed * Time.deltaTime);
    }
}