using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float speed = 10f;
    private float explosionDuration = 1.5f;

    [SerializeField] private GameObject rocketExplosion;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    //OnTriggerEnter2D explosiong particle effect here?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var explode = Instantiate(rocketExplosion, transform.position, Quaternion.identity);
        Destroy(explode, explosionDuration);
    }
}
