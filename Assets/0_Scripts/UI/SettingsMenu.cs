using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer audioMixerSFX;

    public TMPro.TMP_Dropdown resolutionDropDown;

    Resolution[] resolutions;

    public GameObject settingsMenu;
    public GameObject pauseMenu;

    public GameObject selector;

    [Header("Botones")]
    public GameObject settingsButton;
    public GameObject backButton;
    public GameObject settingsFirstButton;
    public GameObject optionsButton; //Este es para volver de los settings

    [Header("Selectors")]
    public List<GameObject> selectorsSettings = new List<GameObject>();
    

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {

            string option = resolutions[i].width + " X " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";

            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;

        resolutionDropDown.RefreshShownValue();

        Screen.SetResolution(resolutions.Last().width, resolutions.Last().height, Screen.fullScreen);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolumeSFX(float volume)
    {
        audioMixerSFX.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SetVolumeBackgroundMusic(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //Botones de PLAY
    public void BTN_Play()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene(1);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
            SceneManager.LoadScene(1);
        //}
    }

    public void SetPositionSelector()
    {
    }

    //Botones para abrir settings
    public void BTN_Settings()
    {
        if (Input.GetButtonDown("Submit"))
        {
            EventSystem.current.SetSelectedGameObject(null);
            settingsMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(settingsFirstButton);

        }

        //if (Input.GetMouseButtonDown(0))
        //{
            settingsMenu.SetActive(true);
        //}
    }

    //Botones de Exit
    public void BTN_Exit()
    {
        if (Input.GetButtonDown("Submit"))
            Application.Quit();

        //if (Input.GetMouseButtonDown(0))
        //{
            Application.Quit();
        //}
    }

    //Botones de VOLVER ATRAS

    public void BTN_Back()
    {
        if (Input.GetButtonDown("Submit"))
        {
            EventSystem.current.SetSelectedGameObject(null);
            settingsMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(optionsButton);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
            settingsMenu.SetActive(false);
        //}
    }

    public void BTN_BackInGame()
    {
        Pause.UnpauseGame();
        pauseMenu.SetActive(false);
    }

    public void ShowSelector(int id)
    {
        selectorsSettings[id].SetActive(true);
    }
    public void HideSelector(int id)
    {
        selectorsSettings[id].SetActive(false);
    }
}
