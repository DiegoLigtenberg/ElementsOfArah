using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public static int timepassed = 0;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject keybindMenuUI;
    public GameObject[] putoff;

    // Start is called before the first frame update
    void Start()
    {
        timepassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void LoadMenu() //this is actually back to main menu
    {
      //  Debug.Log("loading menu...");
        SceneManager.LoadScene(0);
    }


    public void Resume()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        for (int i = 0; i < putoff.Length ; i++)
        {
            putoff[i].SetActive(true);
        }
        pauseMenuUI.SetActive(false);
        keybindMenuUI.SetActive(false);
   
    }

    public void Pause()
    {

        GameIsPaused = true;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        for (int i = 0; i < putoff.Length; i++)
        {
            putoff[i].SetActive(false);
        }
        pauseMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("would quit");
        Application.Quit();
    }


    public void OpenKeybind()
    {
        Debug.Log("are u kidding me");
        PlayerPrefs.SetInt("OpenedKeybind", 1);
        pauseMenuUI.SetActive(false);
        keybindMenuUI.SetActive(true);
    }
}
