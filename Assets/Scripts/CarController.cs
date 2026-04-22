//using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private float inputAcceleration; //tecla w-s
    [SerializeField] private float inputDirection; //
    [SerializeField] private float inputBrake;

    [Header("Wheels")]
    [SerializeField] private WheelCollider frontRight;
    [SerializeField] private WheelCollider frontLeft;
    [SerializeField] private WheelCollider backRight;
    [SerializeField] private WheelCollider backLeft;

    [Header("Visual")]
    [SerializeField] private Transform visualFrontRight;
    [SerializeField] private Transform visualFrontLeft;
    [SerializeField] private Transform visualBackRight;
    [SerializeField] private Transform visualBackLeft;

    [Header("Settings")]
    [SerializeField] private CarConfigurationSO carConfig;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        rb.mass = carConfig.weight;
    }

    private void Start()
    {
        SetupWheelFriction(frontRight);
        SetupWheelFriction(frontLeft);
        SetupWheelFriction(backRight);
        SetupWheelFriction(backLeft);
    }

    private void Update()
    {
        inputAcceleration = Input.GetAxis("Vertical") * carConfig.motorForce;
        inputDirection = Input.GetAxis("Horizontal") * carConfig.directionAngle;
        inputBrake = Input.GetAxisRaw("Brake") * carConfig.brakeForce;
    }

    private void FixedUpdate()
    {
        //Traccion en las 4 ruedas (Aceleracion)
        float speed = GetSpeed();
        float speedFactor = Mathf.Clamp01(1 - (speed / carConfig.maxSpeed));
        float finalMotor = inputAcceleration * speedFactor;

        frontRight.motorTorque = finalMotor;
        frontLeft.motorTorque = finalMotor;
        backRight.motorTorque = finalMotor;
        backLeft.motorTorque = finalMotor;

        //Direccion
        // Factor: 1 en baja velocidad, menor en alta
        float steerFactor = Mathf.Clamp01(1 - (speed / carConfig.maxSpeed));

        float finalSteer = inputDirection * steerFactor;
        frontRight.steerAngle = finalSteer;
        frontLeft.steerAngle = finalSteer;

        //Sincronizacion visual
        SyncWheel(frontRight, visualFrontRight);
        SyncWheel(frontLeft, visualFrontLeft);
        SyncWheel(backRight, visualBackRight);
        SyncWheel(backLeft, visualBackLeft);

        //Frenado
        frontRight.brakeTorque = inputBrake;
        frontLeft.brakeTorque = inputBrake;
        backRight.brakeTorque = inputBrake;
        backLeft.brakeTorque = inputBrake;
    }

    private float GetSpeed()
    {
        return rb.linearVelocity.magnitude * 3.6f;
    }

    private void SyncWheel(WheelCollider wheel, Transform visual)
    {
        wheel.GetWorldPose(out Vector3 pos, out Quaternion rot);
        visual.position = pos;
        visual.rotation = rot;
    }

    private void SetupWheelFriction(WheelCollider wheel)
    {
        WheelFrictionCurve forward = wheel.forwardFriction;
        WheelFrictionCurve sideways = wheel.sidewaysFriction;

        // TRACCIÓN (forward)
        forward.extremumSlip = 0.4f;
        forward.extremumValue = 1.2f;
        forward.asymptoteSlip = 0.8f;
        forward.asymptoteValue = 0.9f;
        forward.stiffness = 1.5f;

        // AGARRE LATERAL (clave anti-derrape)
        sideways.extremumSlip = 0.2f;
        sideways.extremumValue = 1.5f;
        sideways.asymptoteSlip = 0.5f;
        sideways.asymptoteValue = 1.2f;
        sideways.stiffness = 2.0f;

        wheel.forwardFriction = forward;
        wheel.sidewaysFriction = sideways;
    }
}
