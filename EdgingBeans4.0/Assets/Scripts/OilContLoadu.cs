using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OilContLoadu : MonoBehaviour
{
    
    [SerializeField] private TMP_Text countText;
    [SerializeField] private float maxOil = 50f;
    [SerializeField] private Slider ShowSlider;

    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Slider healthSlider;


    void Start()
    {
        countText.text = PlayerStats.tankCount.ToString() + "/" + maxOil;
        ShowSlider.value = PlayerStats.tankCount / maxOil;

        HealthText.text = PlayerStats.health.ToString() + "/" + maxHealth;
        healthSlider.value = PlayerStats.health / maxHealth;
    }
    void Update()
    {
        countText.text = PlayerStats.tankCount.ToString() + "/" + maxOil;
        ShowSlider.value = PlayerStats.tankCount / maxOil;

        HealthText.text = PlayerStats.health.ToString() + "/" + maxHealth;
        healthSlider.value = PlayerStats.health / maxHealth;

    }
}
