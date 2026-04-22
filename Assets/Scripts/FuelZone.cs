using UnityEngine;

public class FuelZone : MonoBehaviour
{
    [SerializeField] private float refuelRate = 15f; // combustible por segundo
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        FuelSystem fuel = rb.GetComponentInChildren<FuelSystem>();
        //FuelSystem fuel = other.GetComponentInChildren<FuelSystem>();

        if (fuel != null)
        {
            fuel.Refuel(refuelRate * Time.deltaTime);
        }
    }
}
