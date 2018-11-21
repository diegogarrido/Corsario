using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIslands : MonoBehaviour
{

    public GameObject[] spawns;
    public GameObject[] islands;
    public int cant;
    // Use this for initialization
    void Start()
    {
        Vector3 rotation = new Vector3(0, Random.Range(0, 360), 0);
        this.transform.Rotate(rotation, Space.Self);
        if (cant <= -1)
        {
            float c = Random.Range(1f, 100f);
            if (c > 90)
            {
                cant = (int)spawns.Length * 90 / 100;
            }
            else if (c > 60)
            {
                cant = (int)spawns.Length * 60 / 100;
            }
            else if (c > 30)
            {
                cant = (int)spawns.Length * 30 / 100;
            }
            else
            {
                cant = 1;
            }
        }

        for (int i = 0; i < cant; i++)
        {
            int island = (int)Random.Range(0, islands.Length - 1);
            int spawn = (int)Random.Range(0, spawns.Length - 1);
            rotation = new Vector3(0, Random.Range(0, 360), 0);
            while (spawns[spawn].transform.childCount != 0)
            {
                spawn = (int)Random.Range(0, spawns.Length - 1);
            }
            Instantiate(islands[island], spawns[spawn].transform).transform.Rotate(rotation, Space.Self);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
