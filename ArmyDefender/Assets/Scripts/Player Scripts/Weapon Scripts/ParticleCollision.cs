using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private EnemyBaseClass enemy;
    private EnemyOne one;

    private void Awake()
    {
        enemy = FindObjectOfType<EnemyBaseClass>();
        one = FindObjectOfType<EnemyOne>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        DamageDealer damage = gameObject.GetComponent<DamageDealer>();
        if (!damage) { return; }

        if (other.gameObject.tag == Constants.enemyTag)
        {
            Debug.Log("Enemy Hit");
            enemy.TakeDamage(damage);
        }
    }

    private void OnParticleTrigger()
    {
       /* DamageDealer damage = gameObject.GetComponent<DamageDealer>();
        if (!damage) { return; }

        if (tag == Constants.enemyTag)
        {
            Debug.Log("Enemy Hit");
            enemy.TakeDamage(damage);
        }*/
    }
}
