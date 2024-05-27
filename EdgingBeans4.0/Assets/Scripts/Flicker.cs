using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flicker : MonoBehaviour
{
    public RawImage myImage;

    void Start()
    {
        StartCoroutine(FlickerOpacity());
    }
    IEnumerator FlickerOpacity()
    {
        while (true)
        {
            float randomNumber = Random.Range(20f, 80f) / 255f;

            float randomInterval = Random.Range(0.05f, 0.15f);
            
            SetOpacity(randomNumber);

            yield return new WaitForSeconds(randomInterval);
        }
    }

    public void SetOpacity(float opacity)
    {
        Color color = myImage.color;
        color.a = opacity;
        myImage.color = color;
    }

}
