using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMover _mover;
    [SerializeField] private UnitCollector _collector;
    [SerializeField] private UnitBaseBuilder _unitBaseBuilder;

    private Base _base;

    public bool IsBusy { get; private set; }

    private void OnEnable()
    {
        _collector.ResourceSent += OnResourceSent;
        _collector.ResourceCollected += OnResourceCollected;
        _unitBaseBuilder.BaseBuilt += OnBaseBuilt;
    }

    private void Awake()
    {
        IsBusy = false;
    }

    private void OnDisable()
    {
        _collector.ResourceSent -= OnResourceSent;
        _collector.ResourceCollected -= OnResourceCollected;
        _unitBaseBuilder.BaseBuilt -= OnBaseBuilt;
    }

    public void Dig(ResourceCell resourceCell)
    {
        IsBusy = true;
        _mover.SetTarget(resourceCell.transform);
        _collector.SetResourceCell(resourceCell);
    }

    public void BuildBase(BaseFlag flagPosition)
    {
        IsBusy = true;
        _mover.SetTarget(flagPosition.transform);
        _unitBaseBuilder.SetFlagPosition(flagPosition);
    }

    public void Init(Base currentBase) => _base = currentBase;

    private void OnResourceCollected(Resource obj) => MoveToBase();

    private void MoveToBase() => _mover.SetTarget(_base.transform);

    private void OnResourceSent() => IsBusy = false;

    private void OnBaseBuilt(Base newBase)
    {
        _base = newBase;
        newBase.AddUnit(this);
        IsBusy = false;
    }
}