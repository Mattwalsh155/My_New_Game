using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Player
{
    private float minigunFireRate = 0.05f;
    private int bulletSpawnIndex = 0;

    [SerializeField] private Transform[] bulletSpawns;
    [SerializeField] GameObject minigunBulletPrefab;
    void Start()
    {
        
    }

    protected override void Update()
    {
        FireDelay();
        
    }

    private void FireBullet()
    {
        if (bulletSpawnIndex < bulletSpawns.Length)
        {
            GameObject bullet = Instantiate(minigunBulletPrefab, bulletSpawns[bulletSpawnIndex].position, bulletSpawns[bulletSpawnIndex].rotation) as GameObject;
            bulletSpawnIndex++;
        }

        if (bulletSpawnIndex == bulletSpawns.Length)
        {
            bulletSpawnIndex = 0;
        }

    }

    private void FireDelay()
    {
        fireRate += Time.deltaTime;

        if (fireRate >= minigunFireRate)
        {
            FireBullet();
            fireRate = 0f;
        }
    }
}
