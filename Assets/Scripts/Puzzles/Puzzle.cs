using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

	public bool completed;
    public bool failed;

    public void Win(float difficulty)
    {
        InventoryScript inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        Item i = RollItem();
        int amount = Random.Range(1, 200 / (i.rarity * 10));
        amount = Mathf.RoundToInt(difficulty * amount);
        inv.AddItem(i, int.Parse("" + amount));
        completed = true;
    }

    public Item RollItem()
    {
        InventoryScript inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        Item item = inv.items[Random.Range(0, inv.items.Length)];
        if (Random.Range(0, 100) >= (item.rarity * 10) - 10)
        {
            return item;
        }
        else
        {
            return RollItem();
        }
    }

    public void Lose()
    {
        failed = true;
    }
}
