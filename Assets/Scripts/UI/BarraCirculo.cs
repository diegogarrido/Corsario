using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarraCirculo : MonoBehaviour {

    public Image circulo;
    public float porcentaje = 0;
   
    private BoatController boatCont;

	 
	void Update () {
        circulo.fillAmount = porcentaje / 100;
        
	}


    void RechargeTime()
    {
      

    }
}
