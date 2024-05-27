using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SearchService;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    //Oil
    public TMP_Text oilCountText;
    public static int oilCount;
     public static int tankCount = 0;

    public int publicOilCount;

    //Health
    public TMP_Text healthScore;
    public static float health;
    public float publicHealth;
    public bool isDead;
    public static bool isDeadStatic;

    public GameObject deathScreen;
    public GameObject crossHair;
    public GameObject Stats;

    void Start()
    {
        oilCount = publicOilCount;
        health = publicHealth;

        deathScreen.SetActive(false);
    }

    void Update()
    {
        isDeadStatic = isDead;
        publicHealth = health;

        healthScore.text = health.ToString() + "/100";
        oilCountText.text = ": " + oilCount.ToString();

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
        health = 20f;
    }
    public void Quit()
    {
        deathScreen.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
}
