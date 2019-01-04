using System.Collections;
using UnityEngine;

public class TutorialBoatController : MonoBehaviour
{
    public GameObject[] cannonsLeft;
    public GameObject[] cannonsRight;
    public CameraFollow cameraFollowing;
    public bool chased;
    public float speed;
    public bool died;

    private Vector3 previous;
    private BoatSpyGlass spyGlass;
    private BoatScript boat;
    private TutorialInventoryScript inv;
    private TutorialEquippmentScript equ;

    void Start()
    {
        died = false;
        speed = 0;
        chased = false;
        boat = GetComponent<BoatScript>();
        previous = transform.position;
        spyGlass = GetComponent<BoatSpyGlass>();
        inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialInventoryScript>();
        equ = GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialEquippmentScript>();
    }

    void Update()
    {
        if (boat.health > 0)
        {
            if (GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialMenuController>().mainUI.activeInHierarchy && !GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialMenuController>().gamePaused)
            {
                Vector3 prev = new Vector3(previous.x, 0, previous.z);
                Vector3 now = new Vector3(transform.position.x, 0, transform.position.z);
                speed = ((now - prev) / Time.deltaTime).magnitude;
                Movement();
                Shoot();
                Reload();
                Repair();
            }
        }
        else if (!died)
        {
            inv.Die();
            died = true;
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

    private void Repair()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int index = inv.FindItem(inv.items[0]);
            if (index != -1)
            {
                if (inv.playerItemsQuantities[index] >= 5 && boat.health < boat.boat.health)
                {
                    GameObject.FindGameObjectWithTag("Menu").GetComponent<RepairScript>().repairing = true;
                }
            }
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < cannonsRight.Length; i++)
            {
                if (cannonsRight[i].transform.childCount > 0)
                {
                    if (inv.cannonBallEquiped != -1)
                    {
                        GameObject.FindGameObjectWithTag("Menu").GetComponent<RepairScript>().repairing = false;
                        cannonsRight[i].GetComponentInChildren<CannonScript>().Reload();
                        cannonsLeft[i].GetComponentInChildren<CannonScript>().Reload();
                    }
                }
            }
        }
    }

    private void Shoot()
    {
        if (!GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialMenuController>().gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                IEnumerator shoot = null;
                if (cameraFollowing.looking == "Right")
                {
                    GameObject.FindGameObjectWithTag("Menu").GetComponent<RepairScript>().repairing = false;
                    shoot = ShootRight();
                    StartCoroutine(shoot);
                }
                else if (cameraFollowing.looking == "Left")
                {
                    GameObject.FindGameObjectWithTag("Menu").GetComponent<RepairScript>().repairing = false;
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
                TutorialCooldownScript barra = GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialCooldownScript>();
                GameObject l = Instantiate(cannon.cannon, cannonsLeft[i].transform);
                GameObject r = Instantiate(cannon.cannon, cannonsRight[i].transform);
                if (barra.cooldownsLeft.transform.childCount > 0)
                {
                    barra.EquipCannos(l.GetComponent<CannonScript>(), r.GetComponent<CannonScript>(), i);
                }
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
                    TutorialCooldownScript barra = GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialCooldownScript>();
                    if (barra.cooldownsLeft.transform.childCount > 0)
                    {
                        barra.EquipCannos(null, null, i);
                    }
                    break;
                }
            }
        }
    }

    private void Movement()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
        }
        if (Input.GetAxis("Horizontal") != 0 && !spyGlass.isZooming)
        {
            GameObject.FindGameObjectWithTag("Menu").GetComponent<RepairScript>().repairing = false;
            previous = transform.position;
            Vector3 rotation = Vector3.forward * Input.GetAxis("Horizontal") * boat.turnSpeed * Time.deltaTime;
            transform.Rotate(rotation);
            GameObject.FindGameObjectWithTag("Compass").GetComponent<Compass>().north.transform.Rotate(-rotation);
        }
        if (Input.GetAxis("Vertical") != 0 && !spyGlass.isZooming)
        {
            GameObject.FindGameObjectWithTag("Menu").GetComponent<RepairScript>().repairing = false;
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
                if (inv.cannonBallEquiped != -1)
                {
                    cannonsLeft[i].GetComponentInChildren<CannonScript>().cannonBall = ((ItemCannonBall)inv.items[inv.cannonBallEquiped]).cannonBall;
                    if (cannonsLeft[i].GetComponentInChildren<CannonScript>().Shoot(transform.GetChild(0).gameObject))
                    {
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ship" || collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<BoatScript>().health > 0)
            {
                collision.gameObject.GetComponent<BoatScript>().health -= speed * GetComponent<BoatScript>().boat.rammingDamage;
                GetComponent<BoatScript>().health -= speed;
                gameObject.GetComponent<AudioSource>().Stop();
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
}
