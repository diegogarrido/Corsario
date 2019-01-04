using UnityEngine;
using UnityEngine.UI;

public class BuildingScript : MonoBehaviour {

    public Text woodText;
    public Text rockText;
    public int wood;
    public int rock;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update () {
        if (player != null)
        {
            Vector3 playerPosition = new Vector3(player.transform.position.x, 30, player.transform.position.z);
            transform.LookAt(playerPosition);
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        woodText.text = wood + "/" + 450;
        rockText.text = rock + "/" + 200;
    }
}
