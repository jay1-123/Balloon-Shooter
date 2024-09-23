
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;           // Prefab to instantiate balloons
    public int poolSize = 10;                  // Number of balloons in the pool
    private List<GameObject> balloonPool;      // The balloon pool list
    private float nextSpawnTime;               // Timer to control spawn rate
    public float spawnRate = 2f;               // Time between each balloon spawn
    public Vector2 spawnAreaMin, spawnAreaMax; // Bounds for random spawn positions

    private bool isGameOver = false;           // Game over flag
    private bool canSpawn = false;             // Flag to start spawning after delay
    public static BalloonSpawner Instance;
    public float startDelay = 0.5f;              // Delay before starting balloon spawning

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitializePool();
        StartCoroutine(StartSpawningWithDelay()); // Start coroutine to delay the spawning
    }

    private void Update()
    {
        if (isGameOver || !canSpawn) return; // Prevent spawning if game is over or delay not over

        // Check if it's time to spawn a new balloon
        if (Time.time >= nextSpawnTime)
        {
            SpawnBalloon();
            nextSpawnTime = Time.time + spawnRate; // Set next spawn time
        }
    }

    // Initialize the pool of balloons
    private void InitializePool()
    {
        balloonPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject balloon = Instantiate(balloonPrefab);
            balloon.SetActive(false);          // Start all balloons as inactive
            balloonPool.Add(balloon);
        }
    }

    // Coroutine to wait for delay before starting the balloon spawning
    private IEnumerator StartSpawningWithDelay()
    {
        yield return new WaitForSeconds(startDelay); // Wait for the specified delay

        canSpawn = true; // Enable spawning after the delay
        nextSpawnTime = Time.time + spawnRate; // Initialize spawn timer after delay
        
    }

    // Spawn a balloon from the pool
    private void SpawnBalloon()
    {
        GameObject balloon = GetPooledBalloon();
        if (balloon != null)
        {
            balloon.transform.position = GetRandomSpawnPosition();
            balloon.SetActive(true);          // Activate the balloon when spawned
        }
    }

    // Get an inactive balloon from the pool
    private GameObject GetPooledBalloon()
    {
        foreach (var balloon in balloonPool)
        {
            if (!balloon.activeInHierarchy)   // Find the first inactive balloon
            {
                return balloon;               // Return the inactive balloon
            }
        }

        // Optionally handle the case where no inactive balloons are available
        return null;
    }

    // Generate a random position within the specified spawn area
    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        return new Vector3(randomX, randomY, 0f);
    }

    // Call this method when the game is over to stop spawning and reset balloons
    public void GameOver()
    {
        isGameOver = true;                    // Stop spawning balloons
        canSpawn = false;                     // Disable spawning

        // Deactivate all active balloons
        foreach (var balloon in balloonPool)
        {
            if (balloon.activeInHierarchy)
            {
                balloon.SetActive(false);
            }
        }
    }
}

