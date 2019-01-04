using UnityEngine.UI;
using UnityEngine;

public class MapScript : MonoBehaviour
{

    public GameObject map;
    public GameObject panel;
    public GameObject section;
    public GameObject world;
    public GameObject playerDot;

    void Start()
    {
        RefreshMap();
        map.SetActive(false);
        panel.SetActive(false);
    }

    public void RefreshMap()
    {
        foreach (Transform child in map.transform)
        {
            Destroy(child.gameObject);
        }
        int x = world.GetComponent<WorldGeneration>().playerCoord[0] - 2;
        int z = world.GetComponent<WorldGeneration>().playerCoord[1] - 2;
        for (int i = x; i < (x + 5); i++)
        {
            for (int j = z; j < (z + 5); j++)
            {
                GameObject sec = Instantiate(section, map.transform);
                for (int k = 0; k < world.transform.childCount; k++)
                {
                    if (world.transform.GetChild(k).gameObject.name == i + "-" + j)
                    {
                        sec.GetComponent<Image>().sprite = world.transform.GetChild(k).GetComponentInChildren<SpriteRenderer>().sprite;
                        if (i == x + 2 && j == z + 2)
                        {
                            GetComponent<MiniMapScript>().ChangeSquare(sec.GetComponent<Image>().sprite);
                            playerDot = Instantiate(GetComponent<MiniMapScript>().playerDot,sec.transform);
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
        panel.SetActive(active);
    }
}
