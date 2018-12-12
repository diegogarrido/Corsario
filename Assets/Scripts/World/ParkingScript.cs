using UnityEngine.SceneManagement;
using UnityEngine;

public class ParkingScript : MonoBehaviour
{

    public GameObject interactionMenu;

    void Start()
    {
        interactionMenu.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactionMenu.SetActive(true);
            GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>().SetTransfereable(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactionMenu.SetActive(false);
            GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>().SetTransfereable(false);
            GameObject.FindGameObjectWithTag("Menu").GetComponent<BaseScript>().baseInventoryPanel.SetActive(false);
        }
    }

    public void Interact(string type)
    {
        if (type.Contains("small"))
        {
            PlayerPrefs.SetString("Load", "Acertijos");
            SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
            GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().HideMainUI();
        }
        else if (type.Contains("big"))
        {
            GameObject menu = GameObject.FindGameObjectWithTag("Menu");
            MenuController menuController = menu.GetComponent<MenuController>();
            SquareData data = DataManager.LoadSquareData(GetComponentInParent<BigSpawns>().x, GetComponentInParent<BigSpawns>().z);
            if (data.content == "HouseFinished")
            {
                if (menuController.gamePaused)
                {
                    menu.GetComponent<BaseScript>().Active(false);
                    menu.GetComponent<InventoryScript>().Active(false);
                    menuController.gamePaused = false;
                }
                else
                {
                    menu.GetComponent<BaseScript>().Active(true);
                    menu.GetComponent<InventoryScript>().Active(true);
                    menuController.gamePaused = true;
                    menuController.lastPressed = KeyCode.I;
                }
            }else if (data.content == "Shop")
            {
                if (menuController.gamePaused)
                {
                    menu.GetComponent<ShopScript>().Active(false);
                    menu.GetComponent<InventoryScript>().Active(false);
                    menuController.gamePaused = false;
                }
                else
                {
                    menu.GetComponent<ShopScript>().Active(true);
                    menu.GetComponent<InventoryScript>().Active(true);
                    menuController.gamePaused = true;
                    menuController.lastPressed = KeyCode.I;
                }
            }
        }
    }
}
