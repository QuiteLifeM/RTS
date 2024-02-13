using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBaseBuilder : MonoBehaviour
{
    [SerializeField] private Base _base;

    private BaseFlag _flagPosition;

    public event Action<Base> BaseBuilt;

    private void Update()
    {
        if (_flagPosition == null)
        {
            return;
        }
        
        if (transform.position == _flagPosition.transform.position)
        {
            BuildBase(_flagPosition);
            _flagPosition = null;
        }
    }

    public void SetFlagPosition(BaseFlag flagPosition) => _flagPosition = flagPosition;

    private void BuildBase(BaseFlag flagPosition)
    {
        Base newBase = Instantiate(_base, flagPosition.transform.position, Quaternion.identity);
        _flagPosition.Clear();
        BaseBuilt?.Invoke(newBase);
    }
}
