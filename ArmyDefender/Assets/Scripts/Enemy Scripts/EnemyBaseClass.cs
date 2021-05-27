using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    //Enemy variables
    //Set the values in each enemy child class
    protected private float enemyStartingHealth;
    [SerializeField] protected private float enemyMoveSpeed;
    protected private float minSpeed;
    protected private float maxSpeed;
    protected private int scoreValue;
    //
    private float explosionDuration = 1f;
    private float impactDuration = 0.3f;
    private float impactLocationAdjust = 1f;
    private float healthPickUpDropRate = 10f;
    private float coinSpawnRadius = 0.5f;
    private float weaponDropRate = 5f;

    //Object references
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject bulletImpactPrefab;
    [SerializeField] GameObject healthPickUpPrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject[] weaponPickups;

    //Cached script references
    //EnemyDamageDealer damage;

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

    protected virtual void DestroyEnemy()
    {
        Destroy(gameObject);
        TriggerDeathEffect();
        AddScore();
        NumberOfCoins();
        CheckForDrops();  
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
        var randNumHealth = Random.Range(0f, 100f);
        if (randNumHealth <= healthPickUpDropRate)
        {
            GenerateHealthPickUp();
        }

        var randNumGun = Random.Range(0f, 100f);
        if (randNumGun <= weaponDropRate)
        {
            GenerateWeaponPickUp();
        }
        //Debug.Log(randNumGun);
    }

    private void GenerateHealthPickUp()
    {
        Instantiate(healthPickUpPrefab, transform.position, Quaternion.identity);
    }

    private void GenerateWeaponPickUp()
    {
        var weaponIndex = Random.Range(0, 3);
        Instantiate(weaponPickups[weaponIndex], transform.position, Quaternion.identity);
    }
}
