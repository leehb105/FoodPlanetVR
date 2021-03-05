using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed;
    private bool crashed;
    private AudioSource audioSource;

    void Start()
    {
        // Randomly set the rotation speed.
        speed = Random.Range(-1.0f, 1.0f);
        audioSource = this.GetComponent<AudioSource>();
        crashed = false;
    }

    void Update()
    {
        // Rotate obstacle.
        transform.Rotate(0, 0, speed, Space.Self); 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Check if did not crashed already.
        if(!crashed)
        {
            // Checks if obstacle collides with one of the player colliders.
            if(col.name == "Front" || col.name == "Back")
            {
                // Play crash sound.
                audioSource.Play();
                // Reset player speed.
                speed = 0;
                // Open game over window.
                GameOver.instance.Crashed();
                // Set crashed value to true so that this function would not be called again.
                crashed = true;
            }
        }
    }
}
