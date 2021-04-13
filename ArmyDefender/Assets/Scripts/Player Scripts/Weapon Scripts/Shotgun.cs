using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Player
{
    
    private float shotgunFireRate = 0.5f;

    [SerializeField] private List<Transform> bulletSpawnPoints;
    
    [SerializeField] GameObject shotgunBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Update()
    {
        //base.Update();
        FireDelay();
    }

    private void FireBullet()
    {

        foreach (Transform spawnPoint in bulletSpawnPoints)
        {
            GameObject bullet = Instantiate(shotgunBulletPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }

        
    }

    private void FireDelay()
    {
        fireRate += Time.deltaTime;

        if (fireRate >= shotgunFireRate)
        {
            FireBullet();
            fireRate = 0f;
        }
    }

    
}
