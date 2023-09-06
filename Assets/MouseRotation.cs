using UnityEngine;
using System.IO;

public class MouseRotation : MonoBehaviour
{
    public Transform target; //object
    public float rotationSpeed = 2.0f;
    public float zoomSpeed = 2.0f;
    public float minZoomDistance = 2.0f;
    public float maxZoomDistance = 10.0f;

    private Vector3 lastMousePosition;

    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            //rotation
            float deltaX = Input.GetAxis("Mouse X") * rotationSpeed;
            float deltaY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // camera rig 
            transform.RotateAround(target.position, Vector3.up, deltaX);
            transform.RotateAround(target.position, transform.right, -deltaY);

            lastMousePosition = Input.mousePosition;
        }

        
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log("Scroll Wheel Input: " + scrollWheelInput);

        if (scrollWheelInput != 0)
        {
            // new distance
            float distance = Vector3.Distance(transform.position, target.position);
            distance = Mathf.Clamp(distance - scrollWheelInput * zoomSpeed, minZoomDistance, maxZoomDistance);

            
            transform.position = target.position + (transform.position - target.position).normalized * distance;
        }
    }
}