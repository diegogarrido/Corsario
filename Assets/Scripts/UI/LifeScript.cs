using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour {

    public Slider healthBar;

    private BoatScript boat;

    void Start () {
        boat = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>();
        healthBar.maxValue = boat.health;
    }
	
	void Update () {
        healthBar.value = boat.health;
    }
}
