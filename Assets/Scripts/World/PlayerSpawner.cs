using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject[] boats;
    public int boat;

    private WorldGeneration world;

    void Awake()
    {
        world = GetComponent<WorldGeneration>();
        LoadData();
    }

    public void LoadData()
    {
        PlayerPrefs.SetString("LastPlayed", DataManager.saveName);
        GameData data = DataManager.LoadData();
        if (data != null)
        {
            int x = data.playerX;
            int z = data.playerZ;
            boat = data.boat;
            world.player = Instantiate(boats[boat]);
            world.playerCoord = new int[] { x, z };
            Vector3 position = new Vector3((x * world.spacing) + 1, 0, (z * world.spacing) + 1);
            world.player.gameObject.transform.position = position;
        }
        else
        {
            boat = 0;
            world.player = Instantiate(boats[boat]);
            world.playerCoord = new int[] { 2, 2 };
            Vector3 position = new Vector3((2 * world.spacing) + 1, 0, (2 * world.spacing) + 1);
            world.player.gameObject.transform.position = position;
        }
        if (DataManager.MapCreated())
        {
            for (int i = world.playerCoord[0] - 2; i < world.playerCoord[0] + 3; i++)
            {
                for (int j = world.playerCoord[1] - 2; j < world.playerCoord[1] + 3; j++)
                {
                    MapSquare sq = DataManager.LoadSquare(i, j);
                    world.CreateSquare(sq.islandType, sq.islandIndex, sq.CoordX, sq.CoordZ);
                }
            }
        }
        else
        {
            world.GenerateStart();
        }
        world.SaveData();
    }
}
