using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Player
{
    [SerializeField] public float myCurrentHealth;
    [SerializeField] private float newHealth;
    private int healthLevel;
    private int maxHealthLevel = 20;
    private float healthMultiplier = 1.1f;

    [SerializeField] private float currentFireRate;
    [SerializeField] private float newFireRate;
    private int fireRateLevel;
    private int maxFireRateLevel = 20;
    private float fireRateIncrease = 0.05f;

    [SerializeField] private float currentDamage;
    [SerializeField] private float newDamage;
    private int damageLevel;
    private int maxDamageLevel = 20;
    private float damageMultiplier = 1.05f;

    [SerializeField] private float currentGold;
    [SerializeField] private int[] nextLevelCost;
    private int currentLevel;
    private int maxLevel = 20;

    private float nextLevelFactor = 1.25f;

    Player player;
    DamageDealer damage;

    private void Awake()
    {
        var playerCount = FindObjectsOfType<PlayerStats>().Length;

        if (playerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetUpStats();
        SetLevels();
    }

    private void SetLevels()
    {
        nextLevelCost = new int[maxLevel];
        nextLevelCost[0] = 1000;

        for (int i = 1; i < maxLevel; i++)
        {
            nextLevelCost[i] = Mathf.RoundToInt(nextLevelCost[i - 1] * nextLevelFactor);
        }
    }

    private void SetUpStats()
    {
        player = FindObjectOfType<Player>();
        myCurrentHealth = player.GetHealth();

        currentFireRate = player.GetFireRate();

        damage = FindObjectOfType<DamageDealer>();
        currentDamage = damage.GetDamage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuyStats();
        }
    }

    public void BuyStats()
    {
        currentGold += 10000;
        if (currentGold >= nextLevelCost[currentLevel] && currentLevel < maxLevel - 1)
        {
            SpendGold();
        }
        if (currentLevel >= maxLevel)
        {
            currentGold = 0;
        }
    }

    public void BuyHealth()
    {
        IncreaseHealth();
    }

    public void BuyFireRate()
    {
        IncreaseFireRate();
    }

    public void BuyDamage()
    {
        IncreaseDamage();
    }

    private void SpendGold()
    {
        currentGold -= nextLevelCost[currentLevel];
        currentLevel++;
        IncreaseHealth();
        IncreaseDamage();
        IncreaseFireRate();
    }

    private void IncreaseHealth()
    {
        newHealth = Mathf.RoundToInt(myCurrentHealth * healthMultiplier);
        myCurrentHealth = newHealth;
        player.SetHealth(newHealth);
    }

    private void IncreaseFireRate()
    {
        newFireRate = currentFireRate - fireRateIncrease;
        currentFireRate = newFireRate;
        player.SetFireRate(newFireRate);
    }

    private void IncreaseDamage()
    {
        newDamage = currentDamage * damageMultiplier;
        currentDamage = newDamage;
        damage.SetDamage(newDamage);
    }
}
