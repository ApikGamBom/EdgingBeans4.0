using UnityEngine;

public class Gun : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;
    public int publicPointsPerKill = 5;
    public static int PointsPerKill;
    public static GameObject hittedObject;

    public LayerMask goThroug;

    public Camera Camera;
    public GameObject bulletSpawnPivot;
    public Transform rigthHandPivot;
    public GameObject gunObject;

    [System.Obsolete]
    void Update()
    {
        PointsPerKill = publicPointsPerKill;
        
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
        if (Physics.Raycast(bulletSpawnPivot.transform.position, Camera.transform.forward, out RaycastHit hit, Range, goThroug))
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
