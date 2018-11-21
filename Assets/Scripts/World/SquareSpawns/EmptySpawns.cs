using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySpawns : MonoBehaviour
{

    public GameObject[] spawns;
    public GameObject[] pirateBoats;
    public GameObject tornado;

    private bool[] spawnUsed;

    void Start()
    {
        spawnUsed = new bool[spawns.Length];
        int rand = Random.Range(0, 100);
        if (rand > 90)
        {
            //spawn treasures under water
        }
        else if (rand > 60)
        {
            Instantiate(tornado,transform);
        }
        else if (rand > 30)
        {
            int ammount = Random.Range(0, spawns.Length / 2);
            for (int i = 0; i < ammount; i++)
            {
                int spawn;
                while (true)
                {
                    spawn = Random.Range(0, spawns.Length);
                    if (!spawnUsed[spawn])
                    {
                        Instantiate(pirateBoats[Random.Range(0, pirateBoats.Length)]).transform.position = spawns[spawn].transform.position;
                        spawnUsed[spawn] = true;
                        break;
                    }
                }
            }
        }
        //else nothing
    }

}
