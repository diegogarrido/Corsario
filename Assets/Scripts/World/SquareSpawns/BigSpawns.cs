using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpawns : MonoBehaviour
{

    public GameObject buildingSpawn;
    public GameObject pirateSpawn;
    public GameObject[] pirateBoats;
    public GameObject playerHouse1;
    public GameObject playerHouse2;
    public GameObject playerHouseFinished;
    public GameObject shop;
    public GameObject pirateBase;
    public GameObject altar;

    public bool forceBase;
    public int x;
    public int z;
    public SquareData data;

    void Start()
    {
        data = DataManager.LoadSquareData(x, z);
        if (data == null)
        {
            data = new SquareData();
            if (forceBase)
            {
                Instantiate(playerHouseFinished, buildingSpawn.transform);
                data.content = "HouseFinished";
                DataManager.SaveSquareData(data, x, z);
            }
            else
            {
                RollBuilding();
            }
        }
        else
        {
            if (data.content == "Shop")
            {
                Instantiate(shop, buildingSpawn.transform);
            }
            else if (data.content == "PirateBase")
            {
                Instantiate(pirateBase, buildingSpawn.transform);
                Instantiate(pirateBoats[pirateBoats.Length - 1], pirateSpawn.transform);
                Instantiate(pirateBoats[Random.Range(0, pirateBoats.Length - 2)], pirateSpawn.transform).transform.localPosition += new Vector3(40, 0, 0);
                Instantiate(pirateBoats[Random.Range(0, pirateBoats.Length - 2)], pirateSpawn.transform).transform.localPosition += new Vector3(0, 0, 40);
                Instantiate(pirateBoats[Random.Range(0, pirateBoats.Length - 2)], pirateSpawn.transform).transform.localPosition += new Vector3(-40, 0, 0);
                Instantiate(pirateBoats[Random.Range(0, pirateBoats.Length - 2)], pirateSpawn.transform).transform.localPosition += new Vector3(0, 0, -40);

            }
            else if (data.content.Contains("House1"))
            {
                GameObject h = Instantiate(playerHouse1, buildingSpawn.transform);
                h.GetComponentInChildren<BuildingScript>().wood = int.Parse(data.content.Split('-')[1]);
                h.GetComponentInChildren<BuildingScript>().rock = int.Parse(data.content.Split('-')[2]);
            }
            else if (data.content.Contains("House2"))
            {
                GameObject h = Instantiate(playerHouse2, buildingSpawn.transform);
                h.GetComponentInChildren<BuildingScript>().wood = int.Parse(data.content.Split('-')[1]);
                h.GetComponentInChildren<BuildingScript>().rock = int.Parse(data.content.Split('-')[2]);
            }
            else if (data.content == "HouseFinished")
            {
                Instantiate(playerHouseFinished, buildingSpawn.transform);
            }
            else if (data.content == "Altar")
            {
                Instantiate(altar, buildingSpawn.transform);
            }
        }
    }

    public void RollBuilding()
    {
        int rand = Random.Range(0, 100);
        string content = "";
        if (rand > 80)
        {
            Instantiate(shop, buildingSpawn.transform);
            content = "Shop";
        }
        else if (rand > 50)
        {
            Instantiate(pirateBase, buildingSpawn.transform);
            content = "PirateBase";
            Instantiate(pirateBoats[pirateBoats.Length - 1], pirateSpawn.transform);
            Instantiate(pirateBoats[Random.Range(0, pirateBoats.Length - 2)], pirateSpawn.transform).transform.localPosition += new Vector3(40, 0, 0);
            Instantiate(pirateBoats[Random.Range(0, pirateBoats.Length - 2)], pirateSpawn.transform).transform.localPosition += new Vector3(0, 0, 40);
            Instantiate(pirateBoats[Random.Range(0, pirateBoats.Length - 2)], pirateSpawn.transform).transform.localPosition += new Vector3(-40, 0, 0);
            Instantiate(pirateBoats[Random.Range(0, pirateBoats.Length - 2)], pirateSpawn.transform).transform.localPosition += new Vector3(0, 0, -40);
        }
        else if (rand > 25)
        {
            GameObject h = Instantiate(playerHouse1, buildingSpawn.transform);
            content = "House1-0-0";
            h.GetComponentInChildren<BuildingScript>().wood = 0;
            h.GetComponentInChildren<BuildingScript>().rock = 0;
        }
        else
        {
            Instantiate(altar, buildingSpawn.transform);
            content = "Altar";
        }
        data.content = content;
        SaveData();
    }

    public void SaveData()
    {
        DataManager.SaveSquareData(data, x, z);
    }
}
