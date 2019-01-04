using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoints : MonoBehaviour {
    public bool finish;
    void OnTriggerEnter(Collider col)
    {

        print(col.gameObject.name);
        if(col.gameObject.name=="pelotita" && finish){
            //el jugador ha ganado
            print("juego terminado");
        }

    }
}
