using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransferScript : MonoBehaviour
{

    public Text textAmount;
    public Slider sliderAmount;
    public int amount;
    public int max;
    public string target;
    public Item item;

    private InventoryScript inv;
    private BaseScript bas;

    void Start()
    {
        Hide();
        inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        bas = GameObject.FindGameObjectWithTag("Menu").GetComponent<BaseScript>();
    }

    void Update()
    {
        sliderAmount.maxValue = max;
        amount = int.Parse("" + sliderAmount.value);
        textAmount.text = "" + amount;
    }

    public void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        if(max == 1)
        {
            amount = 1;
            Accept();
        }
    }

    public void Hide()
    {
        sliderAmount.value = 0;
        max = 1;
        item = null;
        target = "";
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Accept()
    {
        if (target == "Base")
        {
            bas.AddItem(item, amount);
            inv.RemoveItem(item, amount);
        }
        else if (target == "Player")
        {
            inv.AddItem(item, amount);
            bas.RemoveItem(item, amount);
        }
        else if (target == "DropFromBase")
        {
            bas.RemoveItem(item, amount);
        }
        else if (target == "DropFromPlayer")
        {
            inv.RemoveItem(item, amount);
        }
        else if (target == "SellFromPlayer")
        {
            inv.AddItem(inv.items[1], item.price * amount);
            inv.RemoveItem(item, amount);
            GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>().SetSellable(true);
        }
        else if(target == "SellFromShop")
        {
            if (inv.FindItem(inv.items[1]) != -1 & amount > 0)
            {
                int coins = inv.playerItemsQuantities[inv.FindItem(inv.items[1])];
                if (coins >= item.price * amount)
                {
                    inv.AddItem(item, amount);
                    inv.RemoveItem(inv.items[1], item.price * amount);
                    GameObject.FindGameObjectWithTag("Menu").GetComponent<ShopScript>().shop.Sell(item,amount);
                    GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>().SetSellable(true);
                }
            }
        }
        bool baseActive = GameObject.FindGameObjectWithTag("Menu").GetComponent<BaseScript>().baseInventoryPanel.activeInHierarchy;
        if (baseActive)
        {
            inv.SetTransfereable(true);
        }
        Hide();
    }
}
