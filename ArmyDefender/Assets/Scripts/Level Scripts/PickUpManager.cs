using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    //Variables
    [SerializeField] private int coinCount = 0;
    private float healthBonus; // =
    private float startingHealth = 100f;
    private float healPercent = 0.2f;
    //Object references
    [SerializeField] GameObject coin;
    [SerializeField] GameObject heart;

    //Cached script references
    Player player;
    Enemy enemy;

    private void Start()
    {
        healthBonus = (startingHealth * healPercent);
        player = FindObjectOfType<Player>();
    }

    public void PickUpItem(GameObject itemPickedUp)
    {
        switch(itemPickedUp.tag)
        {
            case Constants.coinTag:
                PickedUpCoin();
                break;
            case Constants.healthTag:
                PickedUpHealth();
                break;
            default:
                Debug.Log("No tag on gameobject");
                break;
        }
    }

    private void PickedUpCoin()
    {
        coinCount++;
    }
    private void PickedUpHealth()
    {
        //player.Health = player.GetHealth();
        player.Health += healthBonus;
        player.CheckHealth();
        //healthBonus += player.GetHealth();
    }

    


}
