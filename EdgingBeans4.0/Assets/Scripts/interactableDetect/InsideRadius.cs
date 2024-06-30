using UnityEngine;

public class InsideRadius : MonoBehaviour
{
    public float radius;
    public static bool inRadius;

    public Transform center;
    public Transform target;

    void Update()
    {
        float distance = Vector3.Distance(center.position, target.position);
        if (distance < radius)
        {
            inRadius = true;
            Debug.Log("Inside raduis!");
        } else
            inRadius = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(center.position, radius);
    }
}