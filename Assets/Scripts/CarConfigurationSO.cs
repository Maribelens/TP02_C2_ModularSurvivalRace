using UnityEngine;

[CreateAssetMenu(fileName = "CarConfig", menuName = "Car/Configuration")]
public class CarConfigurationSO : ScriptableObject
{
    [Header("Movement")]
    public float motorForce = 200f;
    public float directionAngle = 45f;
    public float brakeForce = 500f;

    [Header("Stats")]
    public float maxSpeed = 50f;
    public float weight = 1500f;

    [Header("Health & Fuel")]
    public float maxHealth = 100f;
    public float maxFuel = 100f;
}
