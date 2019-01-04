using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEquippmentScript : MonoBehaviour
{

    public GameObject cannonBallSlot;
    public GameObject sailSlot;
    public GameObject cannonsSlots;
    public GameObject cannonSlot;

    private TutorialInventoryScript inv;

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialInventoryScript>();
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
        sailSlot.GetComponent<TutorialInventorySlot>().item = item;
        sailSlot.GetComponent<TutorialInventorySlot>().RefreshItem();
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>().speed += item.extraSpeed;
    }

    public void UnequipSail()
    {
        inv.sailEquiped = -1;
        inv.AddItem(sailSlot.GetComponent<TutorialInventorySlot>().item, 1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoatScript>().speed -= ((ItemSail)sailSlot.GetComponent<TutorialInventorySlot>().item).extraSpeed;
        sailSlot.GetComponent<TutorialInventorySlot>().item = null;
        sailSlot.GetComponent<TutorialInventorySlot>().RefreshItem();
    }

    public void EquipCannon(ItemCannon item)
    {
        int slot = HasCannonSlot();
        if (slot != -1)
        {
            if (cannonsSlots.transform.GetChild(slot).GetComponent<TutorialInventorySlot>().item == null)
            {
                cannonsSlots.transform.GetChild(slot).GetComponent<TutorialInventorySlot>().item = item;
                cannonsSlots.transform.GetChild(slot).GetComponent<TutorialInventorySlot>().RefreshItem();
                GameObject.FindGameObjectWithTag("Player").GetComponent<TutorialBoatController>().AddCannon(item);
            }
        }
    }

    public void UnEquipCannons()
    {
        foreach (Transform child in cannonsSlots.transform)
        {
            child.gameObject.GetComponent<TutorialInventorySlot>().Unequip();
        }
    }

    public void EquipCannonBall(ItemCannonBall item)
    {
        cannonBallSlot.GetComponent<TutorialInventorySlot>().item = item;
        cannonBallSlot.GetComponent<TutorialInventorySlot>().RefreshItem();
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
        cannonBallSlot.GetComponent<TutorialInventorySlot>().item = null;
        cannonBallSlot.GetComponent<TutorialInventorySlot>().RefreshItem();
    }

    public void UnequipCannon(TutorialInventorySlot slot)
    {
        ItemCannon item = (ItemCannon)slot.item;
        slot.item = null;
        slot.RefreshItem();
        GameObject.FindGameObjectWithTag("Player").GetComponent<TutorialBoatController>().RemoveCannon(item);
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
            if (cannonsSlots.transform.GetChild(i).GetComponent<TutorialInventorySlot>().item == null)
            {
                return i;
            }
        }
        return -1;
    }
}
