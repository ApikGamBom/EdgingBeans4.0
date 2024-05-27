using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        
        Debug.Log("Remaing health of object: " + Gun.hittedObject + " is " + health);

        if (health <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        PlayerStats.oilCount += 1;
    }
}
