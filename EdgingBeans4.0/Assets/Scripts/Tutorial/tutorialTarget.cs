using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialTarget : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        Debug.Log("Remaing health of object: " + Gun.hittedObject + " is " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
