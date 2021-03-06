using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    Player player;
    Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        healthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Health < 0)
        {
            player.Health = 0;
        }
        healthText.text = player.GetHealth().ToString();
    }
}
