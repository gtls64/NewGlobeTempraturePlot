using UnityEngine;
using System.IO;

public class MouseRotation : MonoBehaviour
{
    public Transform target; // The object to focus on
    public float rotationSpeed = 2.0f;
    public float zoomSpeed = 2.0f;
    public float minZoomDistance = 2.0f;
    public float maxZoomDistance = 10.0f;

    private Vector3 lastMousePosition;

    void Update()
    {
        // Check for mouse button input
        if (Input.GetMouseButton(0))
        {
            // Calculate the mouse input for rotation
            float deltaX = Input.GetAxis("Mouse X") * rotationSpeed;
            float deltaY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Rotate the camera rig around the target object
            transform.RotateAround(target.position, Vector3.up, deltaX);
            transform.RotateAround(target.position, transform.right, -deltaY);

            // Store the current mouse position
            lastMousePosition = Input.mousePosition;
        }

        // Check for mouse wheel input for zoom
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log("Scroll Wheel Input: " + scrollWheelInput);

        if (scrollWheelInput != 0)
        {
            // Calculate the new distance from the target
            float distance = Vector3.Distance(transform.position, target.position);
            distance = Mathf.Clamp(distance - scrollWheelInput * zoomSpeed, minZoomDistance, maxZoomDistance);

            // Move the camera rig towards or away from the target
            transform.position = target.position + (transform.position - target.position).normalized * distance;
        }
    }
}