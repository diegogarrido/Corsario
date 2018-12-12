using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatEnemy : MonoBehaviour
{

    public float minDistance;
    public GameObject[] cannonsLeft;
    public GameObject[] cannonsRight;
    public string playerIs;
    public string playerAt;
    public bool chasing;
    public GameObject resourceCrate;

    private BoatScript boat;
    private bool dying;

    void Start()
    {
        dying = false;
        playerIs = "None";
        playerAt = "None";
        boat = GetComponent<BoatScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boat.loaded)
        {
            if (boat.health <= 0 && !dying)
            {
                StartCoroutine(DieIn(5));
                dying = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<BoatController>().chased = false;
            chasing = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && boat.health > 0)
        {
            GameObject player = other.gameObject;
            player.GetComponent<BoatController>().chased = true;
            Vector3 targetPoint = transform.position - player.transform.position;
            Vector3 dirFromAtoB = targetPoint.normalized;
            float dotProd = Vector3.Dot(dirFromAtoB, transform.up);
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (transform.InverseTransformPoint(player.transform.position).x < 0)
            {
                playerIs = "Left";
            }
            else if (transform.InverseTransformPoint(player.transform.position).x >= 0)
            {
                playerIs = "Right";
            }
            else
            {
                playerIs = "None";
            }

            if (dotProd > 0.9)
            {
                playerAt = "Front";
            }
            else if (dotProd <= 0.1 && dotProd >= -0.1)
            {
                playerAt = "Shooting Range";
            }
            else if (dotProd < -0.9)
            {
                playerAt = "Back";
            }
            else if ((dotProd <= 0.9 && dotProd > 0.1) || (dotProd >= -0.9 && dotProd < -0.1))
            {
                playerAt = "Side";
            }

            if (distance > minDistance)
            {
                if (playerAt != "Front")
                {
                    if (playerIs == "Right")
                    {
                        TurnRight(1f);
                    }
                    else if (playerIs == "Left")
                    {
                        TurnLeft(1f);
                    }
                }

                float mod = 1;
                if (distance < (minDistance * 1.5))
                {
                    mod = 0.5f;
                }

                if (playerAt == "Back")
                {
                    GoBackward(mod);
                }
                else if (playerAt == "Front")
                {
                    GoForward(mod);
                }
            }
            else
            {
                if (playerAt != "Shooting Range")
                {
                    if (playerAt == "Front" || (playerAt == "Side" && dotProd > 0))
                    {
                        if (playerIs == "Right")
                        {
                            TurnLeft(1);
                        }
                        else if (playerIs == "Left")
                        {
                            TurnRight(1);
                        }
                    }
                    else if (playerAt == "Back" || (playerAt == "Side" && dotProd < 0))
                    {
                        if (playerIs == "Right")
                        {
                            TurnRight(1);
                        }
                        else if (playerIs == "Left")
                        {
                            TurnLeft(1);
                        }
                    }
                }
                else if (playerAt == "Shooting Range")
                {
                    if (!GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().gamePaused)
                    {
                        IEnumerator shoot = null;
                        if (playerIs == "Right")
                        {
                            shoot = ShootRight();
                        }
                        else if (playerIs == "Left")
                        {
                            shoot = ShootLeft();
                        }
                        StartCoroutine(shoot);
                    }
                }
            }
        }
        else if (other.gameObject.tag == "Player" && boat.health <= 0)
        {
            other.gameObject.GetComponent<BoatController>().chased = false;
        }
    }

    private void TurnRight(float modifier)
    {
        transform.Rotate(Vector3.forward * boat.turnSpeed * 1.5f * modifier * Time.deltaTime);
    }

    private void TurnLeft(float modifier)
    {
        transform.Rotate(Vector3.forward * -boat.turnSpeed * 1.5f * modifier * Time.deltaTime);
    }

    private void GoForward(float modifier)
    {
        transform.position += transform.up * -boat.speed * modifier * Time.deltaTime;
    }

    private void GoBackward(float modifier)
    {
        transform.position += transform.up * boat.speed * modifier * Time.deltaTime;
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

    private IEnumerator DieIn(float sec)
    {
        int cant = Random.Range(1, 5);
        for (int i = 0; i < cant; i++)
        {
            Vector3 cratePosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            Instantiate(resourceCrate).transform.position = transform.position + cratePosition;
        }
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
}
