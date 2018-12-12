using System.Collections;
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
    private InventoryScript inv;
    private EquippmentScript equ;

    void Start()
    {
        chased = false;
        boat = GetComponent<BoatScript>();
        previous = transform.position;
        spyGlass = GetComponent<BoatSpyGlass>();
        inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        equ = GameObject.FindGameObjectWithTag("Menu").GetComponent<EquippmentScript>();
    }

    void Update()
    {
        if (boat.health > 0)
        {
            Movement();
            Shoot();
        }
        if (chased && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<MusicScript>().Play();
        }
        else if (!chased && GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<MusicScript>().Stop();
        }
    }

    private void Shoot()
    {
        if (!GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().gamePaused)
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
    }

    public void AddCannon(ItemCannon cannon)
    {
        for (int i = 0; i < cannonsLeft.Length; i++)
        {
            if (cannonsLeft[i].transform.childCount == 0)
            {
                Instantiate(cannon.cannon, cannonsLeft[i].transform);
                Instantiate(cannon.cannon, cannonsRight[i].transform);
                break;
            }
        }
    }

    public void RemoveCannon(ItemCannon cannon)
    {
        for (int i = cannonsLeft.Length - 1; i >= 0; i--)
        {
            if (cannonsLeft[i].transform.childCount > 0)
            {
                if (cannonsLeft[i].transform.GetChild(0).gameObject.GetComponent<CannonScript>().cannon.cannonName == cannon.itemName)
                {
                    Destroy(cannonsLeft[i].transform.GetChild(0).gameObject);
                    Destroy(cannonsRight[i].transform.GetChild(0).gameObject);
                    break;
                }
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
            if (cannonsRight[i].transform.childCount > 0)
            {
                if (inv.cannonBallEquiped != -1)
                {
                    cannonsRight[i].GetComponentInChildren<CannonScript>().cannonBall = ((ItemCannonBall)inv.items[inv.cannonBallEquiped]).cannonBall;
                    if (cannonsRight[i].GetComponentInChildren<CannonScript>().Shoot(transform.GetChild(0).gameObject))
                    {
                        equ.UseCannonBall();
                    }
                }
            }
        }
    }

    private IEnumerator ShootLeft()
    {
        for (int i = 0; i < cannonsLeft.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            if (cannonsLeft[i].transform.childCount > 0)
            {
                if(inv.cannonBallEquiped != -1)
                {
                    cannonsLeft[i].GetComponentInChildren<CannonScript>().cannonBall = ((ItemCannonBall)inv.items[inv.cannonBallEquiped]).cannonBall;
                    if (cannonsLeft[i].GetComponentInChildren<CannonScript>().Shoot(transform.GetChild(0).gameObject)) {
                        equ.UseCannonBall();
                    }
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            boat.health -= 50;
        }

        if (collision.gameObject.tag == "Ship")
        {
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
