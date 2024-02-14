using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private int _startUnitCount = 2;

    public event Action<Unit> UnitSpawned; 

    private void Start()
    {
        for (int i = 0; i < _startUnitCount; i++)
        {
            SpawnUnit();
        }
    }

    public void SpawnUnit()
    {
        Unit newUnit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
        UnitSpawned?.Invoke(newUnit);
    }
}
