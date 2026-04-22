using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public bool isInvulnerable = false;
    [SerializeField] private CarConfigurationSO config;
    [SerializeField] private float currentHealth;
    //[SerializeField] private float maxHealth;

    public float CurrentHealth => currentHealth;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        //maxHealth = config.maxHealth;
        currentHealth = config.maxHealth;
    }

    private void Start()
    {
        OnHealthChanged?.Invoke(currentHealth, config.maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0 || isInvulnerable) return;

        currentHealth -= amount;

        OnHealthChanged?.Invoke(currentHealth, config.maxHealth);

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }

        Debug.Log("DoDamage", gameObject);
    }

    public void Heal(float amount)
    {
        if (amount <= 0) return;

        currentHealth += amount;

        OnHealthChanged?.Invoke(currentHealth, config.maxHealth);
    }
}
