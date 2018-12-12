using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{

    public GameObject shopPanel;
    public GameObject itemsPanel;
    public GameObject boatsPanel;
    public GameObject optionsPanel;
    public GameObject backButton;
    public GameObject[] boats;
    public GameObject boatSlot;
    public int[] shopItems;
    public int[] quantities;

    private InventoryScript inv;

    void Start()
    {
        inv = GetComponent<InventoryScript>();
        backButton.SetActive(false);
    }

    public void RefreshInventory()
    {
        foreach (Transform child in itemsPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < shopItems.Length; i++)
        {
            GameObject newSlot = Instantiate(inv.slot, itemsPanel.transform);
            newSlot.GetComponent<SlotScript>().item = inv.items[shopItems[i]];
            newSlot.GetComponent<SlotScript>().quantity.text = "" + quantities[i];
            newSlot.GetComponent<SlotScript>().inBase = false;
            newSlot.GetComponent<SlotScript>().transferable = false;
        }
    }

    public void Back()
    {
        boatsPanel.SetActive(false);
        itemsPanel.SetActive(false);
        backButton.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ShowItems()
    {
        optionsPanel.SetActive(false);
        backButton.SetActive(true);
        itemsPanel.SetActive(true);
    }

    public void ShowBoats()
    {
        optionsPanel.SetActive(false);
        backButton.SetActive(true);
        boatsPanel.SetActive(true);
    }

    public void Active(bool active)
    {
        shopPanel.SetActive(active);
    }

    public void RollItems()
    {
        int ammount = Random.Range(3, 5);
        shopItems = new int[ammount];
        quantities = new int[ammount];
        for (int i = 0; i < ammount; i++)
        {
            while (true)
            {
                shopItems[i] = Random.Range(0, inv.items.Length);
                if (Random.Range(0, 100) >= (inv.items[shopItems[i]].rarity * 10) - 10)
                {
                    int quantity = Random.Range(1, 200 / (inv.items[shopItems[i]].rarity * 10));
                    quantities[i] = quantity;
                    break;
                }
            }
        }
    }
}
