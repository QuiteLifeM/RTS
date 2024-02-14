using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private BaseFlag _baseFlag;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private BaseMoneySystem _baseMoneySystem;
    [SerializeField] private BaseResourceHandler _baseResourceHandler;
    [SerializeField] private int _maxUnitCount;

    private List<Unit> _units = new List<Unit>();
    private float _time = 0f;
    private float _delay = 2f;
    private bool _isTouch = false;
    private bool _canBuildBase = false;
    private bool _isEnoughMoneyToBuildNewBase = false;
    private Coroutine _buildingNewBase;

    private void OnEnable()
    {
        _unitSpawner.UnitSpawned += OnUnitSpawned;
        _resourceScanner.ResourceCellFound += OnResourceCellFound;
        _baseResourceHandler.ResourceSent += OnResourceSent;
        _baseMoneySystem.BotAccumulated += OnBotAccumulated;
        _baseMoneySystem.BaseAccumulated += OnBaseAccumulated;
    }
    
    private void Start() => StartCoroutine(Scanning());

    private void OnDisable()
    {
        _unitSpawner.UnitSpawned -= OnUnitSpawned;
        _resourceScanner.ResourceCellFound -= OnResourceCellFound;
        _baseResourceHandler.ResourceSent -= OnResourceSent;
        _baseMoneySystem.BotAccumulated -= OnBotAccumulated;
        _baseMoneySystem.BaseAccumulated -= OnBotAccumulated;
    }

    public void AddUnit(Unit unit) => _units.Add(unit);

    public void SetIsTouch() => _isTouch = !_isTouch;

    public void SetFlag(Vector3 newPosition)
    {
        if (_isTouch == false)
        {
            return;
        }

        _baseFlag.SetPosition(newPosition);
    }

    private void OnUnitSpawned(Unit unit)
    {
        unit.Init(this);
        AddUnit(unit);
    }

    private void OnBotAccumulated()
    {
        if (_units.Count < _maxUnitCount && _baseFlag.gameObject.activeSelf == false)
        {
            _unitSpawner.SpawnUnit();
        }
    }

    private void OnResourceSent()
    {
        _baseMoneySystem.AddResource();

        if (_baseFlag.gameObject.activeSelf == false)
        {
            _baseMoneySystem.BuyUnit();
        }
        else
        {
            _baseMoneySystem.BuyBase();   
        }
    }

    private void OnResourceCellFound(ResourceCell resourceCell)
    {
        foreach (var unit in _units)
        {
            if (unit.IsBusy == false)
            {
                if (_canBuildBase && _baseFlag.gameObject.activeSelf && _baseFlag.IsReserve == false)
                {
                    _baseFlag.Reserve();
                    unit.BuildBase(_baseFlag);
                    _units.Remove(unit);
                    
                    return;
                }
                
                resourceCell.Reserve();
                unit.Dig(resourceCell);
                
                return;
            }
        }
    }
    
    private void OnBaseAccumulated() => _canBuildBase = true;

    private IEnumerator Scanning()
    {
        while (enabled)
        {
            print("Скан работает");
            yield return new WaitForSeconds(_delay);
            _resourceScanner.Scan();
        }
    }
}