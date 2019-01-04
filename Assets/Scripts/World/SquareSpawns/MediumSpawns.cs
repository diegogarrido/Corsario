using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumSpawns : MonoBehaviour
{

    public GameObject[] trees;
    public GameObject[] rocks;
    public GameObject resourceSpawn;
    public int x;
    public int z;
    public SquareData data;

    void Start()
    {
        data = DataManager.LoadSquareData(x, z);
        if (data != null)
        {
            switch (data.content)
            {
                case "Wood":
                    Instantiate(trees[Random.Range(0, trees.Length)], resourceSpawn.transform);
                    break;
                case "Rock":
                    Instantiate(rocks[Random.Range(0, rocks.Length)], resourceSpawn.transform);
                    break;
            }
        }
        else
        {
            data = new SquareData();
            int resource = Random.Range(0, 1);
            switch (resource)
            {
                case 0:
                    Instantiate(trees[Random.Range(0, trees.Length)], resourceSpawn.transform);
                    data.content = "Wood";
                    break;
                case 1:
                    Instantiate(rocks[Random.Range(0, rocks.Length)], resourceSpawn.transform);
                    data.content = "Rock";
                    break;
            }
            DataManager.SaveSquareData(data, x, z);
        }
    }

}
