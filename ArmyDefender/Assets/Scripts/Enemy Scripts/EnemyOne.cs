using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : EnemyBaseClass
{
    private float moveDelayTime = 0.1f;

    private void Awake()
    {
        minSpeed = 0.5f;
        maxSpeed = 1.2f;
        scoreValue = 100;
        StartCoroutine(MoveEnemy());
    }
    protected override void Start()
    {
        base.Start();
        enemyStartingHealth = 30f; 
    }

    protected override void Update()
    {
        
    }

    private IEnumerator MoveEnemy()
    {
        Player target = GameObject.FindObjectOfType<Player>();
        if(!target) { yield break; }
        Vector3 findPlayer = target.transform.position - transform.position;
        Vector2 moveDirection = findPlayer.normalized;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, -enemyMoveSpeed);
        //
        yield return new WaitForSeconds(moveDelayTime);
        //
        StartCoroutine(MoveEnemy());
    }
    
}
