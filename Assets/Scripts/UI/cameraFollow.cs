using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target; // The GameObject the camera will follow
    public float smoothSpeed = 0.125f; // How smoothly the camera will follow the target
    public Vector2 offset; // The offset of the camera from the target

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position of the camera
            Vector2 desiredPosition = (Vector2)target.position + offset;

            // Smoothly move the camera towards the desired position
            Vector2 smoothedPosition = Vector2.Lerp((Vector2)transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}
