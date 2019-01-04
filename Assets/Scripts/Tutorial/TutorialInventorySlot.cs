using UnityEngine;

public class TutorialInventorySlot : Slot
{
    public GameObject transferButton;
    public GameObject sellButton;
    public GameObject equipButton;
    public bool transferable;
    public bool sellable;
    public bool inBase;

    private TutorialInventoryScript inve;

    void Update()
    {
        ShowOptions();
    }

    public void Transfer()
    {
        if(inve == null)
        {
            inve = GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialInventoryScript>();
        }
        if (inBase)
        {
            trScript.max = GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialBaseScript>().baseQuantities[inve.ItemIndex(item)];
            trScript.target = "Player";
        }
        else
        {
            trScript.max = inve.playerItemsQuantities[inve.FindItem(item)];
            trScript.target = "Base";
        }
        trScript.item = item;
        trScript.Show();
    }

    public void Drop()
    {
        if (inve == null)
        {
            inve = GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialInventoryScript>();
        }
        if (inBase)
        {
            trScript.max = GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialBaseScript>().baseQuantities[inve.ItemIndex(item)];
            trScript.target = "DropFromBase";
        }
        else
        {
            trScript.max = inve.playerItemsQuantities[inve.FindItem(item)];
            trScript.target = "DropFromPlayer";
        }
        trScript.item = item;
        trScript.Show();
    }

    public void Sell()
    {
        if (inve == null)
        {
            inve = GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialInventoryScript>();
        }
        trScript.item = item;
        trScript.max = inv.playerItemsQuantities[inve.FindItem(item)];
        trScript.target = "SellFromPlayer";
        trScript.Show();
    }

    public void Equip()
    {
        GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialInventoryScript>().Equip(item);
    }

    public void Unequip()
    {
        if (item.equipableType == "Cannon")
        {
            GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialEquippmentScript>().UnequipCannon(this);
        }
        else if (item.equipableType == "CannonBall")
        {
            GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialEquippmentScript>().UnequipCannonBall();
        }
        else if (item.equipableType == "Sail")
        {
            GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialEquippmentScript>().UnequipSail();
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
                    if (equipButton != null)
                    {
                        equipButton.SetActive(item.equipable);
                    }
                    if (transferButton != null)
                    {
                        transferButton.SetActive(transferable);
                    }
                    if (sellButton != null)
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
