using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiTarget : MonoBehaviour
{

    public float drop = 2f;


    public void Commence (float amount)
    {
        drop -= amount;
        if (drop <= 1)
        {
            Go();
        }
    }

    void Go()
    {
        Destroy(gameObject);
    }
}
