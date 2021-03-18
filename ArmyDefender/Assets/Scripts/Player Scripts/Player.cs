using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player stat variables
    private float startingHealth;
    [SerializeField] private float currentHealth = 100f;
    private float fireRate = 0.5f;
    private float bulletSpeed = 10f;
    private float healValue;
    [SerializeField] private float baseFireRate = 1f;
    

    //Screen setup variables
    private float xMin;
    private float xMax;
    private float screenUnits;

    //Player sound variables

    //Object references
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletTravelPoint;

    // Start is called before the first frame update
    void Start()
    {
        LoadSavedValues();
        SetUpMoveBoundaries();
        SetHealthValues();
        
    }

    private void LoadSavedValues()
    {
        currentHealth = PlayerStats.instance.myCurrentHealth;
        baseFireRate = PlayerStats.instance.currentFireRate;
    }

    private void SetHealthValues()
    {
        startingHealth = currentHealth;
        healValue = currentHealth * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        FireDelay();
        FindObjectOfType<DamageDealer>().damage = PlayerStats.instance.currentDamage;
    }

    //Getters
    public float GetHealth() { return currentHealth; }
    public float GetFireRate() { return baseFireRate; }

    public void SetFireRate(float newRate)
    {
        baseFireRate = newRate;
    }
    // Setters
    public void SetHealth(float zeroHealth)
    {
        if (zeroHealth < 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth = zeroHealth;
        }
    }
    //Need only one Getter/Setter. Find out which one is working. But I think I'm using them both
    public float Health
    {
        get { return currentHealth; }
        set { currentHealth += healValue; }
    }

    public void SaveStats()
    {
        PlayerStats.instance.myCurrentHealth = GetHealth();
        PlayerStats.instance.currentFireRate = GetFireRate();
        PlayerStats.instance.currentDamage = FindObjectOfType<DamageDealer>().GetDamage();
    }

    public void CheckHealth()
    {
        if (currentHealth >= startingHealth)
        {
            currentHealth = startingHealth;
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        screenUnits = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }

    private void MovePlayer()
    {
        var movePlayer = Input.mousePosition.x / Screen.width * screenUnits;
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        playerPos.x = Mathf.Clamp(movePlayer, xMin, xMax);
        transform.position = playerPos;
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletTravelPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
    }

    private void FireDelay()
    {
        fireRate += Time.deltaTime;

        if (fireRate >= baseFireRate)
        {
            FireBullet();
            fireRate = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherGameObject)
    {
        HandleHit(otherGameObject);
    }

    private void HandleHit(Collider2D otherGameObject)
    {
        EnemyDamageDealer damage = otherGameObject.gameObject.GetComponent<EnemyDamageDealer>();
        if (!damage) { return; }
        TakeDamage(damage);  
    }

    private void TakeDamage(EnemyDamageDealer damage)
    {
        var damagedHealth = currentHealth -= damage.GetDamage();
        damage.Hit();
        if (damagedHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        //SaveStats();
        FindObjectOfType<SceneLoader>().LoadGameOver();
    }
}
