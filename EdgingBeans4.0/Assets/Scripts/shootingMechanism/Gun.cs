using UnityEngine;

public class Gun : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;
    public static GameObject hittedObject;
    
    public Camera Camera;
    public GameObject bulletSpawnPivot;
    public Transform rigthHandPivot;
    public GameObject gunObject;

    [System.Obsolete]
    void Update()
    {
        
        if (!PauseMenu.isPaused && rigthHandPivot.FindChild(gunObject.transform.name))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (Physics.Raycast(bulletSpawnPivot.transform.position, Camera.transform.forward, out RaycastHit hit, Range))
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
}
