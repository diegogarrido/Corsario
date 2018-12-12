using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSlotScript : MonoBehaviour {

    public Boat boat;

    private ToolTipScript toolTipScript;

    void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void PointerEnter()
    {
        if (boat != null)
        {
            toolTipScript.title.text = boat.boatName;
            toolTipScript.AddStat("Valor", "" + boat.price);
            toolTipScript.AddStat("Capacidad", "" + boat.capacity);
            toolTipScript.AddStat("Cañones", "" + (boat.cannonsPerSide * 2));
            toolTipScript.AddStat("Puntos de vida", "" + boat.health);
            toolTipScript.AddStat("Velocidad", "" + boat.speed);
            toolTipScript.AddStat("Velocidad de giro", "" + boat.turnSpeed);
            toolTipScript.AddStat("Precio", "" + boat.price);
            toolTipScript.Show();
        }
    }

    public void PointerExit()
    {
        toolTipScript.ClearStats();
        toolTipScript.Hide();
    }
}
