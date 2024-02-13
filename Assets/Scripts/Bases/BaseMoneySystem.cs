using System;
using UnityEngine;

public class BaseMoneySystem : MonoBehaviour
{
    [SerializeField] private int _unitPrice;
    [SerializeField] private int _basePrice;

    public int ResourcesCount { get; private set; }
    public bool CanCreateUnit => ResourcesCount >= _unitPrice;
    public bool CanBuildBase => ResourcesCount >= _basePrice;

    public event Action BotAccumulated;
    public event Action BaseAccumulated;

    public void AddResource() => ResourcesCount++;

    public void BuyBase()
    {
        if (CanBuildBase)
        {
            ResourcesCount -= _basePrice;
            BaseAccumulated?.Invoke();
        }
    }

    public void BuyUnit()
    {
        if (CanCreateUnit)
        {
            ResourcesCount -= _unitPrice;
            BotAccumulated?.Invoke();
        }
    }
}