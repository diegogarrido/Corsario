using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RiddleScript : MonoBehaviour
{

    public Riddle[] riddles;
    public GameObject panel;
    public GameObject boton;
    public GameObject panelText;
    public GameObject texto;

    void Start()
    {

        int rid = Random.Range(0, riddles.Length);

        for (int i = 0; i < riddles[rid].answers.Length; i++)
        {
            GameObject b = Instantiate(boton, panel.transform);
            b.GetComponentInChildren<Text>().text = riddles[rid].answers[i];
            b.GetComponent<ButtonScript>().number = i;
            b.GetComponent<ButtonScript>().ridd = riddles[rid];
        }
        texto.GetComponent<Text>().text = riddles[rid].riddle;
    }

}
