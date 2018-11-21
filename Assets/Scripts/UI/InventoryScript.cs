using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    public GameObject inventoryPanel;
    public GameObject itemsPanel;
    public GameObject equipPanel;
    public GameObject slot;
    public Item[] items;

    private Item[] playerItems;
    private int[] playerItemsIndexes;
    private int[] playerItemsQuantities;

    void Start()
    {
        equipPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        if(DataManager.LoadInventory() != null)
        {
            playerItemsIndexes = DataManager.LoadInventory().items;
            playerItemsQuantities = DataManager.LoadInventory().quantities;
            playerItems = new Item[playerItemsIndexes.Length];
        }
        else
        {
            playerItemsIndexes = new int[0];
            playerItemsQuantities = new int[0];
            playerItems = new Item[0];
            SaveInventory();
        }
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        foreach (Transform child in itemsPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < playerItemsIndexes.Length; i++)
        {
            GameObject newSlot = Instantiate(slot, itemsPanel.transform);
            playerItems[i] = items[playerItemsIndexes[i]];
            newSlot.GetComponent<SlotScript>().item = playerItems[i];
            newSlot.GetComponent<SlotScript>().quantity.text = "" + playerItemsQuantities[i];
        }
    }

    public void RemoveItem(Item item, int quantity)
    {
        int itemIndex = FindItem(item);
        bool hasItem = itemIndex != -1;
        if (hasItem && playerItemsQuantities[itemIndex] >= quantity)
        {
            playerItemsQuantities[itemIndex] -= quantity;
        }
        if(playerItemsQuantities[itemIndex] <= 0)
        {
            Item[] temp = playerItems;
            int[] temp2 = playerItemsIndexes;
            int[] temp3 = playerItemsQuantities;
            playerItems = new Item[temp.Length - 1];
            playerItemsIndexes = new int[temp2.Length - 1];
            playerItemsQuantities = new int[temp3.Length - 1];
            for (int i = 0; i < temp.Length; i++)
            {
                if (i < itemIndex)
                {
                    playerItems[i] = temp[i];
                    playerItemsIndexes[i] = temp2[i];
                    playerItemsQuantities[i] = temp3[i];
                }
                else if(i > itemIndex)
                {
                    playerItems[i-1] = temp[i];
                    playerItemsIndexes[i-1] = temp2[i];
                    playerItemsQuantities[i-1] = temp3[i];
                }
            }
        }
        SaveInventory();
        RefreshInventory();
    }

    private int FindItem(Item item)
    {
        int itemIndex = -1;
        for (int i = 0; i < playerItems.Length; i++)
        {
            if (playerItems[i].itemName == item.itemName)
            {
                itemIndex = i;
            }
        }
        return itemIndex;
    }

    public void AddItem(Item item, int quantity)
    {
        int itemIndex = FindItem(item);
        bool hasItem = itemIndex != -1;
        if (hasItem)
        {
            playerItemsQuantities[itemIndex] += quantity;
        }
        else
        {
            Item[] temp = playerItems;
            int[] temp2 = playerItemsIndexes;
            int[] temp3 = playerItemsQuantities;
            playerItems = new Item[temp.Length + 1];
            playerItemsIndexes = new int[temp2.Length + 1];
            playerItemsQuantities = new int[temp3.Length + 1];
            for (int i = 0; i < temp.Length; i++)
            {
                playerItems[i] = temp[i];
                playerItemsIndexes[i] = temp2[i];
                playerItemsQuantities[i] = temp3[i];
            }
            playerItems[playerItems.Length - 1] = item;
            playerItemsIndexes[playerItemsIndexes.Length - 1] = ItemIndex(item);
            playerItemsQuantities[playerItemsQuantities.Length - 1] = quantity;
        }
        SaveInventory();
        RefreshInventory();
    }

    private int ItemIndex(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == item.itemName)
            {
                return i;
            }
        }
        return -1;
    }

    void Update()
    {
        OpenClose();
    }

    private void OpenClose()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        }
    }

    public void SwapMenus()
    {
        itemsPanel.SetActive(!itemsPanel.activeInHierarchy);
        equipPanel.SetActive(!equipPanel.activeInHierarchy);
    }

    private void SaveInventory()
    {
        PlayerInventory inv = new PlayerInventory();
        inv.items = playerItemsIndexes;
        inv.quantities = playerItemsQuantities;
        DataManager.SaveInventory(inv);
    }
}
