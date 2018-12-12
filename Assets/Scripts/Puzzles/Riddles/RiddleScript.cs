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

    Random rnd = new Random();


    void Start()
    {

        int rid = Random.Range(0, riddles.Length);

        for (int i = 0; i < riddles[rid].answers.Length; i++)
        {
            GameObject b = Instantiate(boton, panel.transform);
            b.GetComponentInChildren<Text>().text = riddles[rid].answers[i];

        }
        texto.GetComponent<Text>().text = riddles[rid].riddle;
    }

    // Update is called once per frame
    void Update()
    {

    }

   public void Win()
    {
        RollItem();
    }

    public void RollItem()
    {
       /* InventoryScript inv = GameObject.FindGameObject("Menu").GetComponent<InventoryScript>();


        item = inve.items[Random.Range(0, inventory.items.Length)];
        if (Random.Range(0, 100) >= (item.rarity * 10) - 10)
        {
            quantity = Random.Range(1, 200 / (item.rarity * 10));
        }
        else
        {
            RollItem();
        } */
    }


}
