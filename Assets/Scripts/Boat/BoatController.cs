using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public GameObject[] cannonsLeft;
    public GameObject[] cannonsRight;
    public CameraFollow cameraFollowing;
    public bool chased;

    private Vector3 previous;
    private BoatSpyGlass spyGlass;
    private BoatScript boat;

    // Use this for initialization
    void Start()
    {
        chased = false;
        boat = GetComponent<BoatScript>();
        previous = transform.position;
        spyGlass = GetComponent<BoatSpyGlass>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
        if (chased && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
        else if(!chased && GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Pause();
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            IEnumerator shoot = null;
            if (cameraFollowing.looking == "Right")
            {
                shoot = ShootRight();
                StartCoroutine(shoot);
            }
            else if (cameraFollowing.looking == "Left")
            {
                shoot = ShootLeft();
                StartCoroutine(shoot);
            }
        }
    }

    private void Movement()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            transform.position = new Vector3(previous.x, transform.position.y, previous.z);
        }
        if (Input.GetAxis("Horizontal") != 0 && !spyGlass.isZooming)
        {
            previous = transform.position;
            transform.Rotate(Vector3.forward * Input.GetAxis("Horizontal") * boat.turnSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") != 0 && !spyGlass.isZooming)
        {
            previous = transform.position;
            transform.position += transform.up * -Input.GetAxis("Vertical") * boat.speed * Time.deltaTime;
        }
    }

    private IEnumerator ShootRight()
    {
        for (int i = 0; i < cannonsRight.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            cannonsRight[i].GetComponentInChildren<CannonScript>().Shoot(transform.GetChild(0).gameObject);
        }
    }

    private IEnumerator ShootLeft()
    {
        for (int i = 0; i < cannonsLeft.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            cannonsLeft[i].GetComponentInChildren<CannonScript>().Shoot(transform.GetChild(0).gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Terrain")
        {
            boat.health -= 50;
        }

        if(collision.gameObject.tag == "Ship")
        {
            gameObject.GetComponent<AudioSource>().Stop(); 
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
