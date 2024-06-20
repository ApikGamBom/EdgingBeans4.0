using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitching : MonoBehaviour
{
    public static int weapon;
    public int currentWeapon;

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;

    public GameObject weaponSelected1;
    public GameObject weaponSelected2;
    public GameObject weaponSelected3;


    void Update()
    {
        currentWeapon = weapon;

        if (!PauseMenu.isPaused)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                weapon++;
                if (weapon >= 3)
                    weapon = 0;

            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                weapon--;
                if (weapon <= -1)
                    weapon = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                weapon = 0;
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                weapon = 1;
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                weapon = 2;

            UpdateWeapon();
        }
    }

    void UpdateWeapon()
    {
        if (weapon == 0)
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);

            weaponSelected1.SetActive(true);
            weaponSelected2.SetActive(false);
            weaponSelected3.SetActive(false);
        }
        else if (weapon == 1)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);

            weaponSelected1.SetActive(false);
            weaponSelected2.SetActive(true);
            weaponSelected3.SetActive(false);
        }
        else if (weapon == 2)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);

            weaponSelected1.SetActive(false);
            weaponSelected2.SetActive(false);
            weaponSelected3.SetActive(true);
        }
    }
}
