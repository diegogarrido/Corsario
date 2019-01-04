using UnityEngine.UI;
using UnityEngine;

public class TutorialMiniMapScript : MonoBehaviour
{

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
        if (player != null)
        {
            x = 0;
            z = 0;
            float posX = player.transform.position.x - (x * 1000);
            float posZ = player.transform.position.z - (z * 1000);
            playerDot.transform.localPosition = new Vector3(Calculate(posX, 120), Calculate(posZ, 120), 0);
            GetComponent<TutorialMapScript>().playerDot.transform.localPosition = new Vector3(Calculate(posX, 60), Calculate(posZ, 60), 0);
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
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
