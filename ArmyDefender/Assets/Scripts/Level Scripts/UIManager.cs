using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text healthCost;
    Text damageCost;
    Text fireRateCost;
    Text coinCount;

    private void Start()
    {
        healthCost = GetComponent<Text>();
        damageCost = GetComponent<Text>();
        fireRateCost = GetComponent<Text>();
        coinCount = GetComponent<Text>();
    }
}
