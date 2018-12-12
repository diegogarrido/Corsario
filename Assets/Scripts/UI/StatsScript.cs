using UnityEngine.UI;
using UnityEngine;

public class StatsScript : MonoBehaviour {

    public Text health;
    public Text speed;
    public Text weight;
    public Text turnSpeed;
    private BoatScript player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>();
    }

    void Update () {
        health.text = "" + player.health;
        speed.text = "" + player.speed;
        weight.text = "" + player.maxWeight;
        turnSpeed.text = "" + player.turnSpeed;
	}
}
