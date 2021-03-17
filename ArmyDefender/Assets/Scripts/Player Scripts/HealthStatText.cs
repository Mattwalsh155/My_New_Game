using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatText : MonoBehaviour
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
        healthText.text = "Health: " + player.GetHealth().ToString();
    }
}
