using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] public float damage = 10f;

    public float GetDamage() { return damage; }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
