using System;
using UnityEngine;

public class SpeedProvider : MonoBehaviour
{
    private Rigidbody rb;
    public float SpeedKmh { get; private set; }

    [SerializeField] private float smoothing = 5f;

    public event Action<float> OnSpeedChanged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float rawSpeed = rb.linearVelocity.magnitude * 3.6f;

        // Suavizado (evita vibraciones)
        SpeedKmh = Mathf.Lerp(SpeedKmh, rawSpeed, Time.deltaTime * smoothing);

        OnSpeedChanged?.Invoke(SpeedKmh);
    }
}