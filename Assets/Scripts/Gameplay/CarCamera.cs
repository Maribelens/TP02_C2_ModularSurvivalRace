using UnityEngine;

public class CarCamera : MonoBehaviour
{
    [SerializeField] private Transform firstPersonPoint;
    [SerializeField] private Transform thirdPersonPoint;
    [SerializeField] private Transform cameraTransform;

    private float smoothSpeed = 5f;
    private bool isFirstPerson = false;

    private void Awake()
    {
        Transform[] points = GetComponentsInChildren<Transform>();

        firstPersonPoint = points[1];
        thirdPersonPoint = points[2];
        cameraTransform = points[3];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;
        }
    }

    private void LateUpdate()
    {
        Transform targetPoint = isFirstPerson ? firstPersonPoint : thirdPersonPoint;

        //Interpolacion suave de posicion
        cameraTransform.position = Vector3.Lerp(
            cameraTransform.position,
            targetPoint.position,
            smoothSpeed * Time.deltaTime
        );

        //Rotacion igual al pivot 
        cameraTransform.rotation = targetPoint.rotation;
    }
}    

