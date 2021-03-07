using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    //Enemy variables
    //Set the values in each enemy child class
    protected private float enemyStartingHealth;
    protected private float enemyMoveSpeed;
    protected private float minSpeed;
    protected private float maxSpeed;
    protected private int scoreValue;
    //
    private float explosionDuration = 1f;
    private float impactDuration = 0.3f;
    private float impactLocationAdjust = 1f;
    private float healthPickUpDropRate = 20f;
    private float coinSpawnRadius = 0.5f;

    //Object references
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject bulletImpactPrefab;
    [SerializeField] GameObject healthPickUpPrefab;
    [SerializeField] GameObject coinPrefab;

    //Cached script references
    DamageDealer damage;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetMoveSpeed();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        EnemyMove();
    }

    private void OnTriggerEnter2D(Collider2D otherGameObject)
    {
        HandleHit(otherGameObject);
    }

    private void SetMoveSpeed()
    {
        enemyMoveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    protected virtual void EnemyMove()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyMoveSpeed);
    }

    private void HandleHit(Collider2D otherGameObject)
    {
        DamageDealer damage = otherGameObject.gameObject.GetComponent<DamageDealer>();
        if (!damage) { return; }
        TakeDamage(damage);
        TriggerBulletImpactEffect();
    }

    public void TakeDamage(DamageDealer damage)
    {
        float currentHealth = enemyStartingHealth -= damage.GetDamage();
        damage.Hit();
        if (currentHealth <= 0f)
        {
            DestroyEnemy();
        }
    }

    private void TriggerBulletImpactEffect()
    {
        var impact = Instantiate(bulletImpactPrefab, new Vector3(transform.position.x, 
            transform.position.y - impactLocationAdjust, transform.position.z), Quaternion.identity);
        Destroy(impact, impactDuration);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
        HandleDamage();
        TriggerDeathEffect();
        AddScore();
        NumberOfCoins();
        CheckForDrops();
    }

    private void HandleDamage()
    {
        damage = FindObjectOfType<DamageDealer>();
    }

    private void TriggerDeathEffect()
    {
        var explode = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explode, explosionDuration);
    }

    private void AddScore()
    {
        FindObjectOfType<GameManager>().AddToScore(scoreValue);
    }

    private void NumberOfCoins()
    {
        var randNum = Random.Range(1, 4);
        for (int i = 0; i < randNum; i++)
        {
            SpawnCoins();
        }
    }

    private void SpawnCoins()
    {
        Instantiate(coinPrefab, new Vector3(transform.position.x + Random.Range(-coinSpawnRadius, coinSpawnRadius),
            transform.position.y + Random.Range(-coinSpawnRadius, coinSpawnRadius), transform.position.z), Quaternion.identity);
    }

    //Probably convert to a switch in the future
    private void CheckForDrops()
    {
        var randNum = Random.Range(0f, 100f);
        if (randNum <= healthPickUpDropRate)
        {
            GenerateHealthPickUp();
        }
    }

    private void GenerateHealthPickUp()
    {
        Instantiate(healthPickUpPrefab, transform.position, Quaternion.identity);
    }
}
