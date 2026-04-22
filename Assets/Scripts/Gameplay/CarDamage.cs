using UnityEngine;

public class CarDamage : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private float damageMultiplier = 0.5f;
    [SerializeField] private float minImpactThreshold = 1f;
    [SerializeField] private DamageTextPool damageTextPool;


    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthSystem = GetComponentInChildren<HealthSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            float impactForce = collision.relativeVelocity.magnitude;

            // Ignorar choques suaves
            if (impactForce < minImpactThreshold) return;

            float damage = impactForce * damageMultiplier;

            healthSystem.TakeDamage(damage);

            if (collision.contacts.Length > 0 && damageTextPool != null)
            {
                Vector3 hitPoint = collision.contacts[0].point;
                damageTextPool.ShowDamage(hitPoint, Mathf.RoundToInt(damage));
            }
            Debug.Log("Collision!");
        }
    }
}
