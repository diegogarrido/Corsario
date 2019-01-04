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
    public GameObject boatSlot;
    public GameObject itemsScroll;
    public GameObject boatsScroll;
    public GameObject shopSlot;
    public Shop shop;

    private InventoryScript inv;
    private GameObject[] boats;

    void Start()
    {
        inv = GetComponent<InventoryScript>();
        boats = GameObject.FindGameObjectWithTag("World").GetComponent<PlayerSpawner>().boats;
        backButton.SetActive(false);
        RefreshBoats();
    }

    public void RefreshInventory()
    {
        if (shop != null)
        {
            foreach (Transform child in itemsScroll.transform)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < shop.shopItems.Length; i++)
            {
                GameObject newSlot = Instantiate(shopSlot, itemsScroll.transform);
                newSlot.GetComponent<ShopSlot>().item = inv.items[shop.shopItems[i]];
                newSlot.GetComponent<ShopSlot>().quantity.text = "" + shop.quantities[i];
                newSlot.GetComponent<ShopSlot>().RefreshItem();
            }
        }
    }

    public void RefreshBoats()
    {
        foreach (Transform child in boatsScroll.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < boats.Length; i++)
        {
            if(boats[i].GetComponent<BoatScript>().boat.boatName != boats[GameObject.FindGameObjectWithTag("World").GetComponent<PlayerSpawner>().boat].GetComponent<BoatScript>().boat.boatName)
            {
                GameObject b = Instantiate(boatSlot, boatsScroll.transform);
                b.GetComponent<BoatSlotScript>().boat = boats[i].GetComponent<BoatScript>().boat;
                b.GetComponent<BoatSlotScript>().playerValue = boats[GameObject.FindGameObjectWithTag("World").GetComponent<PlayerSpawner>().boat].GetComponent<BoatScript>().boat.price;
                b.GetComponent<BoatSlotScript>().ShowBoat();
            }
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
        inv.SetSellable(active);
    }

}
