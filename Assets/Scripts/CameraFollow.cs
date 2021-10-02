using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start()
    {
        transform.position = target.position + offset;
    }

    void LateUpdate()
    {
        Vector3 perpendicularOffset = Vector3.Cross(Vector3.up, target.forward).normalized;
        perpendicularOffset *= offset.x;
        perpendicularOffset.y = offset.y;

        Vector3 desiredPosition =  target.position + perpendicularOffset;
        Vector3 smoothPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;

        transform.LookAt(target);
    }
}
