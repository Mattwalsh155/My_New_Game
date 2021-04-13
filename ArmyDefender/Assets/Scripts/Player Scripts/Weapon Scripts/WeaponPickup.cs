using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private float moveSpeed = 1f;
    WeaponManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<WeaponManager>();
        MoveGun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.PickUpWeapon(gameObject);
        Destroy(gameObject);
    }

    private void MoveGun()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -moveSpeed);
    }
}
