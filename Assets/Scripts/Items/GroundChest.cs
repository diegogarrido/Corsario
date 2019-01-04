using UnityEngine;

public class GroundChest : MonoBehaviour {

    public bool activable;

    private SmallSpawns spawns;

	void Start () {
        spawns = GetComponentInParent<SmallSpawns>();
        activable = spawns.data.content.Split('-')[1] == "Closed";
	}
	
	void Update () {
        if (GetComponent<Treasure>().open && GetComponent<Treasure>().active && activable)
        {
            SquareData data = spawns.data;
            data.content = "Treasure-Opened-" + data.content.Split('-')[2];
            DataManager.SaveSquareData(data, spawns.x, spawns.z);
            activable = false;
        }
    }
}
