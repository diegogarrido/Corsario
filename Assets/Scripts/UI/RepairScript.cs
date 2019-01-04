using UnityEngine.UI;
using UnityEngine;

public class RepairScript : MonoBehaviour {

    public Slider slider;
    public float repairTime;
    public bool repairing;

    private InventoryScript inv;
    private BoatScript player;

	void Start () {
        slider.maxValue = repairTime;
        slider.value = 0;
        repairing = false;
        inv = GetComponent<InventoryScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>();
	}
	
	void Update () {
        if (repairing)
        {
            slider.value += Time.deltaTime;
            if (slider.value >= repairTime)
            {
                repairing = false;
                slider.value = 0;
                float amount = player.boat.health - player.health;
                if(amount > 50)
                {
                    amount = 50;
                }
                int woodUsed = Mathf.RoundToInt(amount / 10);
                inv.RemoveItem(inv.items[0], woodUsed);
                player.health += amount;
            }
        }
        else
        {
            slider.value = 0;
        }
	}
}
