using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class EquipScript : MonoBehaviour
{
    [Header("Gameobjects")]
    public GameObject Gun;
    public GameObject raySpawnPoint;
    public GameObject rayObject;
    public GameObject child;
    public Transform rigthHandPivot;

    [Header("Floats and Bools")]
    public float range = 2f;
    //public float open = 100f; //Ikke nødvendig
    public bool isHoldingItem;
    public static int timesPickedUpInteractable = 0;

    [Header("UI Components")]
    public string objName;
    public GameObject pickUpText;
    public TMP_Text rayObjectName;

    void Start()
    {
        Gun.GetComponent<Rigidbody>().isKinematic = true;
    }

    [System.Obsolete]
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            ShootRay();

            if (Input.GetKeyDown("q"))
            {
                if (rigthHandPivot.transform.childCount > 0)
                {
                    UnequipObject();
                } else
                {
                    Debug.Log("No child found on gameobject called: " + rigthHandPivot.transform.name);
                }
            }
        }
        if (PauseMenu.isPaused)
        {
            pickUpText.SetActive(false);
        }
    }

    void ShootRay(){
        RaycastHit hit;

        if (Physics.Raycast(raySpawnPoint.transform.position, raySpawnPoint.transform.forward, out hit, range))
        {
            objName = hit.transform.name;
            rayObjectName.text = hit.transform.name.ToString();
            rayObject = hit.transform.gameObject;
            Debug.Log(objName);

            FiTarget target = hit.transform.GetComponent<FiTarget>();
            if (target != GetComponent<FiTarget>() && !isHoldingItem)
            {
                pickUpText.SetActive(true);

                if (Input.GetKeyDown("e") && !isHoldingItem)
                {
                    EquipObject();
                }
            }
            else
            {
                pickUpText.SetActive(false);
            }
        }
    }

    [System.Obsolete]
    void UnequipObject()
    {
        isHoldingItem = false;

        child = rigthHandPivot.transform.GetChild(0).gameObject;

        if (child.GetComponent<Rigidbody>())
        {
            Debug.Log(child.transform.name + " has the rigidbody component!");
            Debug.Log("Unequipping Item " + child.transform.name);
            rigthHandPivot.DetachChildren();
            child.transform.eulerAngles = new Vector3(rayObject.transform.eulerAngles.x, rayObject.transform.eulerAngles.y, rayObject.transform.eulerAngles.z - 45);
            child.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void EquipObject()
    {
        timesPickedUpInteractable++;
        isHoldingItem = true;

        Debug.Log("Equipping Item " + objName);

        rayObject.GetComponent<Rigidbody>().isKinematic = true;
        rayObject.transform.position = rigthHandPivot.transform.position;
        rayObject.transform.rotation = rigthHandPivot.transform.rotation;
        rayObject.transform.SetParent(rigthHandPivot);
    }
}
