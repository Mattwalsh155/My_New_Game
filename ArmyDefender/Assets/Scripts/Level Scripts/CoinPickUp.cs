﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    private float moveSpeed = 1f; 
    private void Start()
    {
        MoveCoin();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collider2D collision)
    {
        PickUpManager manager = collision.GetComponent<PickUpManager>();
        if (!manager) { return; }
        manager.PickUpItem(gameObject); //This line is causing null reference exception
        Destroy(gameObject);
        
    }

    private void MoveCoin()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -moveSpeed);
    }
}
