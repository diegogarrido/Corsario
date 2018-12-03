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
    public float totalWeight;
    public BoatScript player;

    private Item[] playerItems;
    private int[] playerItemsIndexes;
    private int[] playerItemsQuantities;
    public int[] cannonsEquipped;
    public int cannonBallEquiped;
    public int sailEquiped;

    void Awake()
    {
        equipPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        LoadData();
    }

    void Update()
    {
        OpenClose();
    }

    public void LoadData()
    {
        PlayerInventory playerInv = DataManager.LoadInventory();
        if (playerInv != null)
        {
            playerItemsIndexes = playerInv.items;
            playerItemsQuantities = playerInv.quantities;
            cannonsEquipped = playerInv.cannonsEquiped;
            playerItems = new Item[playerItemsIndexes.Length];
            cannonBallEquiped = playerInv.cannonBallEquiped;
            sailEquiped = playerInv.sailEquiped;
            totalWeight = playerInv.totalWeight;
        }
        else
        {
            playerItemsIndexes = new int[0];
            playerItemsQuantities = new int[0];
            cannonsEquipped = new int[GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>().boat.cannonsPerSide];
            for (int i = 0; i < cannonsEquipped.Length; i++)
            {
                cannonsEquipped[i] = -1;
            }
            cannonBallEquiped = -1;
            sailEquiped = -1;
            playerItems = new Item[0];
            totalWeight = 0;
            SaveInventory();
            AddItem(items[3], 60);
            AddItem(items[4], 1);
            AddItem(items[6], 1);
            AddItem(items[5], 1);
            AddItem(items[7], 1);
        }
        RefreshInventory();
    }

    public void Equip(Item item)
    {
        int i = FindItem(item);
        if (i != -1)
        {
            if (playerItems[i].equipable)
            {
                if (playerItems[i].equipableType == "Cannon")
                {
                    GetComponent<EquippmentScript>().EquipCannon((ItemCannon)item);
                    for (int j = 0; j < cannonsEquipped.Length; j++)
                    {
                        if (cannonsEquipped[j] == -1)
                        {
                            cannonsEquipped[j] = ItemIndex(item);
                            break;
                        }
                    }
                    RemoveItem(item, 1);
                }
                else if (playerItems[i].equipableType == "CannonBall")
                {
                    GetComponent<EquippmentScript>().EquipCannonBall((ItemCannonBall)item);
                    cannonBallEquiped = ItemIndex(item);
                    SaveInventory();
                }
                else if (playerItems[i].equipableType == "Sail")
                {
                    GetComponent<EquippmentScript>().EquipSail((ItemSail)item);
                    sailEquiped = ItemIndex(item);
                    RemoveItem(item, 1);
                }
            }
        }
    }

    public float AvailableWeight()
    {
        return player.maxWeight - totalWeight;
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
        if (playerItemsQuantities[itemIndex] <= 0)
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
                else if (i > itemIndex)
                {
                    playerItems[i - 1] = temp[i];
                    playerItemsIndexes[i - 1] = temp2[i];
                    playerItemsQuantities[i - 1] = temp3[i];
                }
            }
        }
        totalWeight -= item.weight * quantity;
        SaveInventory();
        RefreshInventory();
    }

    public int FindItem(Item item)
    {
        int itemIndex = -1;
        if (playerItems != null)
        {
            for (int i = 0; i < playerItems.Length; i++)
            {
                if (playerItems[i].itemName == item.itemName)
                {
                    itemIndex = i;
                }
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
            if (temp != null)
            {
                playerItems = new Item[temp.Length + 1];
                playerItemsIndexes = new int[temp2.Length + 1];
                playerItemsQuantities = new int[temp3.Length + 1];
                for (int i = 0; i < temp.Length; i++)
                {
                    playerItems[i] = temp[i];
                    playerItemsIndexes[i] = temp2[i];
                    playerItemsQuantities[i] = temp3[i];
                }
            }
            else
            {
                playerItems = new Item[1];
                playerItemsIndexes = new int[1];
                playerItemsQuantities = new int[1];
            }
            playerItems[playerItems.Length - 1] = item;
            playerItemsIndexes[playerItemsIndexes.Length - 1] = ItemIndex(item);
            playerItemsQuantities[playerItemsQuantities.Length - 1] = quantity;
        }
        totalWeight += item.weight * quantity;
        SaveInventory();
        RefreshInventory();
    }

    public int ItemIndex(Item item)
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

    public void SaveInventory()
    {
        PlayerInventory inv = new PlayerInventory();
        inv.items = playerItemsIndexes;
        inv.quantities = playerItemsQuantities;
        inv.cannonsEquiped = cannonsEquipped;
        inv.cannonBallEquiped = cannonBallEquiped;
        inv.sailEquiped = sailEquiped;
        inv.totalWeight = totalWeight;
        DataManager.SaveInventory(inv);
    }
}
