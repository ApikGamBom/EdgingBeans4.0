using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [Header("Slid Controller")]
    public SliderControl sliderController;

    [Header("GameObjects")]
    public GameObject pauseMenu;
    public GameObject Crosshair;
    public GameObject optionsTab;
    public GameObject CountdownObj;

    [Header("Floats")]
    public float UiCountdown = 1;

    [Header("Static Bools")]
    public static bool isPaused = false;

    [Header("Bools")]
    public bool CountDone;
    public bool optionOpen = false;

    [Header("Other")]
    public TextMeshProUGUI countdownText;
    public Transform targetPosition;
    public RawImage  compassNeedle;

    void Update()
    {
        UiCountdown = sliderController.ResumeDelayValue;

        if (Input.GetButtonDown("Cancel") && CountDone && !optionOpen && optionsTab.CompareTag("gameScene"))
        {
            toggleMenu();
        }
        else if (Input.GetButtonDown("Cancel") && CountDone && optionOpen && optionsTab.CompareTag("gameScene"))
        {
            optionsTab.SetActive(false);
            optionOpen = false;
            toggleMenu();
            
        }
    }

    public void toggleMenu()
    {
        StartCoroutine(ToggleUi());
    }

    public IEnumerator ToggleUi()
    {
        float remainingTime = UiCountdown;

        if (isPaused && !optionOpen && !PlayerStats.isAliveStatic)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.SetActive(false);
            CountDone = false;
            CountdownObj.SetActive(true);
            while (remainingTime > 0)
            {
                countdownText.text = remainingTime.ToString("F1");
                yield return new WaitForSecondsRealtime(0.1f);
                remainingTime -= 0.1f;
            }
            Time.timeScale = 1f;
            CountDone = true;
            CountdownObj.SetActive(false);
            Crosshair.SetActive(true);
            countdownText.text = "";
        }
        else if (!isPaused && !optionOpen && !PlayerStats.isAliveStatic)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            Crosshair.SetActive(false);
            Time.timeScale = 0f;
        }
        isPaused = !isPaused;
        CountDone = true;
    }

    public void OptionsToggle()
    {
        optionsTab.SetActive(!optionsTab.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        optionOpen = !optionOpen;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainScene");
    }
    public void GoToGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}