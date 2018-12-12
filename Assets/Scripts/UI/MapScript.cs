using UnityEngine.UI;
using UnityEngine;

public class MapScript : MonoBehaviour
{

    public GameObject map;
    public GameObject section;
    public GameObject world;

    void Start()
    {
        RefreshMap();
        map.SetActive(false);
    }

    public void RefreshMap()
    {
        foreach (Transform child in map.transform)
        {
            Destroy(child.gameObject);
        }
        int x = world.GetComponent<WorldGeneration>().playerCoord[0] - 3;
        int z = world.GetComponent<WorldGeneration>().playerCoord[1] - 3;
        for (int i = x; i < (x + 6); i++)
        {
            for (int j = z; j < (z + 6); j++)
            {
                GameObject sec = Instantiate(section, map.transform);
                for (int k = 0; k < world.transform.childCount; k++)
                {
                    if (world.transform.GetChild(k).gameObject.name == i + "-" + j)
                    {
                        sec.GetComponent<Image>().sprite = world.transform.GetChild(k).GetComponentInChildren<SpriteRenderer>().sprite;
                        if (i == x + 3 && j == z + 3)
                        {
                            GetComponent<MiniMapScript>().ChangeSquare(sec.GetComponent<Image>().sprite);
                            sec.GetComponent<Image>().color = new Color(255,255,0);
                        }
                        break;
                    }
                }
            }
        }
    }

    public void Active(bool active)
    {
        map.SetActive(active);
    }
}
