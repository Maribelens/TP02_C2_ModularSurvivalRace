using UnityEngine;

public class RampBoost : MonoBehaviour
{
    public float boostForce = 20f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);
        }
    }
}
