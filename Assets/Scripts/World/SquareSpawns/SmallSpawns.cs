using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpawns : MonoBehaviour
{

    public GameObject[] spawns;
    public GameObject treasureChest;
    public int x;
    public int z;

    private SquareData data;

    void Start()
    {
        data = DataManager.LoadSquareData(x, z);
        if (data != null)
        {
            if (data.content.Split('-')[0] == "Treasure")
            {
                GameObject t = Instantiate(treasureChest, spawns[int.Parse(data.content.Split('-')[2])].transform);
                if(data.content.Split('-')[1] == "Opened")
                {
                    t.GetComponent<Animator>().SetBool("Opened", true);
                }
            }
        }
        else
        {
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
            else
            {
                int spawn = Random.Range(0, spawns.Length);
                Instantiate(treasureChest, spawns[spawn].transform);
                data = new SquareData();
                data.content = "Treasure-Closed-" + spawn;
                DataManager.SaveSquareData(data, x, z);
            }
        }
    }

}
