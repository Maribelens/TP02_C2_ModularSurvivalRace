using UnityEngine;

[CreateAssetMenu(fileName = "CarConfig", menuName = "Car/Configuration")]
public class CarConfigurationSO : ScriptableObject
{
    [Header("Movement")]
    public float motorForce = 200f;
    public float directionAngle = 45f;
    public float brakeForce = 500f;

    [Header("Stats")]
    public float maxSpeed = 100f;
    public float weight = 1500f;

    [Header("Health")]
    public float maxHealth = 100f;

    [Header("Fuel")]
    public float maxFuel = 100f;
    public float idleConsumption = 0.01f;    
    public float accelerationConsumption = 0.05f;
}
