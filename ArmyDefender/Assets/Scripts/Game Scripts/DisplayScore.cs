using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    Text scoreText;
    GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        game = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = game.GetScore().ToString();
    }
}
