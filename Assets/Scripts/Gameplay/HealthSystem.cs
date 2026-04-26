using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public bool isInvulnerable = false;
    [SerializeField] private CarConfigurationSO config;

    [SerializeField] private float currentHealth;
    public float CurrentHealth => currentHealth;

    [SerializeField] private bool isDead;
    public bool IsDead => isDead;

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
        if (amount <= 0 || isInvulnerable || isDead) return;

        currentHealth -= amount;

        OnHealthChanged?.Invoke(currentHealth, config.maxHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            OnDeath?.Invoke();
        }

        Debug.Log("DoDamage", gameObject);
    }

    public void Heal(float amount)
    {
        if (amount <= 0) return;

        currentHealth += amount;

        if (currentHealth > config.maxHealth)
        {
            currentHealth = config.maxHealth;
        }

        OnHealthChanged?.Invoke(currentHealth, config.maxHealth);
    }
}
