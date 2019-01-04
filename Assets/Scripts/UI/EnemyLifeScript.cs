using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeScript : MonoBehaviour {

    public BoatScript enemy;
    public Slider healthBar;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthBar.maxValue = enemy.health;
    }

    void Update () {
        healthBar.value = enemy.health;
        if(player != null)
        {
            Vector3 playerPosition = new Vector3(player.transform.position.x, 30, player.transform.position.z);
            transform.LookAt(playerPosition);
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
	}
}
