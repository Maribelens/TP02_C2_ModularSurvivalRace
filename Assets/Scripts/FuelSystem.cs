using System;
using UnityEngine;

public class FuelSystem : MonoBehaviour
{
    [SerializeField] private CarConfigurationSO config;
    [SerializeField] private float currentFuel;

    public float CurrentFuel => currentFuel;

    public event Action<float, float> OnFuelChanged;
    public event Action OnFuelEmpty;

    private void Awake()
    {
        currentFuel = config.maxFuel;
    }

    private void Start()
    {
        OnFuelChanged?.Invoke(currentFuel, config.maxFuel);
    }

    public void Consume(float amount)
    {
        if (amount <= 0) return;

        currentFuel -= amount;

        OnFuelChanged?.Invoke(currentFuel, config.maxFuel);

        if (currentFuel <= 0)
        {
            OnFuelEmpty?.Invoke();
        }
        Debug.Log("DoConsume", gameObject);
    }

    public void Refuel(float amount)
    {
        if (amount <= 0) return;

        currentFuel += amount;

        OnFuelChanged?.Invoke(currentFuel, config.maxFuel);
    }
}
