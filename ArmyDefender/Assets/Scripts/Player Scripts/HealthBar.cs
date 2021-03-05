using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBar;
    private float currentHealth;
    private float startingHealth = 100f;
    Player player;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        currentHealth = player.GetHealth();
        healthBar.fillAmount = (currentHealth / startingHealth);
    }
}
