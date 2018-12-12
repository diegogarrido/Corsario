using UnityEngine.UI;
using UnityEngine;

public class SlotScript : MonoBehaviour
{

    public Item item;
    public Text quantity;
    public Image image;
    public GameObject options;
    public bool transferable;
    public bool inBase;

    private ToolTipScript tScript;
    private TransferScript trScript;
    private GameObject t;
    private InventoryScript inv;

    void Start()
    {
        t = GameObject.FindGameObjectWithTag("ToolTip");
        tScript = t.GetComponent<ToolTipScript>();
        inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        trScript = GameObject.FindGameObjectWithTag("TransferOptions").GetComponent<TransferScript>();
        PointerExit();
        RefreshItem();
    }

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

    }

    public void PointerEnter()
    {
        if (item != null)
        {
            tScript.title.text = item.itemName;
            tScript.AddStat("Valor", "" + item.price);
            tScript.AddStat("Peso", "" + item.weight);
            tScript.AddStat("Rareza", "" + item.rarity);
            if (item.equipable)
            {
                tScript.AddStat("Equipable", "Si");
                if (item.equipableType == "CannonBall")
                {
                    tScript.AddStat("Daño", "" + ((ItemCannonBall)item).cannonBall.GetComponent<CannonBallScript>().cannonBall.damage);
                }
                else if (item.equipableType == "Cannon")
                {
                    tScript.AddStat("Tiempo de recarga", "" + ((ItemCannon)item).cannon.GetComponent<CannonScript>().cannon.coolDown);
                    tScript.AddStat("Tiros", "" + ((ItemCannon)item).cannon.GetComponent<CannonScript>().cannon.shoots);
                    tScript.AddStat("Alcance", "" + ((ItemCannon)item).cannon.GetComponent<CannonScript>().cannon.shootForce);
                }
                else if (item.equipableType == "Sail")
                {
                    tScript.AddStat("Velocidad extra", "" + ((ItemSail)item).extraSpeed);
                }
                tScript.AddStat("RMB", "Equipar");
            }
            else
            {
                tScript.AddStat("Equipable", "No");
            }
            tScript.Show();
        }
    }

    public void PointerExit()
    {
        tScript.ClearStats();
        tScript.Hide();
    }

    public void RefreshItem()
    {
        options.SetActive(false);
        if (item != null)
        {
            image.sprite = item.sprite;
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            image.sprite = null;
            image.color = new Color(0, 0, 0, 0);
            if (quantity != null)
            {
                quantity.text = "";
            }
        }
    }

    private bool MouseIsOver()
    {
        bool mouseInX = Input.mousePosition.x < transform.position.x + 50 && Input.mousePosition.x > transform.position.x - 35;
        bool mouseInY = Input.mousePosition.y < transform.position.y + 50 && Input.mousePosition.y > transform.position.y - 35;
        return (mouseInX && mouseInY);
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
                    options.transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    options.transform.GetChild(0).gameObject.SetActive(item.equipable);
                }
                if (options.transform.childCount > 1)
                {
                    options.transform.GetChild(1).gameObject.SetActive(transferable);
                }
            }
        }
        if (!MouseIsOver())
        {
            options.SetActive(false);
        }
    }
}
