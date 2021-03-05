using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShoot : MonoBehaviour
{
    //Enemy stat variables
    private float enemyStartingHealth = 20f;
    private float minFireRate = 0.5f; //Shoot faster
    private float maxFireRate = 2f; //Shoot slower
    private float fireRate;
    private float enemyBulletSpeed = 8f;
    private float enemyMoveSpeed;
    private float minSpeed = 0.1f;
    private float maxSpeed = 0.5f;
    private int scoreValue = 100;
    private float explosionDuration = 1f;
    private float impactDuration = 0.3f;
    private float impactLocationAdjust = 1f;

    //Object references
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] Transform enemyBulletTravelPoint;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject bulletImpactPrefab;

    DamageDealer damage;

    private void Start()
    {
        SetFireRate();
        SetMoveSpeed();
    }

    private void Update()
    {
        EnemyMove();
        EnemyFireDelay();
    }

    private void SetMoveSpeed()
    {
        enemyMoveSpeed = Random.Range(minSpeed, maxSpeed);
    }
    private void EnemyMove()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyMoveSpeed);
    }

    private void EnemyFireDelay()
    {
        fireRate -= Time.deltaTime;
        if (fireRate <= 0)
        {
            FireBullet();
            SetFireRate();
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, enemyBulletTravelPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyBulletSpeed);
    }

    private void SetFireRate()
    {
        fireRate = Random.Range(minFireRate, maxFireRate);
    }

    private void OnTriggerEnter2D(Collider2D otherGameObject)
    {
        DamageDealer damage = otherGameObject.gameObject.GetComponent<DamageDealer>();
        if (!damage) { return; }
        TakeDamage(damage);
        var impact = Instantiate(bulletImpactPrefab, new Vector3(transform.position.x, transform.position.y - impactLocationAdjust, transform.position.z)
            , Quaternion.identity);
        Destroy(impact, impactDuration);
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

    private void DestroyEnemy()
    {
        Destroy(gameObject);
        damage = FindObjectOfType<DamageDealer>();
        damage.Hit();
        var explode = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explode, explosionDuration);
        FindObjectOfType<GameManager>().AddToScore(scoreValue);
    }
}
