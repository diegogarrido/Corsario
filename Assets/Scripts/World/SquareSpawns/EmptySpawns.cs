using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySpawns : MonoBehaviour
{

    public GameObject[] spawns;
    public GameObject[] pirateBoats;
    public GameObject[] officerBoats;
    public GameObject tornado;
    public GameObject resourceCrate;
    public GameObject floatingChest;

    private bool[] spawnUsed;

    void Start()
    {
        spawnUsed = new bool[spawns.Length];
        int rand = Random.Range(0, 100);
        if (rand > 80)
        {
            Instantiate(officerBoats[officerBoats.Length - 1], transform).transform.localPosition = new Vector3(500,0,500);
            Instantiate(floatingChest,transform).transform.localPosition = new Vector3(500, 0, 480);
            GameObject b = Instantiate(officerBoats[Random.Range(0, officerBoats.Length - 2)], transform);
            b.transform.localPosition += new Vector3(450, 0, 450);
            b.GetComponent<BoatOfficer>().patrolPoints = new Vector3[] { transform.position + new Vector3(50, 0, 50), transform.position + new Vector3(950, 0, 50) };
            b = Instantiate(officerBoats[Random.Range(0, officerBoats.Length - 2)], transform);
            b.transform.localPosition += new Vector3(550, 0, 550);
            b.GetComponent<BoatOfficer>().patrolPoints = new Vector3[] { transform.position + new Vector3(50, 0, 950), transform.position + new Vector3(950, 0, 950) };
        }
        else if (rand > 60)
        {
            Instantiate(tornado, transform);
            int ammount = Random.Range(3, 8);
            for (int i = 0; i < ammount; i++)
            {
                Vector3 position = new Vector3(transform.position.x + 500 + Random.Range(-250, 250), 0, transform.position.z + 500 + Random.Range(-250, 250));
                Instantiate(resourceCrate, position, Quaternion.identity, transform);
            }
        }
        else if (rand > 30)
        {
            int ammount = Random.Range(3, spawns.Length);
            for (int i = 0; i < ammount; i++)
            {
                int spawn;
                while (true)
                {
                    spawn = Random.Range(0, spawns.Length);
                    if (!spawnUsed[spawn])
                    {
                        Instantiate(pirateBoats[Random.Range(0, pirateBoats.Length)], transform).transform.position = spawns[spawn].transform.position;
                        spawnUsed[spawn] = true;
                        break;
                    }
                }
            }
            Instantiate(floatingChest, transform).transform.localPosition = new Vector3(500, 0.5f, 500);
        }
        //else nothing
    }

}
