using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    public GameObject[] animalPrefabs;
    public float xSpawnRange = 20;
    public float zSpawnPos = 20;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, spawnInterval);
    }


    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnpos = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), 0, zSpawnPos);
        Instantiate(animalPrefabs[animalIndex], spawnpos, animalPrefabs[animalIndex].transform.rotation);
    }
}
