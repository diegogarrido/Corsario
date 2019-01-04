using UnityEngine;

public class ShopSlot : Slot {

    void Update()
    {
        ShowOptions();
    }

    public void Buy()
    {
        int coins = inv.playerItemsQuantities[inv.FindItem(inv.items[1])];
        int quantity = GameObject.FindGameObjectWithTag("Menu").GetComponent<ShopScript>().shop.quantities[trScript.max = GameObject.FindGameObjectWithTag("Menu").GetComponent<ShopScript>().shop.HasItem(item)];
        if (quantity * item.price > coins)
        {
            quantity = ((int)(coins / item.price));
        }
        if (quantity > 0)
        {
            trScript.max = quantity;
            trScript.item = item;
            trScript.target = "SellFromShop";
            trScript.Show();
        }
    }

    public void ShowOptions()
    {
        if (item != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && MouseIsOver())
            {
                options.SetActive(true);
            }
        }
        if (!MouseIsOver())
        {
            options.SetActive(false);
        }
    }
}
