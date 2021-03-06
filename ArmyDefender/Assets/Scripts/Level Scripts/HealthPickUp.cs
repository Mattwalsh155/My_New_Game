using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    Player player;
    PickUpManager manager;
    private float healthBonus;
    private float healPercent = 0.2f;
    private float moveSpeed = 1f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        manager = FindObjectOfType<PickUpManager>();
        healthBonus = (player.GetHealth() * healPercent);
    }

    private void Update()
    {
        MoveHealthPickUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);

    }

    private void HandleCollision(Collider2D collision)
    {
        PickUpManager manager = collision.GetComponent<PickUpManager>();
        if (!manager) { return; }
        manager.PickUpItem(gameObject);
        Destroy(gameObject);
    }

    private void MoveHealthPickUp()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -moveSpeed);
    }
}
