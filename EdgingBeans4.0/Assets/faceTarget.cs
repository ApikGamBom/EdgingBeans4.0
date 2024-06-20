using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceTarget : MonoBehaviour
{
    public GameObject rotator;
    public GameObject target;

    void Update()
    {
        Vector3 direction = (target.transform.position - rotator.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        rotator.transform.rotation = Quaternion.Slerp(rotator.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
