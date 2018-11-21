using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{

    public GameObject shootPoint;
    public GameObject cannonBall;
    public Cannon cannon;
    public bool ready;
    public float timeLeft;

    void Start()
    {
        ready = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (ready)
            {
                cannon.Shoot(cannonBall, gameObject, shootPoint);
                ready = false;
                timeLeft = cannon.coolDown;
            }
        }
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            ready = true;
        }
    }

}
