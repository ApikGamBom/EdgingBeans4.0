using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamScript : MonoBehaviour
{
    public float sensitivity = 1.0f;
    public float smoothTime = 0.2f;
    public Vector2 pitchLimit = new Vector2(-5, 5);
    public Vector2 yawLimit = new Vector2(-5, 5);

    private Vector2 currentRotation;
    private Vector2 rotationVelocity;

    void Start()
    {
        // Initialize currentRotation to the camera's initial rotation
        currentRotation = new Vector2(transform.localEulerAngles.x, transform.localEulerAngles.y);
        rotationVelocity = Vector2.zero;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * sensitivity;

        Vector2 desiredRotation = new Vector2(
            Mathf.Clamp(currentRotation.x + mouseY, pitchLimit.x, pitchLimit.y),
            Mathf.Clamp(currentRotation.y + mouseX, yawLimit.x, yawLimit.y));

        currentRotation.x = Mathf.SmoothDamp(currentRotation.x, desiredRotation.x, ref rotationVelocity.x, smoothTime);
        currentRotation.y = Mathf.SmoothDamp(currentRotation.y, desiredRotation.y, ref rotationVelocity.y, smoothTime);

        transform.localEulerAngles = new Vector3(currentRotation.x, currentRotation.y, 0);
    }
}
