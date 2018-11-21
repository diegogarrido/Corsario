using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {


	public static bool GamePaused;
	public GameObject pausedMenuUI;

    private void Start()
    {
        GamePaused = false;
        pausedMenuUI.SetActive(GamePaused);
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
            SwapMenu();
		}
	}

    private void SwapMenu()
    {
        GamePaused = !GamePaused;
        pausedMenuUI.SetActive(GamePaused);
    }
	//Funciones Botones

	public void Continue()
	{
		pausedMenuUI.SetActive(false);
		GamePaused = false;
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0,LoadSceneMode.Single);
	}

}
