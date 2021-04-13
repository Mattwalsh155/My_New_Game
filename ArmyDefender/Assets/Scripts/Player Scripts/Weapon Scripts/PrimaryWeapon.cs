using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryWeapon : Player
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletTravelPoint;
    private float bulletSpeed = 10f;
    public float baseFireRate = 1f;

    // Start is called before the first frame update
    protected override void Update()
    {
        base.Update();
        FireDelay();
    }

    public float GetFireRate() { return baseFireRate; }

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
}
