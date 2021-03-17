﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    public float GetDamage() { return damage; }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
