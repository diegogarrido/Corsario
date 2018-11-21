using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour {

    public CannonBall cannonBall;
    public GameObject shooter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            //Splash
        }
        if(other.gameObject.tag == "Ship" && !Object.ReferenceEquals(shooter,other.gameObject))
        {
            other.gameObject.GetComponentInParent<BoatScript>().health -= cannonBall.damage;
        }
        if(other.gameObject.name == "Terrain" || other.gameObject.tag == "Ship" && !Object.ReferenceEquals(shooter, other.gameObject))
        {
            Destroy(gameObject);
        }
    }
}
