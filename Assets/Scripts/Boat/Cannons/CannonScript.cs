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
    public int shoots;

    void Start()
    {
        shoots = cannon.shoots;
        ready = true;
    }
    void Update()
    {
        
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            ready = true;
        }
    }

    public void Shoot(GameObject shooter)
    {
        

        if (ready)
        {
            gameObject.GetComponent<AudioSource>().Stop(); 
            gameObject.GetComponent<AudioSource>().Play();
          
            cannon.Shoot(cannonBall, gameObject, shootPoint,shooter);
            shoots--;
            if (shoots == 0)
            {
                timeLeft = cannon.coolDown;
                shoots = cannon.shoots;
                ready = false;
            }
        }
    }
}
