using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    public GameObject player;
    public GameObject[] terrainsSmall;
    public GameObject[] terrainsMedium;
    public GameObject[] terrainsBig;
    public GameObject terrainEmpty;
    public int[] playerCoord;
    public float spacing = 1000f;

    void Update()
    {
        if (playerCoord != null && playerCoord.Length > 0)
        {
            CheckCoordinates();
        }
    }

    private void CheckCoordinates()
    {
        MapScript map = GameObject.FindGameObjectWithTag("Menu").GetComponent<MapScript>();
        if (player.transform.position.x > ((playerCoord[0] * spacing) + spacing))
        {
            playerCoord[0]++;
            SaveData();
            for (int i = -2; i <= 2; i++)
            {
                Destroy(GameObject.Find((playerCoord[0] - 3 ) + "-" + (playerCoord[1] + i)));
                if (DataManager.SquareCreated(playerCoord[0] + 2, playerCoord[1] + i))
                {
                    MapSquare sq = DataManager.LoadSquare(playerCoord[0] + 2, playerCoord[1] + i);
                    CreateSquare(sq.islandType, sq.islandIndex, sq.CoordX, sq.CoordZ);
                }
                else
                {
                    GenerateSquare(playerCoord[0] + 2, playerCoord[1] + i);
                }
            }
            map.RefreshMap();
        }
        if (player.transform.position.x < (playerCoord[0] * spacing))
        {
            playerCoord[0]--;
            SaveData();
            for (int i = -2; i <= 2; i++)
            {
                Destroy(GameObject.Find((playerCoord[0] + 3) + "-" + (playerCoord[1] + i)));
                if (DataManager.SquareCreated(playerCoord[0] - 2, playerCoord[1] + i))
                {
                    MapSquare sq = DataManager.LoadSquare(playerCoord[0] - 2, playerCoord[1] + i);
                    CreateSquare(sq.islandType, sq.islandIndex, sq.CoordX, sq.CoordZ);
                }
                else
                {
                    GenerateSquare(playerCoord[0] - 2, playerCoord[1] + i);
                }
            }
            map.RefreshMap();
        }
        if (player.transform.position.z > ((playerCoord[1] * spacing) + spacing))
        {
            playerCoord[1]++;
            SaveData();
            for (int i = -2; i <= 2; i++)
            {
                Destroy(GameObject.Find((playerCoord[0] + i) + "-" + (playerCoord[1] - 3)));
                if (DataManager.SquareCreated(playerCoord[0] + i, playerCoord[1] + 2))
                {
                    MapSquare sq = DataManager.LoadSquare(playerCoord[0] + i, playerCoord[1] + 2);
                    CreateSquare(sq.islandType, sq.islandIndex, sq.CoordX, sq.CoordZ);
                }
                else
                {
                    GenerateSquare(playerCoord[0] + i, playerCoord[1] + 2);
                }
            }
            map.RefreshMap();
        }
        if (player.transform.position.z < (playerCoord[1] * spacing))
        {
            playerCoord[1]--;
            SaveData();
            for (int i = -2; i <= 2; i++)
            {
                Destroy(GameObject.Find((playerCoord[0] + i) + "-" + (playerCoord[1] + 3)));
                if (DataManager.SquareCreated(playerCoord[0] + i, playerCoord[1] - 2))
                {
                    MapSquare sq = DataManager.LoadSquare(playerCoord[0] + i, playerCoord[1] - 2);
                    CreateSquare(sq.islandType, sq.islandIndex, sq.CoordX, sq.CoordZ);
                }
                else
                {
                    GenerateSquare(playerCoord[0] + i, playerCoord[1] - 2);
                }
            }
            map.RefreshMap();
        }
    }

    public void CreateSquare(string type, int islandIndex, int x, int z)
    {
        GameObject o = new GameObject(x + "-" + z);
        o.tag = "MapSquare";
        o.transform.SetParent(transform);
        o.transform.localPosition = new Vector3(x * spacing, 0, z * spacing);
        if (type == "Big")
        {
            GameObject sq = Instantiate(terrainsBig[islandIndex], o.transform);
            sq.GetComponent<BigSpawns>().x = x;
            sq.GetComponent<BigSpawns>().z = z;
        }
        else if (type == "Medium")
        {
            Instantiate(terrainsMedium[islandIndex], o.transform);
        }
        else if (type == "Small")
        {
            GameObject sq = Instantiate(terrainsSmall[islandIndex], o.transform);
            sq.GetComponent<SmallSpawns>().x = x;
            sq.GetComponent<SmallSpawns>().z = z;
        }
        else
        {
            Instantiate(terrainEmpty, o.transform);
        }
    }

    public void GenerateSquare(int x, int z)
    {
        GameObject o = new GameObject(x + "-" + z);
        o.tag = "MapSquare";
        string iType;
        o.transform.SetParent(transform);
        o.transform.localPosition = new Vector3(x * spacing, 0, z * spacing);
        int type = Random.Range(0, 100);
        GameObject m;
        int number = -1;
        if (x == 2 && z == 2)
        {
            type = 99;
        }
        if (type >= 95)
        {
            iType = "Big";
            number = Random.Range(0, terrainsBig.Length);
            m = Instantiate(terrainsBig[number], o.transform);
            m.GetComponent<BigSpawns>().x = x;
            m.GetComponent<BigSpawns>().z = z;
            if (x == 2 && z == 2)
            {
                m.GetComponent<BigSpawns>().forceBase = true;
            }
        }
        else if (type >= 85)
        {
            iType = "Medium";
            number = Random.Range(0, terrainsMedium.Length);
            m = Instantiate(terrainsMedium[number], o.transform);
        }
        else if (type >= 70)
        {
            iType = "Small";
            number = Random.Range(0, terrainsSmall.Length);
            m = Instantiate(terrainsSmall[number], o.transform);
            m.GetComponent<SmallSpawns>().x = x;
            m.GetComponent<SmallSpawns>().z = z;
        }
        else
        {
            iType = "Empty";
            m = Instantiate(terrainEmpty, o.transform);
        }
        MapSquare square = new MapSquare();
        square.CoordX = x;
        square.CoordZ = z;
        square.islandType = iType;
        square.islandIndex = number;
        DataManager.SaveSquare(square);
    }

    public void GenerateStart()
    {
        float x = 0;
        for (int i = 0; i < 5; i++)
        {
            float z = 0;
            for (int j = 0; j < 5; j++)
            {
                GenerateSquare(i, j);
                z += spacing;
            }
            x += spacing;
        }
    }

    public void SaveData()
    {
        GameData data = new GameData();
        data.playerX = playerCoord[0];
        data.playerZ = playerCoord[1];
        data.boat = GetComponent<PlayerSpawner>().boat;
        DataManager.SaveData(data);
    }
}
