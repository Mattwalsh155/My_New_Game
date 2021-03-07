using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Spawn location variables
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private float xScreenUnits;
    private float yScreenUnits;
    private float padding = 0.5f;

    //Spawn rate variables
    private float xRand;
    private float yRand;
    [SerializeField] private float spawnRate = 0f;
    [SerializeField] private int spawnCount = 0;
    private Vector2 whereToSpawn;
    private float secondWaveTime = 0f;
    private float secondWaveSpawnTime = 0f;

    //Object references
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject[] twoEnemies;

    // Start is called before the first frame update
    void Start()
    {
        SetUpSpawnBoundaries();
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        DetermineSpawn();
    }

    private void SetUpSpawnBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.3f, 0)).y;

        xScreenUnits = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yScreenUnits = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void DetermineSpawn()
    {
        secondWaveTime += Time.deltaTime;
        spawnRate -= Time.deltaTime;
        if (spawnRate <= 0 && secondWaveTime < secondWaveSpawnTime)
        {
            SpawnEnemy();
        }
        else if (spawnRate <= 0 && secondWaveTime >= secondWaveSpawnTime)
        {
            SpawnEnemies();
        }

    }

    private void SpawnEnemies()
    {
        xRand = Random.Range(xMin + padding, xMax - padding);
        whereToSpawn = new Vector2(xRand, transform.position.y);
        Instantiate(twoEnemies[Random.Range(0, twoEnemies.Length)], whereToSpawn, Quaternion.identity);
        spawnCount++;
        spawnRate = 3.5f - spawnCount * 0.1f;
        if (spawnRate <= 0.5f) { spawnRate = 0.5f; }
    }

    private void SpawnEnemy()
    {
        //Spawn an enemy
        xRand = Random.Range(xMin, xMax);
        whereToSpawn = new Vector2(xRand, transform.position.y);
        Instantiate(enemyPrefab, whereToSpawn, Quaternion.identity);
        spawnRate = 3.5f - spawnCount * 0.1f;
        if (spawnRate <= 0.1f) { spawnRate = 0.1f; }
    }
}
