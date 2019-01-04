using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuOption : MonoBehaviour
{
    public GameObject menuPaused;

    private void Start()
    {
        menuPaused.SetActive(true);
    }

    public void MenuPause()
    {
        menuPaused.SetActive(true);
    }

    public void LoadTutorial()
    {
        GetComponent<MenuController>().gamePaused = false;
        Time.timeScale = 1;
        PlayerPrefs.SetString("Load", "Tutorial");
        SceneManager.LoadScene(3);
    }
}
