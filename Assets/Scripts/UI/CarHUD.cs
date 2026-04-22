using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarHUD : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HealthSystem health;
    [SerializeField] private FuelSystem fuel;
    [SerializeField] private SpeedProvider speedProvider;

    [Header("UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image fuelBar;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text speedText;

    //[Header("Config")]
    //[SerializeField] private CarConfigurationSO config;

    private void OnEnable()
    {
        health.OnHealthChanged += UpdateHealthUI;
        fuel.OnFuelChanged += UpdateFuelUI;
        speedProvider.OnSpeedChanged += UpdateSpeedUI;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateHealthUI;
        fuel.OnFuelChanged -= UpdateFuelUI;
        speedProvider.OnSpeedChanged -= UpdateSpeedUI;
    }

    private void UpdateHealthUI(float current, float max)
    {
        float lerp = current / max;
        healthBar.fillAmount = lerp;
        if (healthText != null)
            healthText.text = $"{current:0}/{max:0}";
        //Ejemplo: "80/100"
    }

    private void UpdateFuelUI(float current, float max)
    {
        float lerp = current / max;
        fuelBar.fillAmount = lerp;
        //fuelBar.value = current / maxFuel;
    }

    private void UpdateSpeedUI(float speed)
    {
        speed = speedProvider.SpeedKmh;
        speedText.text = Mathf.RoundToInt(speed) + " km/h";
    }
}
