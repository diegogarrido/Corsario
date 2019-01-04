using UnityEngine;

public class InventorySlot : Slot
{
    public GameObject transferButton;
    public GameObject sellButton;
    public GameObject equipButton;
    public bool transferable;
    public bool sellable;
    public bool inBase;

    void Update()
    {
        ShowOptions();
    }

    public void Transfer()
    {
        if (inBase)
        {
            trScript.max = GameObject.FindGameObjectWithTag("Menu").GetComponent<BaseScript>().baseQuantities[inv.ItemIndex(item)];
            trScript.target = "Player";
        }
        else
        {
            trScript.max = inv.playerItemsQuantities[inv.FindItem(item)];
            trScript.target = "Base";
        }
        trScript.item = item;
        trScript.Show();
    }

    public void Drop()
    {
        if (inBase)
        {
            trScript.max = GameObject.FindGameObjectWithTag("Menu").GetComponent<BaseScript>().baseQuantities[inv.ItemIndex(item)];
            trScript.target = "DropFromBase";
        }
        else
        {
            trScript.max = inv.playerItemsQuantities[inv.FindItem(item)];
            trScript.target = "DropFromPlayer";
        }
        trScript.item = item;
        trScript.Show();
    }

    public void Sell()
    {
        trScript.item = item;
        trScript.max = inv.playerItemsQuantities[inv.FindItem(item)];
        trScript.target = "SellFromPlayer";
        trScript.Show();
    }

    public void Equip()
    {
        GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>().Equip(item);
    }

    public void Unequip()
    {
        if (item.equipableType == "Cannon")
        {
            GameObject.FindGameObjectWithTag("Menu").GetComponent<EquippmentScript>().UnequipCannon(this);
        }
        else if (item.equipableType == "CannonBall")
        {
            GameObject.FindGameObjectWithTag("Menu").GetComponent<EquippmentScript>().UnequipCannonBall();
        }
        else if (item.equipableType == "Sail")
        {
            GameObject.FindGameObjectWithTag("Menu").GetComponent<EquippmentScript>().UnequipSail();
        }
    }

    public void ShowOptions()
    {
        if (item != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && MouseIsOver())
            {
                options.SetActive(true);
                if (inBase)
                {
                    equipButton.SetActive(false);
                    sellButton.SetActive(false);
                }
                else
                {
                    if(equipButton != null)
                    {
                        equipButton.SetActive(item.equipable);
                    }
                    if(transferButton != null)
                    {
                        transferButton.SetActive(transferable);
                    }
                    if(sellButton != null)
                    {
                        sellButton.SetActive(sellable);
                    }
                }
            }
        }
        if (!MouseIsOver())
        {
            options.SetActive(false);
        }
    }
}
