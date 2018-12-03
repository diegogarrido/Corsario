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
        sailSlot.GetComponent<SlotScript>().item = item;
        sailSlot.GetComponent<SlotScript>().RefreshItem();
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>().speed += item.extraSpeed;
    }

    public void UnequipSail()
    {
        inv.sailEquiped = -1;
        inv.AddItem(sailSlot.GetComponent<SlotScript>().item, 1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>().speed -= ((ItemSail)sailSlot.GetComponent<SlotScript>().item).extraSpeed;
        sailSlot.GetComponent<SlotScript>().item = null;
        sailSlot.GetComponent<SlotScript>().RefreshItem();
    }

    public void EquipCannon(ItemCannon item)
    {
        for (int i = 0; i < cannonsSlots.transform.childCount; i++)
        {
            if (cannonsSlots.transform.GetChild(i).GetComponent<SlotScript>().item == null)
            {
                cannonsSlots.transform.GetChild(i).GetComponent<SlotScript>().item = item;
                cannonsSlots.transform.GetChild(i).GetComponent<SlotScript>().RefreshItem();
                GameObject.FindGameObjectWithTag("Player").GetComponent<BoatController>().AddCannon(item);
                break;
            }
        }
    }

    public void EquipCannonBall(ItemCannonBall item)
    {
        cannonBallSlot.GetComponent<SlotScript>().item = item;
        cannonBallSlot.GetComponent<SlotScript>().RefreshItem();
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
        cannonBallSlot.GetComponent<SlotScript>().item = null;
        cannonBallSlot.GetComponent<SlotScript>().RefreshItem();
        inv.SaveInventory();
    }

    public void UnequipCannon(SlotScript slot)
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

    void Update()
    {

    }
}
