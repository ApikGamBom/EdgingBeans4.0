using UnityEngine;
using UnityEngine.UI;


public class oilCountSlider : MonoBehaviour
{

    [SerializeField] public Slider tankCountSlider;

    void Update()
    {
        tankCountSlider.value = PlayerStats.currentTankCount;
    }
}