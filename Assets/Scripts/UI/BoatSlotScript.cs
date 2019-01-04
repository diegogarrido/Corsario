using UnityEngine.UI;
using UnityEngine;

public class BoatSlotScript : MonoBehaviour
{

    public Boat boat;
    public Image image;
    public Text price;
    public int playerValue;

    private ToolTipScript toolTipScript;

    void Start()
    {
        toolTipScript = GameObject.FindGameObjectWithTag("ToolTip").GetComponent<ToolTipScript>();
    }

    public void ShowBoat()
    {
        if (boat != null)
        {
            image.sprite = boat.sprite;
            price.text =""+ (boat.price - playerValue);
        }
    }

    public void Buy()
    {
        InventoryScript inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        int coins = inv.playerItemsQuantities[inv.FindItem(inv.items[1])];
        if(coins > int.Parse(price.text))
        {
            GameObject.FindGameObjectWithTag("Menu").GetComponent<EquippmentScript>().UnEquipCannons();
            GameObject.FindGameObjectWithTag("Menu").GetComponent<EquippmentScript>().UnequipSail();
            GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().gamePaused = false;
            int[] cannonsEquipped = new int[boat.cannonsPerSide];
            for (int i = 0; i < cannonsEquipped.Length; i++)
            {
                cannonsEquipped[i] = -1;
            }
            inv.cannonsEquipped = cannonsEquipped;
            inv.RemoveItem(GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>().items[1], int.Parse(price.text));
            GameObject.FindGameObjectWithTag("World").GetComponent<PlayerSpawner>().ChangeBoat(boat);
        }
    }

    public void PointerEnter()
    {
        if (boat != null)
        {
            toolTipScript.title.text = boat.boatName;
            toolTipScript.AddStat("Valor", "" + boat.price);
            toolTipScript.AddStat("Capacidad", "" + boat.capacity);
            toolTipScript.AddStat("Cañones", "" + (boat.cannonsPerSide * 2));
            toolTipScript.AddStat("Puntos de vida", "" + boat.health);
            toolTipScript.AddStat("Velocidad", "" + boat.speed);
            toolTipScript.AddStat("Velocidad de giro", "" + boat.turnSpeed);
            toolTipScript.AddStat("Fuerza de embestida", "" + boat.rammingDamage);
            toolTipScript.Show();
        }
    }

    public void PointerExit()
    {
        toolTipScript.ClearStats();
        toolTipScript.Hide();
    }
}
