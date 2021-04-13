using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private EnemyBaseClass enemy;

    private void Awake()
    {
        enemy = FindObjectOfType<EnemyBaseClass>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
        if (!damage) { return; }

        if (other.tag == Constants.enemyTag)
        {
            Debug.Log("Enemy Hit");
            enemy.TakeDamage(damage);
        }
    }

    private void OnParticleTrigger()
    {
        
    }
}
