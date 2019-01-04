using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    //de aqui sacaremos el punto de inicio y el final
    public GameObject[] spawns;
    public GameObject ball;
    public GameObject shine;
    GameObject ballPlayer;
    GameObject shineP;
    int inicio;
    int final;

	void Start () {

        StartPuzzle();
    }

    void StartPuzzle(){

         
        inicio = Random.Range(0, (spawns.Length ));
         
        ballPlayer = Instantiate(ball);
        ballPlayer.transform.position = spawns[inicio].transform.position;
       
        //aqui pueden colocar un tag o lo que sea
        ballPlayer.name = "pelotita";
        do
        {
            final = Random.Range(0, (spawns.Length ));
        } while (final==inicio);

        spawns[final].GetComponent<StartPoints>().finish = true;
        shineP = Instantiate(shine);
        shineP.transform.position = spawns[final].transform.position;

    }
}
