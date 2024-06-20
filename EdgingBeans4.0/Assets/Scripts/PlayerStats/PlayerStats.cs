using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    //Oil
    public TMP_Text currentTankCountText;
    public TMP_Text tankCountGoalText;
    public TMP_Text currentOilCount;
    public static int oilCount;
    public static int tankCountGoal = 50;
    public static int currentTankCount = 0;

    public int pOilCount;

    //Health
    public TMP_Text currentHealthCountText;
    public TMP_Text maxHealthCountText;
    public static int health = 20;

    public static int maxHealth;
    public int pHealth;
    public bool isDead;
    public static bool isDeadStatic;

    public GameObject deathScreen;
    public GameObject crossHair;
    public GameObject Stats;

    void Start()
    {
        oilCount = pOilCount;
        
        if (health <= 0)
            health = pHealth;

        maxHealth = health;

        deathScreen.SetActive(false);
    }

    void Update()
    {
        isDeadStatic = isDead;

        pOilCount = oilCount;
        pHealth = health;

        currentHealthCountText.text = health.ToString();
        maxHealthCountText.text = maxHealth.ToString();
        
        currentTankCountText.text = currentTankCount.ToString();
        tankCountGoalText.text = tankCountGoal.ToString();
        currentOilCount.text = oilCount.ToString();

        if (health <= 0)
        {
            isDead = true;
        }
        if (isDead)
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
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        isDead = false;
        health = maxHealth;
    }
    public void Quit()
    {
        deathScreen.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
}
