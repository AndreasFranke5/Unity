using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] powerupPrefabs;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;

    public float powerupSpawnInterval = 10.0f; // Time interval between powerup spawns
    public float powerupLifetime = 7.0f; // Time until a powerup disappears if not picked up

    void Start()
    {
        SpawnEnemyWave(waveNumber);
        StartCoroutine(SpawnPowerupRoutine());
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private UnityEngine.Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new UnityEngine.Vector3(spawnPosX, 0, spawnPosZ);
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        while (true)
        {
            SpawnRandomPowerup(); // Spawn a random powerup
            yield return new WaitForSeconds(powerupSpawnInterval); // Wait before spawning the next powerup
        }
    }

    private void SpawnRandomPowerup()
    {
        int randomIndex = Random.Range(0, powerupPrefabs.Length);
        GameObject powerup = Instantiate(powerupPrefabs[randomIndex], GenerateSpawnPosition(), powerupPrefabs[randomIndex].transform.rotation);

        // Destroy the powerup after a certain lifetime if not picked up
        Destroy(powerup, powerupLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length; 
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

}
