using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {

	public GameObject menu1;
	public GameObject menu2;

	  private void Start()
    {
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
		menu2.SetActive(true);
	}

	public void Go()
	{
		Menu2();
	}

	public void Back()
	{
		Menu1();
	}



}
