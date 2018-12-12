using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public bool gamePaused;
    public KeyCode lastPressed;
    public GameObject mainUI;

    private GameObject menu;

	void Start () {
        gamePaused = false;
        menu = GameObject.FindGameObjectWithTag("Menu");
    }
	
	void Update () {
        if (!gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                lastPressed = KeyCode.Escape;
                menu.GetComponent<PauseMenu>().Active(true);
                gamePaused = true;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                lastPressed = KeyCode.I;
                menu.GetComponent<InventoryScript>().Active(true);
                gamePaused = true;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                lastPressed = KeyCode.M;
                menu.GetComponent<MapScript>().Active(true);
                gamePaused = true;
            }
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            if (Input.GetKeyDown(lastPressed))
            {
                GameObject.FindGameObjectWithTag("ToolTip").GetComponent<ToolTipScript>().Hide();
                GameObject.FindGameObjectWithTag("TransferOptions").GetComponent<TransferScript>().Hide();
                CloseAll();
            }
        }
    }

    public void HideMainUI()
    {
        mainUI.SetActive(false);
    }

    public void ShowMainUI()
    {
        mainUI.SetActive(true);
    }

    public void CloseAll()
    {
        menu.GetComponent<InventoryScript>().Active(false);
        menu.GetComponent<PauseMenu>().Active(false);
        menu.GetComponent<MapScript>().Active(false);
        menu.GetComponent<BaseScript>().Active(false);
        menu.GetComponent<ShopScript>().Active(false);
        Time.timeScale = 1;
        gamePaused = false;
    }
}
