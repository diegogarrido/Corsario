using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public int[] shopItems;
    public int[] quantities;
    private InventoryScript inv;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        RollItems();
    }

    public void Sell(Item item, int amount)
    {
        int index = -1;
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (inv.items[shopItems[i]].itemName == item.itemName)
            {
                index = i;
                break;
            }
        }
        if (index != -1)
        {
            quantities[index] -= amount;
            if (quantities[index] <= 0)
            {
                int[] items = shopItems;
                int[] quan = quantities;
                shopItems = new int[items.Length - 1];
                quantities = new int[quan.Length - 1];
                for (int i = 0; i < items.Length; i++)
                {
                    if (i != index)
                    {
                        if (i < index)
                        {
                            shopItems[i] = items[i];
                            quantities[i] = quan[i];
                        }
                        else
                        {
                            shopItems[i - 1] = items[i];
                            quantities[i - 1] = quan[i];
                        }
                    }
                }
            }
        }
        GameObject.FindGameObjectWithTag("Menu").GetComponent<ShopScript>().RefreshInventory();
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
                int index;
                do
                {
                    index = Random.Range(0, inv.items.Length);
                } while (HasItem(index) != -1);
                shopItems[i] = index;
                if (Random.Range(0, 100) >= (inv.items[shopItems[i]].rarity * 10) - 10 && shopItems[i] != 1)
                {
                    int quantity = Random.Range(1, 200 / (inv.items[shopItems[i]].rarity * 10));
                    quantities[i] = quantity;
                    break;
                }
            }
        }
    }

    public int HasItem(int item)
    {
        int index = -1;
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i] == item)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    public int HasItem(Item item)
    {
        int it = inv.ItemIndex(item);
        int index = -1;
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i] == it)
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
