using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpawns : MonoBehaviour
{

    public GameObject[] spawns;
    public GameObject treasureChest;
    public GameObject resourceCrate;
    public int x;
    public int z;
    public SquareData data;

    void Start()
    {
        data = DataManager.LoadSquareData(x, z);
        if (data != null)
        {
            if (data.content.Split('-')[0] == "Treasure")
            {
                GameObject t = Instantiate(treasureChest, spawns[int.Parse(data.content.Split('-')[2])].transform);
                if (data.content.Split('-')[1] == "Opened")
                {
                    t.GetComponent<Animator>().SetBool("Opened", true);
                }
            }
            else if (data.content == "Resources")
            {
                for (int i = 0; i < spawns.Length; i++)
                {
                    GameObject c = Instantiate(resourceCrate, spawns[i].transform);
                    Destroy(c.GetComponent<BoatPhysics>());
                    c.GetComponent<Rigidbody>().freezeRotation = false;
                    c.transform.localPosition += new Vector3(0, 1, 0);
                    c.GetComponentInChildren<MeshCollider>().isTrigger = false;
                }
            }
        }
        else
        {
            int rand = Random.Range(0, 100);
            if (rand > 40)
            {
                for (int i = 0; i < spawns.Length; i++)
                {
                    GameObject c = Instantiate(resourceCrate, spawns[i].transform);
                    Destroy(c.GetComponent<BoatPhysics>());
                    c.GetComponent<Rigidbody>().freezeRotation = false;
                    c.transform.localPosition += new Vector3(0, 1, 0);
                    c.GetComponentInChildren<MeshCollider>().isTrigger = false;
                }
                data = new SquareData();
                data.content = "Resources";
                DataManager.SaveSquareData(data, x, z);
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
