using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (Tutorial.tutCountPublic == 3)
        {
            Debug.Log(other.name + "Walked into tutTrigg1");
            Tutorial.tutCountPublic = 4;
        }
    }
}
