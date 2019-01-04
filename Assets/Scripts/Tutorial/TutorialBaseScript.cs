using UnityEngine;

public class TutorialBaseScript : MonoBehaviour
{

    public GameObject baseInventoryPanel;
    public GameObject itemsPanel;
    public int[] baseQuantities;
    public TutorialInventoryScript inv;

    void Start()
    {
        inv = GetComponent<TutorialInventoryScript>();
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
                newSlot.GetComponent<TutorialInventorySlot>().item = inv.items[i];
                newSlot.GetComponent<TutorialInventorySlot>().quantity.text = "" + baseQuantities[i];
                newSlot.GetComponent<TutorialInventorySlot>().inBase = true;
                newSlot.GetComponent<TutorialInventorySlot>().transferable = true;
                newSlot.GetComponent<TutorialInventorySlot>().sellable = false;
            }
        }
    }

    public void AddItem(Item item, int quantity)
    {
        baseQuantities[inv.ItemIndex(item)] += quantity;
        RefreshInventory();
    }

    public void RemoveItem(Item item, int quantity)
    {
        baseQuantities[inv.ItemIndex(item)] -= quantity;
        RefreshInventory();
    }

    public void Active(bool active)
    {
        RefreshInventory();
        baseInventoryPanel.SetActive(active);
    }
}
