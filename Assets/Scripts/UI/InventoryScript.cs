using UnityEngine.UI;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    public Text weightText;
    public GameObject resourceCrate;
    public GameObject inventoryPanel;
    public GameObject itemsPanel;
    public GameObject equipPanel;
    public GameObject slot;
    public Item[] items;
    public float totalWeight;
    public BoatScript player;
    public int[] playerItemsIndexes;
    public int[] playerItemsQuantities;
    public int[] cannonsEquipped;
    public int cannonBallEquiped;
    public int sailEquiped;
    public int coinsInAltar;
    public bool gameFinished;

    private Item[] playerItems;
    private WorldGeneration world;

    void Awake()
    {
        equipPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        LoadData();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>();
        world = GameObject.FindGameObjectWithTag("World").GetComponent<WorldGeneration>();
    }

    private void Update()
    {
        weightText.text = "" + totalWeight + "/" + player.maxWeight;
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
            GetComponent<BaseScript>().baseQuantities = playerInv.baseQuantities;
            coinsInAltar = playerInv.coinsInAltar;
            gameFinished = playerInv.gameFinished;
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
            GetComponent<BaseScript>().baseQuantities = new int[items.Length];
            for (int i = 0; i < GetComponent<BaseScript>().baseQuantities.Length; i++)
            {
                GetComponent<BaseScript>().baseQuantities[i] = 0;
            }
            coinsInAltar = 0;
            gameFinished = false;
            SaveInventory();
            AddItem(items[3], 100);
            AddItem(items[4], 1);
            AddItem(items[6], 1);
            AddItem(items[5], 1);
            AddItem(items[7], 1);
            GetComponent<BaseScript>().AddItem(items[0], 10);
            GetComponent<BaseScript>().AddItem(items[3], 100);
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
                if (playerItems[i].equipableType == "Cannon" && GetComponent<EquippmentScript>().HasCannonSlot() != -1)
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
                    RefreshInventory();
                    SaveInventory();
                }
                else if (playerItems[i].equipableType == "Sail")
                {
                    if(sailEquiped == -1)
                    {
                        GetComponent<EquippmentScript>().EquipSail((ItemSail)item);
                        sailEquiped = ItemIndex(item);
                        RemoveItem(item, 1);
                    }
                }
            }
        }
    }

    public float AvailableWeight()
    {
        return player.maxWeight - totalWeight;
    }

    public void SetTransfereable(bool transferable)
    {
        foreach(Transform child in itemsPanel.transform)
        {
            child.GetComponent<InventorySlot>().transferable = transferable;
        }
    }

    public void SetSellable(bool sellable)
    {
        foreach (Transform child in itemsPanel.transform)
        {
            child.GetComponent<InventorySlot>().sellable = sellable;
        }
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
            newSlot.GetComponent<InventorySlot>().item = playerItems[i];
            newSlot.GetComponent<InventorySlot>().quantity.text = "" + playerItemsQuantities[i];
            newSlot.GetComponent<InventorySlot>().inBase = false;
            newSlot.GetComponent<InventorySlot>().transferable = false;
            newSlot.GetComponent<InventorySlot>().sellable = false;
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
        if(world != null)
        {
            world.SaveData();
        }
        else
        {
            world = GameObject.FindGameObjectWithTag("World").GetComponent<WorldGeneration>();
        }
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
        if (world != null)
        {
            world.SaveData();
        }
        else
        {
            world = GameObject.FindGameObjectWithTag("World").GetComponent<WorldGeneration>();
        }
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

    public void Active(bool active)
    {
        inventoryPanel.SetActive(active);
    }

    public void SwapMenus()
    {
        itemsPanel.SetActive(!itemsPanel.activeInHierarchy);
        equipPanel.SetActive(!equipPanel.activeInHierarchy);
    }

    public void Die()
    {
        int amount = Random.Range(1, 3);
        for(int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, playerItemsIndexes.Length);
            int quant = Random.Range(0, playerItemsQuantities[index]);
            RemoveItem(items[playerItemsIndexes[index]],quant);
            Instantiate(resourceCrate).transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(Random.Range(0.5f,3),0,Random.Range(0.5f, 3));
        }
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
        inv.baseQuantities = GetComponent<BaseScript>().baseQuantities;
        inv.coinsInAltar = coinsInAltar;
        inv.gameFinished = gameFinished;
        DataManager.SaveInventory(inv);
    }
}
