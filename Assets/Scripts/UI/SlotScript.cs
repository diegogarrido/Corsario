using UnityEngine.UI;
using UnityEngine;

public class SlotScript : MonoBehaviour
{

    public Item item;
    public Text itemName;
    public Text quantity;
    public Image image;
    public GameObject options;

    void Start()
    {
        options.SetActive(false);
        itemName.text = item.itemName;
        image.sprite = item.sprite;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && MouseIsOver() && item.equipable)
        {
            options.SetActive(true);
        }

        if (!MouseIsOver())
        {
            options.SetActive(false);
        }
    }

    private bool MouseIsOver()
    {
        bool mouseInX = Input.mousePosition.x < transform.position.x + 50 && Input.mousePosition.x > transform.position.x - 50;
        bool mouseInY = Input.mousePosition.y < transform.position.y + 50 && Input.mousePosition.y > transform.position.y - 50;
        return (mouseInX && mouseInY);
    }

    public void ShowOptions()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("E");
        }
    }
}
