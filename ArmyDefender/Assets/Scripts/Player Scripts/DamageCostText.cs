using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCostText : MonoBehaviour
{
    PlayerStats player;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = player.GetDamageCost().ToString();

        if (player.GetCoinCount() >= player.GetDamageCost())
        {
            text.color = Color.green;
        }
        else
        {
            text.color = Color.red;
        }
    }
}
