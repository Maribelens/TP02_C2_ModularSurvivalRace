using UnityEngine;

public class Mine : MonoBehaviour
{
    public float damage = 25f;
    public GameObject explosionVFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<IDamageable>()?.TakeDamage(damage);

            Instantiate(explosionVFX, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
