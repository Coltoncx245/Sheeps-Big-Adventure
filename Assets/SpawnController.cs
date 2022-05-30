using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public Bounds spawnArea;
    public GameObject enemy;
    
    public int startEnemiesToSpawn = 1;
    public int respawnInterval = 15;
    public bool increaseWithTime;
    
    public Stopwatch respawnStopwatch = new Stopwatch();
    public Timer respawnTimer = new Timer();

    private int count = 1;

    void Awake()
    {
        SpawnEnemies(startEnemiesToSpawn);
    }
    
    void Start()
    {
        respawnTimer.Start(respawnInterval);
        respawnStopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (increaseWithTime)
        {
            if (respawnTimer.Done())
            {
                SpawnEnemies(startEnemiesToSpawn + count);
                count++;
                respawnTimer.Reset();
                respawnTimer.Start(respawnInterval);
            }
            
        }
        else
        {
            if (respawnTimer.Done())
            {
                SpawnEnemies(startEnemiesToSpawn);
                respawnTimer.Reset();
                respawnTimer.Start(respawnInterval);
            }

        }
       
    }

    void SpawnEnemies(int n)
    {
        Vector3 spawnPoint;
        for (int i = 0; i < n; i++)
        {
            spawnPoint = GetRandomPoint();
            Instantiate(enemy, spawnPoint, Quaternion.Euler(0f, 0f, 0f));
            
        }
    }

    Vector3 GetRandomPoint()
    {
        float randomX = Random.Range(-spawnArea.extents.x + 1, spawnArea.extents.x - 1);
        float randomZ = Random.Range(-spawnArea.extents.z + 1, spawnArea.extents.z - 1);
        return new Vector3(randomX, 10, randomZ);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawnArea.center, spawnArea.size);
        
    }
}
