using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutText1;
    public GameObject tutText2;
    public GameObject tutText3;
    public GameObject tutText4;

    public int tutCount = 1;
    public static int tutCountPublic;

    public GameObject tutorialObstacle;
    public bool tutObstacle = true;

    void Start()
    {
        tutText1.SetActive(true);
        tutText2.SetActive(false);
        tutText3.SetActive(false);
        tutText4.SetActive(false);
    }

    void Update()
    {
        int tutCheck = tutCount;

        if (Input.GetKeyDown(KeyCode.J))
        {
            tutCount++;
        }

        if (tutCount == 1 && EquipScript.timesPickedUpInteractable == 1)
        {
            Debug.Log("Updating text to two!");
            tutCount = 2;
        } else if (tutCount == 2 && !tutorialObstacle.activeSelf)
        {
            Debug.Log("tutCount is now 3!");
            tutCount = 3;
        } else if (tutCountPublic == 4)
        {
            Debug.Log("Updated tutCount to 4");
            tutCount = 4;
        }

        if (tutCheck != tutCount)
        {
            UpdateText();
        }

        tutCountPublic = tutCount;
    }

    void UpdateText()
    {
        if (tutCount == 1)
        {
            tutText1.SetActive(true);
        }
        else if (tutCount == 2)
        {
            tutText1.SetActive(false);
            tutText2.SetActive(true);
        }
        else if (tutCount == 3)
        {
            tutText2.SetActive(false);
            tutText3.SetActive(true);
        }
        else if (tutCount == 4)
        {
            tutText3.SetActive(false);
            tutText4.SetActive(true);
        }
    }
}
