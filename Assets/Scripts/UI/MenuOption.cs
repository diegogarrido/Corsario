using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuOption : MonoBehaviour
{
    public GameObject menuPaused;
    public GameObject menuOption;
    public GameObject menuCredits;
    float time;

    private void Start()
    {
        menuOption.SetActive(false);
        menuPaused.SetActive(true);
        menuCredits.SetActive(false);
        time = 10;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if ((int)time <= 0)
        {
            menuOption.SetActive(true);
            menuCredits.SetActive(false);
        }
    }

    public void MenuPause()
    {
        menuOption.SetActive(false);
        menuPaused.SetActive(true);
    }

    public void MenuOpt()
    {
        menuPaused.SetActive(false);
        menuOption.SetActive(true);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void Creditos()
    {
        menuCredits.SetActive(true);
        menuPaused.SetActive(false);
        menuOption.SetActive(false);

        Update();

    }


}
