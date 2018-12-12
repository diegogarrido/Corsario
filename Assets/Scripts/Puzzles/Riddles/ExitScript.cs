using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {

    public GameObject riddle;
    public GameObject ExitOption;


    void Start () {

        ExitOption.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Exit()
    {
        ExitOption.SetActive(true);
    }

    public void Confirm()
    {
        SceneManager.LoadScene(1);

    }

    public void Deny()
    {
        ExitOption.SetActive(false);
    }
}
