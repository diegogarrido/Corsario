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
    public GameObject[,] map;
    public int[] playerCoord;

    private float spacing = 1000f;

    private void Awake()
    {
        PlayerPrefs.SetString("LastPlayed", DataManager.saveName);
        GameData data = DataManager.LoadData();
        if (data != null)
        {
            playerCoord = new int[] { data.playerX, data.playerZ };
            Vector3 position = new Vector3((data.playerX * spacing) + Mathf.Sign(data.playerX), 0, (data.playerZ * spacing) + Mathf.Sign(data.playerZ));
            player.gameObject.transform.position = position;
        }
        else
        {
            playerCoord = new int[2];
        }
    }

    void Start()
    {
        if (DataManager.MapCreated())
        {
            map = new GameObject[6, 6];
            for (int i = playerCoord[0] - 3; i < playerCoord[0] + 3; i++)
            {
                for (int j = playerCoord[1] - 3; j < playerCoord[1] + 3; j++)
                {
                    MapSquare sq = DataManager.LoadSquare(i, j);
                    CreateSquare(sq.islandType, sq.islandIndex, sq.CoordX, sq.CoordZ);
                }
            }
        }
        else
        {
            GenerateStart();
        }
    }

    void Update()
    {
        if (playerCoord != null && playerCoord.Length > 0)
        {
            CheckCoordinates();
        }
    }

    private void CheckCoordinates()
    {
        if (player.transform.position.x > ((playerCoord[0] * spacing) + spacing))
        {
            playerCoord[0]++;
            SaveData();
            for (int i = -3; i <= 2; i++)
            {
                Destroy(GameObject.Find((playerCoord[0] - 4) + "-" + (playerCoord[1] + i)));
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
        }
        if (player.transform.position.x < (playerCoord[0] * spacing))
        {
            playerCoord[0]--;
            SaveData();
            for (int i = -3; i <= 2; i++)
            {
                Destroy(GameObject.Find((playerCoord[0] + 3) + "-" + (playerCoord[1] + i)));
                if (DataManager.SquareCreated(playerCoord[0] - 3, playerCoord[1] + i))
                {
                    MapSquare sq = DataManager.LoadSquare(playerCoord[0] - 3, playerCoord[1] + i);
                    CreateSquare(sq.islandType, sq.islandIndex, sq.CoordX, sq.CoordZ);
                }
                else
                {
                    GenerateSquare(playerCoord[0] - 3, playerCoord[1] + i);
                }
            }
        }
        if (player.transform.position.z > ((playerCoord[1] * spacing) + spacing))
        {
            playerCoord[1]++;
            SaveData();
            for (int i = -3; i <= 2; i++)
            {
                Destroy(GameObject.Find((playerCoord[0] + i) + "-" + (playerCoord[1] - 4)));
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
        }
        if (player.transform.position.z < (playerCoord[1] * spacing))
        {
            playerCoord[1]--;
            SaveData();
            for (int i = -3; i <= 2; i++)
            {
                Destroy(GameObject.Find((playerCoord[0] + i) + "-" + (playerCoord[1] + 3)));
                if (DataManager.SquareCreated(playerCoord[0] + i, playerCoord[1] - 3))
                {
                    MapSquare sq = DataManager.LoadSquare(playerCoord[0] + i, playerCoord[1] - 3);
                    CreateSquare(sq.islandType, sq.islandIndex, sq.CoordX, sq.CoordZ);
                }
                else
                {
                    GenerateSquare(playerCoord[0] + i, playerCoord[1] - 3);
                }
            }
        }
    }

    private void CreateSquare(string type, int islandIndex, int x, int z)
    {
        GameObject o = new GameObject(x + "-" + z);
        o.tag = "MapSquare";
        o.transform.SetParent(transform);
        o.transform.localPosition = new Vector3(x * spacing, 0, z * spacing);
        if (type == "Big")
        {
            Instantiate(terrainsBig[islandIndex], o.transform);
        }
        else if (type == "Medium")
        {
            Instantiate(terrainsMedium[islandIndex], o.transform);
        }
        else if (type == "Small")
        {
            Instantiate(terrainsSmall[islandIndex], o.transform);
        }
        else
        {
            Instantiate(terrainEmpty, o.transform);
        }
    }

    private void GenerateSquare(int x, int z)
    {
        GameObject o = new GameObject(x + "-" + z);
        o.tag = "MapSquare";
        string iType;
        o.transform.SetParent(transform);
        o.transform.localPosition = new Vector3(x * spacing, 0, z * spacing);
        int type = Random.Range(0, 100);
        GameObject m;
        int number = -1;
        if (type >= 95)
        {
            iType = "Big";
            number = Random.Range(0, terrainsBig.Length);
            m = Instantiate(terrainsBig[number], o.transform);
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
        }
        else
        {
            iType = "Empty";
            m = Instantiate(terrainEmpty, o.transform);
        }
        //map[x, z] = m;
        MapSquare square = new MapSquare();
        square.CoordX = x;
        square.CoordZ = z;
        square.islandType = iType;
        square.islandIndex = number;
        DataManager.SaveSquare(square);
    }

    private void GenerateStart()
    {
        float x = 0;
        map = new GameObject[6, 6];
        for (int i = 0; i < map.Length / Mathf.Sqrt(map.Length); i++)
        {
            float z = 0;
            for (int j = 0; j < map.Length / Mathf.Sqrt(map.Length); j++)
            {
                GenerateSquare(i, j);
                z += spacing;
            }
            x += spacing;
        }
        playerCoord = new int[] { 3, 3 };
        SaveData();
    }

    private void SaveData()
    {
        GameData data = new GameData();
        data.playerX = playerCoord[0];
        data.playerZ = playerCoord[1];
        DataManager.SaveData(data);
    }
}
