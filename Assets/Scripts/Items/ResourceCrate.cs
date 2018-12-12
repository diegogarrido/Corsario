using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCrate : MonoBehaviour
{

    public Item item;
    public int quantity;

    private InventoryScript inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        RollItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(inventory.AvailableWeight() > item.weight)
            {
                inventory.AddItem(item, quantity);
                Destroy(gameObject);
            }
        }
    }

    public void RollItem()
    {
        item = inventory.items[Random.Range(0, inventory.items.Length)];
        if (Random.Range(0, 100) >= (item.rarity * 10) - 10)
        {
            quantity = Random.Range(1, 200 / (item.rarity * 10));
        }
        else
        {
            RollItem();
        }
    }
}
