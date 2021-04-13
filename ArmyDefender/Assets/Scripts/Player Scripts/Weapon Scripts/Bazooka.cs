using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : Player
{
    private float rocketFireRate = 1f;

    [SerializeField] private Transform rocketSpawnPoint;
    [SerializeField] private GameObject rocketPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        FireDelay();
    }

    private void FireDelay()
    {
        fireRate += Time.deltaTime;

        if (fireRate >= rocketFireRate)
        {
            FireRocket();
            fireRate = 0f;
        }
    }

    private void FireRocket()
    {
        GameObject rocket = Instantiate(rocketPrefab, rocketSpawnPoint.position, rocketSpawnPoint.rotation);
    }
}
