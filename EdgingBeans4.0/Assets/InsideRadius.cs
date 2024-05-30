using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideRadius : MonoBehaviour
{
    public static bool inRadius;

    void OnTriggerEnter (Collider other) {
        inRadius = true;
    }

    void OnTriggerExit (Collider other) {
        inRadius = false;
    }
}
