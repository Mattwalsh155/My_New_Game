using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    //Enemy stat variables
    private float enemyStartingHealth = 30f;
    private float enemyMoveSpeed;
    private float minMoveSpeed = 0.5f;
    private float maxMoveSpeed = 1f;
    private float moveAdjust;
    private float moveDelayTime = 0.1f;
    private int scoreValue = 100;
    private float explosionDuration = 1f;
    private float impactDuration = 0.3f;
    private float impactLocationAdjust = 1f;

    //Object References
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject bulletImpactPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SetEnemyMoveSpeed();
        StartCoroutine(MoveEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
        moveAdjust = Random.Range(-1f, 1f);
    }

    //Tried making this a coroutine to add a delay for smoother turning but not working out right now
    private IEnumerator MoveEnemy()
    {
        Player target = GameObject.FindObjectOfType<Player>();
        if (!target) { yield break; }
        Vector3 findPlayer = target.transform.position - transform.position;
        Vector2 moveDirection = findPlayer.normalized;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, -enemyMoveSpeed);
        //
        yield return new WaitForSeconds(moveDelayTime);
         //
        StartCoroutine(MoveEnemy());
        
    }

    private void SetEnemyMoveSpeed()
    {
        enemyMoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D otherGameObject)
    {
        DamageDealer damage = otherGameObject.gameObject.GetComponent<DamageDealer>();
        if (!damage) { return; }
        TakeDamage(damage);
        var impact = Instantiate(bulletImpactPrefab, new Vector3(transform.position.x, transform.position.y - impactLocationAdjust, transform.position.z)
            , Quaternion.identity);
        Destroy(impact, impactDuration);
    }

    public void TakeDamage(DamageDealer damage)
    {
        float currentHealth = enemyStartingHealth -= damage.GetDamage();
        damage.Hit();
        if (currentHealth <= 0f)
        {
            DestroyEnemy();
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
        var explode = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explode, explosionDuration);
        FindObjectOfType<GameManager>().AddToScore(scoreValue);
    }
}
