using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    [Header("Sounds")]
    public AudioSource auidoSource;
    public AudioClip death1;

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
        auidoSource.clip = death1;
        auidoSource.Play();

        Destroy(gameObject);
        PlayerStats.oilCount += Gun.oilPerKill;
    }
}
