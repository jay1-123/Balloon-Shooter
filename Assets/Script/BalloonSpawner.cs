using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public int poolSize = 10;
    private List<GameObject> balloonPool;
    private float nextSpawnTime;
    public float spawnRate = 2f;
    public Vector2 spawnAreaMin, spawnAreaMax;

    void Start()
    {
        balloonPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject balloon = Instantiate(balloonPrefab);
            balloon.SetActive(false);
            balloonPool.Add(balloon);
        }
    }

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            GameObject balloon=GetPooledBalloon();
            balloon.transform.position = GetRandomSpawnPosition();
            nextSpawnTime = Time.time + spawnRate;
        }
    }


    public GameObject GetPooledBalloon()
    {
        foreach (var balloon in balloonPool)
        {
            if (!balloon.activeInHierarchy)
            {
                balloon.SetActive(true);
                return balloon;
            }
        }

        // Optionally increase the pool size if all balloons are in use
        return null;
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        return new Vector3(randomX, randomY, 0f);
    }
    
}
