using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBaseClass : Player
{
    WeaponManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
