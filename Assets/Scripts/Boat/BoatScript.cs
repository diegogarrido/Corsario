using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour {

    public Boat boat;
    public float health;
    public float maxCargo;
    public float cargo;
    public float speed;
    public float turnSpeed;
    public bool loaded;

	void Start () {
        loaded = false;
        health = boat.health;
        maxCargo = boat.capacity;
        cargo = 0;
        speed = boat.speed;
        turnSpeed = boat.turnSpeed;
        loaded = true;
	}
	
	void Update () {
        if (health <= 0) {
            Component.Destroy(GetComponent<MeshFilter>());
            Component.Destroy(GetComponent<BoatPhysics>());
        }
    }

}
