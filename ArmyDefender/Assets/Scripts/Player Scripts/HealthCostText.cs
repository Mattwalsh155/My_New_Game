using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCostText : MonoBehaviour
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
        CheckLevel();
        text.text = player.GetHealthCost().ToString();
        

        if (player.GetCoinCount() >= player.GetHealthCost())
        {
            text.color = Color.green;
        }
        else
        {
            text.color = Color.red;
        }
    }

    private void CheckLevel()
    {
        if (player.GetHealthCost() == player.GetMaxLevel())
        {
            text.text = "MAX";
        }
    }
}
