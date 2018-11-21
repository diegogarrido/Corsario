using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpawns : MonoBehaviour
{

    public GameObject[] spawns;
    public GameObject treasureChest;

    private bool[] spawnUsed;

    void Start()
    {
        spawnUsed = new bool[spawns.Length];
        int rand = Random.Range(0, 100);
        if (rand > 40)
        {
            /*while (true)
            {
                int spawn = Random.Range(0, spawns.Length);
                if (!spawnUsed[spawn])
                {
                    //Spawn resource crate
                    spawnUsed[spawn] = true;
                    break;
                }
            }*/
        }
        else if (rand > 10)
        {
            while (true)
            {
                int spawn = Random.Range(0, spawns.Length);
                if (!spawnUsed[spawn])
                {
                    //Instantiate(treasureChest).transform.position = spawns[spawn].transform.position;
                    spawnUsed[spawn] = true;
                    break;
                }
            }
        }
    }

}
