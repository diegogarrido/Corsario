using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject pausedMenuUI;

    private void Start()
    {
        pausedMenuUI.SetActive(false);
    }

    public void Active(bool active)
    {
        pausedMenuUI.SetActive(active);
    }
	//Funciones Botones

	public void Continue()
	{
        GetComponent<MenuController>().CloseAll();
	}

	public void MainMenu()
	{
        GetComponent<MenuController>().gamePaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
	}

}
