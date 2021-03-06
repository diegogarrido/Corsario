﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CannonScript : MonoBehaviour
{

    public GameObject[] shootPoints;
    public GameObject cannonBall;
    public Cannon cannon;
    public bool ready;
    public float timeLeft;
    public int shoots;

    private int shootPoint;

    void Start()
    {
        shootPoint = 0;
        shoots = cannon.shoots;
        ready = true;
    }
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else if (!ready)
        {
            ready = true;
            timeLeft = 0;
            shoots = cannon.shoots;
        }
    }

    public void Reload()
    {
        shoots = 0;
        timeLeft = cannon.coolDown;
        ready = false;
    }

    public bool Shoot(GameObject shooter)
    {
        if (ready)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().Play();

            cannon.Shoot(cannonBall, gameObject, shootPoints[shootPoint], shooter);
            shootPoint++;
            if (shootPoint >= shootPoints.Length)
            {
                shootPoint = 0;
            }
            shoots--;
            if (shoots == 0)
            {
                Reload();
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
