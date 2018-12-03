using UnityEngine.UI;
using UnityEngine;

public class SlotScript : MonoBehaviour
{

    public Item item;
    public Text quantity;
    public Image image;
    public GameObject options;

    private ToolTipScript tScript;
    private GameObject t;

    private void Awake()
    {
        t = GameObject.FindGameObjectWithTag("ToolTip");
        tScript = t.GetComponent<ToolTipScript>();
    }

    void Start()
    {
        PointerExit();
        RefreshItem();
    }

    void Update()
    {
        ShowOptions();
    }

    public void PointerEnter()
    {
        if(item != null)
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
            foreach (Transform child in t.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void PointerExit()
    {
        foreach (Transform child in t.transform.GetChild(1).transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in t.transform)
        {
           child.gameObject.SetActive(false);
        }
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
            if (Input.GetKeyDown(KeyCode.Mouse1) && MouseIsOver() && item.equipable)
            {
                options.SetActive(true);
            }
        }
        if (!MouseIsOver())
        {
            options.SetActive(false);
        }
    }
}
