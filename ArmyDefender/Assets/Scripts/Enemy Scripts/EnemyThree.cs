using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThree : EnemyTwo
{
    protected override void FireBullet()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, enemyBulletTravelPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyBulletSpeed);
    }
}
