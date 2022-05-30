using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnController : MonoBehaviour
{

    public Bounds spawnArea;
    public GameObject target;

    public int startTargetsToSpawn = 1;
    public int respawnInterval = 15;


    public Stopwatch respawnStopwatch = new Stopwatch();
    public Timer respawnTimer = new Timer();

    private int count = 1;

    public bool increaseWithTime;

    void Awake()
    {
        SpawnTargets(startTargetsToSpawn);
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
                SpawnTargets(startTargetsToSpawn + count);
                count++;
                respawnTimer.Reset();
                respawnTimer.Start(respawnInterval);
            }

        }
        else
        {
            if (respawnTimer.Done())
            {
                SpawnTargets(startTargetsToSpawn);
                respawnTimer.Reset();
                respawnTimer.Start(respawnInterval);
            }

        }

    }

    void SpawnTargets(int n)
    {
        Vector3 spawnPoint;
        float rotation;
        for (int i = 0; i < n; i++)
        {
            rotation = Random.Range(0, 360);
            spawnPoint = GetRandomPoint();
            Instantiate(target, spawnPoint, Quaternion.Euler(0f, rotation, 0f));

        }
    }

    Vector3 GetRandomPoint()
    {
        float randomX = Random.Range(-spawnArea.extents.x + 1, spawnArea.extents.x - 1);
        float randomZ = Random.Range(-spawnArea.extents.z + 1, spawnArea.extents.z - 1);
        return new Vector3(randomX, 1, randomZ);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawnArea.center, spawnArea.size);

    }
}

