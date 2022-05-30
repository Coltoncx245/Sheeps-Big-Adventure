using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnController : MonoBehaviour
{

    public GameObject bigAmmo;
    public GameObject shield;
    public int bigAmmoToSpawn = 1;
    public int shieldToSpawn = 1;
    public Bounds itemSpawnArea;

    void Awake()
    {
        SpawnBigAmmo(bigAmmoToSpawn);
        SpawnShield(shieldToSpawn);
    }

    void SpawnBigAmmo(int n)
    {
        Vector3 spawnPoint;
        for (int i = 0; i < n; i++)
        {
            spawnPoint = GetRandomPoint();
            Instantiate(bigAmmo, spawnPoint, Quaternion.Euler(0f, 0f, 0f));
        }
    }

    void SpawnShield(int n)
    {
        Vector3 spawnPoint;
        for (int i = 0; i < n; i++)
        {
            spawnPoint = GetRandomPoint();
            Instantiate(shield, spawnPoint, Quaternion.Euler(0f, 0f, 0f));
        }
    }

    Vector3 GetRandomPoint()
    {
        float randomX = Random.Range(-itemSpawnArea.extents.x + 1, itemSpawnArea.extents.x - 1);
        float randomZ = Random.Range(-itemSpawnArea.extents.z + 1, itemSpawnArea.extents.z - 1);
        return new Vector3(randomX, 10, randomZ);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(itemSpawnArea.center, itemSpawnArea.size);

    }
}
