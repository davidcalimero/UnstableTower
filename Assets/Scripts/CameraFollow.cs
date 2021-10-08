using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public HasDied player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start()
    {
        transform.position = player.transform.position + offset;
    }

    void FixedUpdate()
    {
        if(!player.hasDied)
        {
            Vector3 perpendicularOffset = Vector3.Cross(Vector3.up, player.transform.forward).normalized;
            perpendicularOffset *= offset.x;
            perpendicularOffset.y = offset.y;

            Vector3 desiredPosition =  player.transform.position + perpendicularOffset;
            Vector3 smoothPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
        }

        transform.LookAt(player.transform);

        transform.position = new Vector3(transform.position.x,  Mathf.Round(transform.position.y * 100.0f) * 0.01f, transform.position.z);
    }
}
