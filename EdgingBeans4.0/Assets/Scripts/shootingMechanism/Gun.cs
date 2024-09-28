using UnityEngine;

public class Gun : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;
    public int pOilPerKill = 5;
    public static int oilPerKill;
    public static GameObject hittedObject;

    public LayerMask damagableObject;

    public Camera Camera;
    public GameObject bulletSpawnPivot;
    public Transform rigthHandPivot;
    public GameObject gunObject;

    [Header("Sounds")]
    public AudioSource auidoSource;
    public AudioClip shoot1,shoot2,shoot3;

    [System.Obsolete]
    void Update()
    {
        oilPerKill = pOilPerKill;

        if (!PauseMenu.isPaused && rigthHandPivot.FindChild(gunObject.transform.name))
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        PlaySound();

        if (Physics.Raycast(bulletSpawnPivot.transform.position, Camera.transform.forward, out RaycastHit hit, Range, damagableObject))
        {
            hittedObject = hit.transform.gameObject;
            Debug.Log("Hitted " + hittedObject);

            Target target = hit.transform.GetComponent<Target>();
            tutorialTarget tutTargtet = hit.transform.GetComponent<tutorialTarget>();
            if (target != null)
            {
                target.TakeDamage(Damage);
            } else if (tutTargtet != null)
            {
                tutTargtet.TakeDamage(Damage);
            }
        }

    }

    void PlaySound()
    {
        if (weaponSwitching.weapon == 0)
        {
            auidoSource.clip = shoot1;
            auidoSource.Play();
        } else if (weaponSwitching.weapon == 1)
        {
            auidoSource.clip = shoot2;
            auidoSource.Play();
        } else if (weaponSwitching.weapon == 2)
        {
            auidoSource.clip = shoot3;
            auidoSource.Play();
        }
    }
}