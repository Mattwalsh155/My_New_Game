using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : EnemyBaseClass
{
    //Variables
    private float fireRate;
    protected private float enemyBulletSpeed = 8f;
    private float minFireRate = 0.5f;
    private float maxFireRate = 2f;

    //Object references
    [SerializeField] protected GameObject enemyBulletPrefab;
    [SerializeField] protected Transform enemyBulletTravelPoint;

    private void Awake()
    {
        enemyStartingHealth = 20f;
        minSpeed = 0.1f;
        maxSpeed = 0.5f;
        scoreValue = 100;
    }
    protected override void Start()
    {
        base.Start();
        SetFireRate(); 
    }

    protected override void Update()
    {
        base.Update();
        EnemyFireDelay();
    }

    private void SetFireRate()
    {
        fireRate = Random.Range(minFireRate, maxFireRate);
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

    protected virtual void FireBullet()
    {
        Player target = GameObject.FindObjectOfType<Player>();
        if (!target) { return; }
        GameObject bullet = Instantiate(enemyBulletPrefab, enemyBulletTravelPoint.position, Quaternion.identity);
        Vector3 direction = (target.transform.position - transform.position).normalized * enemyBulletSpeed;
        Vector2 shootDirection = direction;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y);
    }


}
