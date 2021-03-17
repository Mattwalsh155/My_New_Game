using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireRateStatText : MonoBehaviour
{
    Player player;
    Text fireRateText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        fireRateText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        fireRateText.text = "Fire Rate: " + player.GetFireRate().ToString();
    }
}
