using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStatHolder : MonoBehaviour
{
    public float health;
    public float damage;
    public float fireRate;

    public static GlobalStatHolder gameInstance;

    private void Awake()
    {
        if (gameInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            gameInstance = this;
        }
        else if (gameInstance != this)
        {
            Destroy(gameObject);
        }
    }
}
