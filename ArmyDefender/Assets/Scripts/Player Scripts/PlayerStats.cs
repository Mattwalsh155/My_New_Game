using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : Player
{
    [SerializeField] public float myCurrentHealth;
    [SerializeField] private float newHealth;
    [SerializeField] private int healthLevel;
    [SerializeField] private int maxHealthLevel = 20;
    private float healthMultiplier = 1.1f;

    [SerializeField] public float currentFireRate;
    [SerializeField] private float newFireRate;
    private int fireRateLevel;
    private int maxFireRateLevel = 20;
    private float fireRateIncrease = 0.05f;

    [SerializeField] public float currentDamage;
    [SerializeField] private float newDamage;
    private int damageLevel;
    private int maxDamageLevel = 20;
    private float damageMultiplier = 1.07f;

    [SerializeField] private int currentGold;
    [SerializeField] private int[] nextLevelCost;
    private int currentLevel;
    private int maxLevel = 20;

    private float nextLevelFactor = 1.25f;

    Player player;
    DamageDealer damage;
    public static PlayerStats instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        SetUpStats();
        SetLevels();
    }

    private void Start()
    {
        
    }

    private void SetLevels()
    {
        nextLevelCost = new int[maxLevel];
        nextLevelCost[0] = 10;

        for (int i = 1; i < maxLevel; i++)
        {
            nextLevelCost[i] = Mathf.RoundToInt(nextLevelCost[i - 1] * nextLevelFactor);
        }
    }

    public int GetHealthCost()
    {
        return nextLevelCost[healthLevel];
    }

    public int GetDamageCost()
    {
        return nextLevelCost[damageLevel];
    }

    public int GetFireRateCost()
    {
        return nextLevelCost[fireRateLevel];
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
            AddGold();
        }
    }

    public int GetCoinCount()
    {
        return currentGold;
    }

    public void SetCoinCount(int coins)
    {
        currentGold++;
    }

    private void AddGold()
    {
        currentGold += 10000;
    }

    public void BuyHealth()
    {
        if (currentGold > nextLevelCost[healthLevel] && healthLevel < maxHealthLevel - 1)
        {
            SpendGoldOnHealth();
        }
        
    }

    public void BuyFireRate()
    {
        if (currentGold > nextLevelCost[fireRateLevel] && fireRateLevel < maxFireRateLevel - 1)
        {
            SpendGoldOnFireRate();
        }

    }

    public void BuyDamage()
    {
        if (currentGold > nextLevelCost[damageLevel] && damageLevel < maxDamageLevel - 1)
        {
            SpendGoldOnDamage();
        }
    }

    private void SpendGoldOnHealth()
    {
        currentGold -= nextLevelCost[healthLevel];
        healthLevel++;
        IncreaseHealth();
    }

    private void SpendGoldOnFireRate()
    {
        currentGold -= nextLevelCost[fireRateLevel];
        fireRateLevel++;
        IncreaseFireRate();
    }

    private void SpendGoldOnDamage()
    {
        currentGold -= nextLevelCost[damageLevel];
        damageLevel++;
        IncreaseDamage();
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
