using System.Collections.Generic;
using UnityEngine;

public class ObstacleLineSpawner : MonoBehaviour
{
    [Range(0, 4)]
    public int minObstacles, maxObstacles;
    [Range(0.2f, 0.8f)]
    public float spawnPlace;
    [Range(0, 1.5f)]
    public float randomizeObstaclesOffest;
    [Range(0, 1.0f)]
    public float coinSpawnRate;

    public static ObstacleLineSpawner instance;

    private int lineIndex;

    void Start()
    { 
        instance = this;
        // Spawn line when game starts.
        SpawnLine();
    }

    // Used to spawn obstacle line.
    public void SpawnLine()
    {
        // Create new line gameobject.
        GameObject line = new GameObject("Line-" + lineIndex);
        // Make line gameobject child of current gameobject.
        line.transform.parent = transform;
        // Add ObstaclesLine script to the gameobject.
        line.AddComponent<ObstaclesLine>();
        // Start counting when player reaches first line of rocks.
        if(lineIndex > 1)
        {
            // Increase player score.
            IncreaseScore();
        }
        // Increase line index.
        lineIndex++;
    }

    // Increase player score by one.
    private void IncreaseScore()
    {
        Score.SetAmount(Score.GetAmount() + 1);
    }
}
