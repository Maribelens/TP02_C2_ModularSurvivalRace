using UnityEngine;

public class RepairZone : MonoBehaviour
{
    [SerializeField] private float repairRate = 1f; // vida por segundo
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        HealthSystem health = rb.GetComponentInChildren<HealthSystem>();
        //HealthSystem health = other.GetComponentInChildren<HealthSystem>();

        if (health != null)
        {
            health.Heal(repairRate * Time.deltaTime);
        }
    }
}
