using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippmentScript : MonoBehaviour
{

    public GameObject cannonBallSlot;
    public GameObject sailSlot;
    public GameObject cannonsSlots;
    public GameObject cannonSlot;

    private InventoryScript inv;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        inv.LoadData();
        int cannonsPerSide = inv.cannonsEquipped.Length;
        for (int i = 0; i < cannonsPerSide; i++)
        {
            Instantiate(cannonSlot, cannonsSlots.transform);
            if (inv.cannonsEquipped != null)
            {
                if (inv.cannonsEquipped[i] != -1)
                {
                    EquipCannon((ItemCannon)inv.items[inv.cannonsEquipped[i]]);
                }
            }
        }
        if (inv.cannonBallEquiped != -1)
        {
            EquipCannonBall((ItemCannonBall)inv.items[inv.cannonBallEquiped]);
        }
        if (inv.sailEquiped != -1)
        {
            EquipSail((ItemSail)inv.items[inv.sailEquiped]);
        }
    }

    public void EquipSail(ItemSail item)
    {
        sailSlot.GetComponent<InventorySlot>().item = item;
        sailSlot.GetComponent<InventorySlot>().RefreshItem();
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>().speed += item.extraSpeed;
    }

    public void UnequipSail()
    {
        inv.sailEquiped = -1;
        inv.AddItem(sailSlot.GetComponent<InventorySlot>().item, 1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>().speed -= ((ItemSail)sailSlot.GetComponent<InventorySlot>().item).extraSpeed;
        sailSlot.GetComponent<InventorySlot>().item = null;
        sailSlot.GetComponent<InventorySlot>().RefreshItem();
    }

    public void EquipCannon(ItemCannon item)
    {
        int slot = HasCannonSlot();
        if(slot != -1)
        {
            if (cannonsSlots.transform.GetChild(slot).GetComponent<InventorySlot>().item == null)
            {
                cannonsSlots.transform.GetChild(slot).GetComponent<InventorySlot>().item = item;
                cannonsSlots.transform.GetChild(slot).GetComponent<InventorySlot>().RefreshItem();
                GameObject.FindGameObjectWithTag("Player").GetComponent<BoatController>().AddCannon(item);
            }
        }
    }

    public void UnEquipCannons()
    {
        foreach (Transform child in cannonsSlots.transform)
        {
            child.gameObject.GetComponent<InventorySlot>().Unequip();
        }
    }

    public void EquipCannonBall(ItemCannonBall item)
    {
        cannonBallSlot.GetComponent<InventorySlot>().item = item;
        cannonBallSlot.GetComponent<InventorySlot>().RefreshItem();
    }

    public void UseCannonBall()
    {
        inv.RemoveItem(inv.items[inv.cannonBallEquiped], 1);
        if (inv.FindItem(inv.items[inv.cannonBallEquiped]) == -1)
        {
            UnequipCannonBall();
        }
    }

    public void UnequipCannonBall()
    {
        inv.cannonBallEquiped = -1;
        cannonBallSlot.GetComponent<InventorySlot>().item = null;
        cannonBallSlot.GetComponent<InventorySlot>().RefreshItem();
        inv.SaveInventory();
    }

    public void UnequipCannon(InventorySlot slot)
    {
        ItemCannon item = (ItemCannon)slot.item;
        slot.item = null;
        slot.RefreshItem();
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoatController>().RemoveCannon(item);
        for (int i = 0; i < inv.cannonsEquipped.Length; i++)
        {
            if (inv.cannonsEquipped[i] == inv.ItemIndex(item))
            {
                inv.cannonsEquipped[i] = -1;
                break;
            }
        }
        inv.AddItem(item, 1);
    }
    
    public int HasCannonSlot()
    {
        for (int i = 0; i < cannonsSlots.transform.childCount; i++)
        {
            if (cannonsSlots.transform.GetChild(i).GetComponent<InventorySlot>().item == null)
            {
                return i;
            }
        }
        return -1;
    }
}
