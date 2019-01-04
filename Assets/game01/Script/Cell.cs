using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cell : MonoBehaviour {

    public bool ship;
    public GameObject explotion;
    public GameObject wrong;
    GameObject manager;
    public int NumShip;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("manager");
       
    }
    public void IsShip() {
        int direc = Random.Range(1, 3);


        if (ship)
        {
            explotion.SetActive(true);
            manager.GetComponent<Control>().Acierto(NumShip);

        }
        else
        {
            wrong.SetActive(true);
            manager.GetComponent<Control>().QuitarVida();

        }
    }

}
