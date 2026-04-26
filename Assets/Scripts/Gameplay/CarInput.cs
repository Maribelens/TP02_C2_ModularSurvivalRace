using UnityEngine;

public class CarInput : MonoBehaviour
{
    public float AccelerationInput { get; private set; }
    public float SteeringInput { get; private set; }
    public float BrakeInput { get; private set; }

    private void Update()
    {
        AccelerationInput = Input.GetAxis("Vertical");
        SteeringInput = Input.GetAxis("Horizontal");
        BrakeInput = Input.GetAxisRaw("Brake");
    }
}
