using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LabyController : MonoBehaviour {

   public GameObject panelLose;
    public float timer;
    public Text time;

    // Use this for initialization
    void Start () {

        
        panelLose.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Lose();
        }

        time.text = "" + Mathf.CeilToInt(timer);
    }

    public void Confirm()
    {
        GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().ShowMainUI();
        GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().gamePaused = false;
        SceneManager.UnloadSceneAsync("Puzzle_Laberinto1");
    }

    public void Lose()
    {
        GameObject.FindGameObjectWithTag("Puzzle").GetComponent<Puzzle>().Lose();
        panelLose.SetActive(true);
    }
}
