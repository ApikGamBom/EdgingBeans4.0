using UnityEngine;
using UnityEngine.UI;


public class healthCountSlider : MonoBehaviour
{

    [SerializeField] public Slider healthSlider;

    void Update()
    {
        healthSlider.value = PlayerStats.health;
    }
}