using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageStatText : MonoBehaviour
{
    Text damageText;
    DamageDealer damageDealer;

    // Start is called before the first frame update
    void Start()
    {
        damageDealer = FindObjectOfType<DamageDealer>();
        damageText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        damageText.text = "Damage: " + damageDealer.GetDamage().ToString("F2");
    }
}
