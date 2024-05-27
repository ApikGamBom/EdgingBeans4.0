using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Slid Controller")]
    public SliderControl sliderController;

    [Header("GameObjects")]
    public GameObject pauseMenu;
    public GameObject Crosshair;
    public GameObject optionsTab;
    public GameObject GameplayTab;
    public GameObject VolumeTab;

    [Header("Floats")]
    public float UiCountdown = 1f;

    [Header("Bools")]
    public static bool isPaused = false;
    public bool CountDone;
    public bool optionOpen = false;
    
    public void Update()
    {
        UiCountdown = sliderController.ResumeDelayValue;
    }
    public void playGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void toggleSettings()
    {
        optionsTab.SetActive(!optionsTab.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        optionOpen = !optionOpen;
    }
    
    public void OptionsTabSwitch(Button button)
    {
        if (button.name == "GameplayBtn")
        {
            GameplayTab.SetActive(true);
            VolumeTab.SetActive(false);
        }
        else if (button.name == "VolumeBtn")
        {
            GameplayTab.SetActive(false);
            VolumeTab.SetActive(true);
        }
        
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
