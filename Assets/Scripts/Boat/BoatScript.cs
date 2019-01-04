using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour {

    public Boat boat;
    public float health;
    public float maxWeight;
    public float speed;
    public float turnSpeed;
    public bool loaded;

	void Awake () {
        loaded = false;
        health = boat.health;
        maxWeight = boat.capacity;
        speed = boat.speed;
        turnSpeed = boat.turnSpeed;
        loaded = true;
	}
	
	void Update () {
        if (health <= 0) {
            Component.Destroy(GetComponent<MeshFilter>());
            Component.Destroy(GetComponent<BoatPhysics>());
            GetComponent<Rigidbody>().freezeRotation = false;
        }
    }

}
