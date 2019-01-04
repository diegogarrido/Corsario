using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour {

    public Item item;
    public Text quantity;
    public Image image;
    public GameObject options;

    protected ToolTipScript tScript;
    protected TransferScript trScript;
    protected InventoryScript inv;

    protected void Start()
    {
        tScript = GameObject.FindGameObjectWithTag("ToolTip").GetComponent<ToolTipScript>();
        inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        trScript = GameObject.FindGameObjectWithTag("TransferOptions").GetComponent<TransferScript>();
        PointerExit();
        RefreshItem();
    }

    protected bool MouseIsOver()
    {
        bool mouseInX = Input.mousePosition.x < transform.position.x + 50 && Input.mousePosition.x > transform.position.x - 35;
        bool mouseInY = Input.mousePosition.y < transform.position.y + 50 && Input.mousePosition.y > transform.position.y - 35;
        return (mouseInX && mouseInY);
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
}
