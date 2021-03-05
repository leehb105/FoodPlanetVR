using UnityEngine;

public class LoadBar : MonoBehaviour
{
    [Range(0,1)]
    public float progress;

    private static Quaternion lastRotation;

    void Start()
    {
        // Start with the same rotation as in the previous scene. 
        transform.rotation = lastRotation;
    }

    void Update()
    {
        // Rotates the circle according to the level loading progress.
        transform.Rotate(0, 0, progress, Space.Self); 
    }

    // Save load bar rotation before next level is loaded.
    public void saveRotation()
    {
        lastRotation = transform.rotation;
    }

}
