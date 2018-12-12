using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpawns : MonoBehaviour
{

    public GameObject buildingSpawn;
    public GameObject playerHouse1;
    public GameObject playerHouse2;
    public GameObject playerHouseFinished;
    public GameObject shop;
    public GameObject pirateBase;

    public bool forceBase;
    public int x;
    public int z;

    private SquareData data;

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
                GameObject.FindGameObjectWithTag("Menu").GetComponent<ShopScript>().RollItems();
                GameObject.FindGameObjectWithTag("Menu").GetComponent<ShopScript>().RefreshInventory();
            }
            else if (data.content == "PirateBase")
            {
                //Instantiate(pirateBase, buildingSpawn.transform);
            }
            else if (data.content == "House1")
            {
                Instantiate(playerHouse1, buildingSpawn.transform);
            }
            else if (data.content == "House2")
            {
                Instantiate(playerHouse2, buildingSpawn.transform);
            }
            else if (data.content == "HouseFinished")
            {
                Instantiate(playerHouseFinished, buildingSpawn.transform);
            }
        }
    }

    public void RollBuilding()
    {
        int rand = Random.Range(0, 100);
        string content = "";
        if (rand > 90)
        {
            Instantiate(shop, buildingSpawn.transform);
            content = "Shop";
            GameObject.FindGameObjectWithTag("Menu").GetComponent<ShopScript>().RollItems();
            GameObject.FindGameObjectWithTag("Menu").GetComponent<ShopScript>().RefreshInventory();
        }
        else if (rand > 50)
        {
            Instantiate(playerHouse1, buildingSpawn.transform);
            content = "House1";
        }
        else if (rand > 10)
        {
            //Instantiate(pirateBase, buildingSpawn.transform);
            content = "PirateBase";
        }
        data.content = content;
        DataManager.SaveSquareData(data, x, z);
    }
}
