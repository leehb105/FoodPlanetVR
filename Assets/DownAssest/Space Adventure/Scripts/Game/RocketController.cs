using System.Collections;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Range(0, 9.5f)]
    public float speed = 8.0f;
    [Range(0, 2.0f)]
    public float changeLanesSpeed = 2.0f;

    public Transform parts;

    public GameObject crashedParticles;
    
    private int lane = 0;
    private bool changingLanes;
    private float duration;
    private Vector3 startPos, endPos;
    private bool paused;

    private Animation anim;
    private AudioSource audioSource;

    void Start()
    {
        anim = this.GetComponent<Animation>();
        audioSource = this.GetComponent<AudioSource>();
        // Load rocket.
        LoadRocket();
        // Reset rocket position.
        UpdatePosition();
        // Set obstacles speed.
        UpdateObstaclesSpeed(speed);
    }

    void Update()
    {
        // If game isn't paused.
        if(!paused)
        {
            // If player hasn't reached endPos.
            if(transform.position != endPos)
            {
                // Then player is changing lines.
                changingLanes = true;
            }
            else
            {
                // Or player has already chanded line.
                changingLanes = false;
            }

            // If user presses A or Left Arrow.
            if ((Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow))) 
            {
                // Move player left.
                MoveLeft();
            }

            // If user presses D or Right Arrow.
            if ((Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow))) 
            {
                // Move player right.
                MoveRight();
            }

            // If player is changing lines.
            if(changingLanes)
            {
                // Player speed is higher than 0.
                if(changeLanesSpeed != 0)
                {
                    // How long player will change lanes.
                    duration += Time.deltaTime/((2-changeLanesSpeed)/10);
                    // Move player to the endPos.
                    transform.position = Vector3.Lerp(startPos, endPos, duration);
                }
            }
        }
    }

    // User pressed button to move left.
    public void MoveLeft()
    {
        // If it's not the first lane.
        if(lane > -2)
        {
            // Play move left animation.
            anim.Play("Move-Left");
            // Play moving sound.
            audioSource.Play();
            // Change lane and update position.
            lane--;
            UpdatePosition();             
        }       
    }

    // User pressed button to move right.
    public void MoveRight()
    {
        // If it's not the last lane.
        if(lane < 2)
        {
            // Play move right animation.
            anim.Play("Move-Right");
            // Play moving sound.
            audioSource.Play();
            // Change lane and update position.
            lane++;
            UpdatePosition();   
        }     
    }

    // Load player rocket.
    private void LoadRocket()
    {
        // Cycle between all rocket parts.
        foreach(Transform part in parts)
        {
            if(part.name != "Base")
            {
                // Get value if rocket part is added.
                bool partAdded = PlayerPrefs.GetInt("PartAdded-" + part.name, 0) == 1 ? true : false;
                // Set rocket part gameobject state to active or disabled according to partAdded value.
                part.gameObject.SetActive(partAdded);
            }
        }
    }

    // Update player position.
    private void UpdatePosition()
    {
        // Start from the beginning.
        duration = 0;
        // Set current player position.
        startPos = transform.position;
        // Set player destination position.
        endPos = new Vector3(lane, transform.position.y, transform.position.z);
    }

    // Update obstacle speed.
    private void UpdateObstaclesSpeed(float obstaclesSpeed)
    {
        ObstaclesLine.speed = obstaclesSpeed;
    }

    // Pause player controller.
    public void Pause()
    {
        paused = true;
        // Set obstacle speed to 0.
        UpdateObstaclesSpeed(0);
    }

    // Resume player controller.
    public void Resume()
    {
        paused = false;
        // Reset obstacle speed.
        UpdateObstaclesSpeed(speed);
    }

    // If player crashed to obstacle.
    public void Crashed()
    {
        // Pause player controller.
        Pause();
        // Enable crash particles
        crashedParticles.SetActive(true);
        
        #if UNITY_ANDROID || UNITY_IOS
            // If vibration is enabled then vibrate device.
            if(Settings.GetSetting("Vibration"))
            {
                Handheld.Vibrate();
            }
        #endif
    }
}
