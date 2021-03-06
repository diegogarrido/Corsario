﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject loadMenu;
    public GameObject credits;

    
    private void Start()
    {
        Text[] texts = loadMenu.GetComponentsInChildren<Text>();
        for (int i = 1; i <= 3; i++)
        {
            if (DataManager.SaveCreated("Save" + i))
            {
                texts[i - 1].text = "Partida " + i;
            }
            else
            {
                texts[i - 1].text = "Nueva Partida";
            }
        }
        loadMenu.SetActive(false);
        credits.SetActive(false);
    }

    public void openMainMenu()
    {
        mainMenu.SetActive(true);
        loadMenu.SetActive(false);
    }

    public void openLoadMenu()
    {
        mainMenu.SetActive(false);
        loadMenu.SetActive(true);
    }

    public void PlayGame(string saveName)
    {
        if (saveName == "0")
        {
            saveName = PlayerPrefs.GetString("LastPlayed");
            if (saveName.Length == 0)
            {
                saveName = "Save1";
            }
        }
        DataManager.saveName = saveName;
        PlayerPrefs.SetString("Load","Game");
        SceneManager.LoadScene(3);
    }

    public void Tutorial()
    {
        PlayerPrefs.SetString("Load", "Tutorial");
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Debug.Log("Se cerro el juego");
        Application.Quit();
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);   
    }
}
