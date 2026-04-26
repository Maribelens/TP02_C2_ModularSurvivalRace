using UnityEngine;

public class CarController : MonoBehaviour
{
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
    [SerializeField] private FuelSystem fuelSystem;

    [Header("References")]
    [SerializeField] private CarInput carInput;

    private Rigidbody rb;
    private bool hasFuel = true;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        if (rb != null)
        rb.mass = carConfig.weight;

        if(fuelSystem == null)
        fuelSystem = GetComponent<FuelSystem>();

        if (carInput == null)
            carInput = GetComponent<CarInput>();
    }

    private void Start()
    {
        if(fuelSystem != null)
        fuelSystem.OnFuelEmpty += HandleNoFuel;

        SetupWheelFriction(frontRight);
        SetupWheelFriction(frontLeft);
        SetupWheelFriction(backRight);
        SetupWheelFriction(backLeft);
    }

    private void FixedUpdate()
    {
        if (carInput == null || rb == null) return;

        float speed = GetSpeed();

        float accelerationInput = hasFuel ? carInput.AccelerationInput : 0f;
        float steeringInput = carInput.SteeringInput;
        float brakeInput = carInput.BrakeInput;

        float motorForce = accelerationInput * carConfig.motorForce;
        float brakeForce = brakeInput * carConfig.brakeForce;
        float steerAngle = steeringInput * carConfig.directionAngle;

        //Reducir aceleracion en velocidad alta
        float speedFactor = Mathf.Clamp01(1 - (speed / carConfig.maxSpeed));
        float finalMotor = motorForce * speedFactor;

        //Reducir giro en velocidad alta
        float steerFactor = Mathf.Clamp01(1 - (speed / carConfig.maxSpeed));
        float finalSteer = steerAngle * steerFactor;

        //Motor
        frontRight.motorTorque = finalMotor;
        frontLeft.motorTorque = finalMotor;
        backRight.motorTorque = finalMotor;
        backLeft.motorTorque = finalMotor;

        //Direccion
        frontRight.steerAngle = finalSteer;
        frontLeft.steerAngle = finalSteer;

        //Freno
        frontRight.brakeTorque = brakeForce;
        frontLeft.brakeTorque = brakeForce;
        backRight.brakeTorque = brakeForce;
        backLeft.brakeTorque = brakeForce;

        //Visuales
        SyncWheel(frontRight, visualFrontRight);
        SyncWheel(frontLeft, visualFrontLeft);
        SyncWheel(backRight, visualBackRight);
        SyncWheel(backLeft, visualBackLeft);
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

        // Traccion (forward)
        forward.extremumSlip = 0.4f;
        forward.extremumValue = 1.2f;
        forward.asymptoteSlip = 0.8f;
        forward.asymptoteValue = 0.9f;
        forward.stiffness = 1.5f;

        // Agarre lateral (clave anti-derrape)
        sideways.extremumSlip = 0.2f;
        sideways.extremumValue = 1.5f;
        sideways.asymptoteSlip = 0.5f;
        sideways.asymptoteValue = 1.2f;
        sideways.stiffness = 2.0f;

        wheel.forwardFriction = forward;
        wheel.sidewaysFriction = sideways;
    }

    private void HandleNoFuel()
    {
        hasFuel = false;
    }
}
