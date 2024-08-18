using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class PlayerStats : MonoBehaviour
{
    //Oil
    public TMP_Text currentTankCountText;
    public TMP_Text tankCountGoalText;
    public TMP_Text currentOilCount;
    
    public static int tankCountGoal = 50;
    public static int currentTankCount = 0;
    
    public static int oilCount;
    public int pOilCount;

    //Health
    public TMP_Text currentHealthCountText;
    public TMP_Text maxHealthCountText;
    public static int health = 20;

    public int pHealth;

    public static int maxHealth;
    public bool isAlive;
    public static bool isAliveStatic;

    public GameObject deathScreen;
    public GameObject crossHair;
    public GameObject Stats;

    public bool ps4Controller_Active;
    public static bool ps4_active;

    void Start()
    {
        oilCount = pOilCount;
        
        health = pHealth;

        maxHealth = health;

        deathScreen.SetActive(false);
    }

    void Update()
    {
        ps4_active = ps4Controller_Active;

        isAliveStatic = isAlive;

        pOilCount = oilCount;
        pHealth = health;

        currentHealthCountText.text = health.ToString();
        maxHealthCountText.text = maxHealth.ToString();
        
        currentTankCountText.text = currentTankCount.ToString();
        tankCountGoalText.text = tankCountGoal.ToString();
        currentOilCount.text = oilCount.ToString();

        if (health <= 0)
        {
            isAlive = true;
        }
        if (isAlive)
        {
            deathScreen.SetActive(true);
            crossHair.SetActive(false);
            Stats.SetActive(false);

            UnityEngine.Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        } else if (!PauseMenu.isPaused)
        {
            deathScreen.SetActive(false);
            crossHair.SetActive(true);
            Stats.SetActive(true);

            Time.timeScale = 1f;
        }
    }
    public void Respawn()
    {
        deathScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isAlive = false;
        health = maxHealth;
    }
    public void Exit()
    {
        deathScreen.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
}
