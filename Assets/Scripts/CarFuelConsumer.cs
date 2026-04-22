using UnityEngine;

public class CarFuelConsumer : MonoBehaviour
{
    [SerializeField] private FuelSystem fuelSystem;
    [SerializeField] private CarConfigurationSO config;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        fuelSystem = GetComponentInChildren<FuelSystem>();
        //config = GetComponentInChildren<CarConfigurationSO>();
    }

    private void FixedUpdate()
    {
        float speed = rb.linearVelocity.magnitude;

        // Detectar si está acelerando
        float input = Input.GetAxis("Vertical");

        float consumption = config.idleConsumption;

        if (input > 0)
        {
            // Más velocidad + aceleración = más consumo
            consumption += config.accelerationConsumption * (speed / config.maxSpeed);
        }

        fuelSystem.Consume(consumption * Time.fixedDeltaTime);
        //uso posterior con triggers
    }
}
