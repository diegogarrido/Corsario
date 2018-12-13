using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {

	public GameObject menu1;
	public GameObject menu2;
    public GameObject menu3;

	  private void Start()
    {
        menu3.SetActive(false);
        menu2.SetActive(false);
        menu1.SetActive(true);
    }


	public void Menu1()
	{
		menu2.SetActive(false);
		menu1.SetActive(true);
	}

	public void Menu2()
	{
		menu1.SetActive(false);
        menu3.SetActive(false);
        menu2.SetActive(true);
	}

   public void Menu3()
    {
        menu1.SetActive(false);
        menu2.SetActive(false);
        menu3.SetActive(true);
    }

    public void Go()
	{
		Menu2();
	}

    public void Go2()
    {
        Menu3();
    }

    public void Back()
	{
		Menu1();
	}

    public void Back3()
    {
        Menu2();
    }

    public void VolverJuego()
    {
        GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().ShowMainUI();
        GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().gamePaused = false;
        SceneManager.UnloadSceneAsync("Tutorial");

    }


}
