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


    void Start()
    {
        countText.text = PlayerStats.tankCount.ToString() + "/" + maxOil;
        ShowSlider.value = PlayerStats.tankCount / maxOil;
    }
    void Update()
    {
        countText.text = PlayerStats.tankCount.ToString() + "/" + maxOil;
        ShowSlider.value = PlayerStats.tankCount / maxOil;
    }
}
