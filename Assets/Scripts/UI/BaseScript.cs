using UnityEngine;

public class BaseScript : MonoBehaviour
{

    public GameObject baseInventoryPanel;
    public GameObject itemsPanel;
    public int[] baseQuantities;
    public InventoryScript inv;

    void Start()
    {
        inv.LoadData();
        baseInventoryPanel.SetActive(false);
    }

    public void RefreshInventory()
    {
        foreach (Transform child in itemsPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < baseQuantities.Length; i++)
        {
            if (baseQuantities[i] > 0)
            {
                GameObject newSlot = Instantiate(inv.slot, itemsPanel.transform);
                newSlot.GetComponent<SlotScript>().item = inv.items[i];
                newSlot.GetComponent<SlotScript>().quantity.text = "" + baseQuantities[i];
                newSlot.GetComponent<SlotScript>().inBase = true;
                newSlot.GetComponent<SlotScript>().transferable = true;
            }
        }
    }

    public void AddItem(Item item, int quantity)
    {
        baseQuantities[inv.ItemIndex(item)] += quantity;
        inv.SaveInventory();
        RefreshInventory();
    }

    public void RemoveItem(Item item, int quantity)
    {
        baseQuantities[inv.ItemIndex(item)] -= quantity;
        inv.SaveInventory();
        RefreshInventory();
    }

    public void Active(bool active)
    {
        RefreshInventory();
        baseInventoryPanel.SetActive(active);
    }
}
