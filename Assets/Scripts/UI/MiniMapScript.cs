using UnityEngine.UI;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{

    public WorldGeneration world;
    public GameObject playerDot;
    public GameObject square;

    private int x;
    private int z;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        x = world.playerCoord[0];
        z = world.playerCoord[1];
        float posX = player.transform.position.x - (x * world.spacing);
        float posZ = player.transform.position.z - (z * world.spacing);
        playerDot.transform.localPosition = new Vector3(Calculate(posX, 120), Calculate(posZ, 120), 0);
        GetComponent<MapScript>().playerDot.transform.localPosition = new Vector3(Calculate(posX, 60), Calculate(posZ, 60), 0);
    }

    private float Calculate(float pos, float width)
    {
        return (pos * width / 1000) - width / 2;
    }

    public void ChangeSquare(Sprite sprite)
    {
        square.GetComponent<Image>().sprite = sprite;
    }
}
