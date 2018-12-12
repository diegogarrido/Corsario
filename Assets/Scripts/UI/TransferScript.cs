using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransferScript : MonoBehaviour
{

    public Text textAmmount;
    public Slider sliderAmmount;
    public int ammount;
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
        sliderAmmount.maxValue = max;
        ammount = int.Parse("" + sliderAmmount.value);
        textAmmount.text = "" + ammount;
    }

    public void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Accept()
    {
        if (target == "Base")
        {
            bas.AddItem(item, ammount);
            inv.RemoveItem(item, ammount);
        }
        else if (target == "Player")
        {
            inv.AddItem(item, ammount);
            bas.RemoveItem(item, ammount);
        }
        else if (target == "DropFromBase")
        {
            bas.RemoveItem(item, ammount);
        }
        else if (target == "DropFromPlayer")
        {
            inv.RemoveItem(item, ammount);
        }
        bool baseActive = GameObject.FindGameObjectWithTag("Menu").GetComponent<BaseScript>().baseInventoryPanel.activeInHierarchy;
        if (baseActive)
        {
            inv.SetTransfereable(true);
        }
        sliderAmmount.value = 0;
        max = 1;
        item = null;
        target = "";
        Hide();
    }
}
