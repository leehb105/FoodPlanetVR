using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Smooth camera that follows a player.
    void LateUpdate()
    {
        // Limits camera movement for a certain range.
        if(target.position.x < 0 && transform.position.x > -0.25f || target.position.x > 0 && transform.position.x < 0.25f || target.position.x == 0)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
        }
    }
}
