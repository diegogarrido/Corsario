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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactionMenu.SetActive(false);
            GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>().SetTransfereable(false);
            GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>().SetSellable(false);
            GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().CloseAll();
        }
    }

    public void Interact(string type)
    {
        GameObject menu = GameObject.FindGameObjectWithTag("Menu");
        InventoryScript inv = menu.GetComponent<InventoryScript>();
        if (type.Contains("small"))
        {
            if (transform.parent.parent.gameObject.GetComponentInChildren<GroundChest>() != null)
            {
                if (transform.parent.parent.gameObject.GetComponentInChildren<GroundChest>().activable)
                {
                    transform.parent.parent.gameObject.GetComponentInChildren<Treasure>().active = true;
                    transform.parent.parent.gameObject.GetComponentInChildren<Treasure>().ChoosePuzzle();
                }
            }
            else if (transform.parent.parent.gameObject.GetComponentInChildren<ResourceCrate>() != null)
            {
                ResourceCrate[] r = transform.parent.parent.gameObject.GetComponentsInChildren<ResourceCrate>();
                for (int i = 0; i < r.Length; i++)
                {
                    inv.AddItem(r[i].item, r[i].quantity);
                    Destroy(r[i].gameObject);
                    SquareData data = new SquareData();
                    data.content = "Empty";
                    DataManager.SaveSquareData(data, GetComponentInParent<SmallSpawns>().x, GetComponentInParent<SmallSpawns>().z);
                }
            }
        }
        else if (type.Contains("big"))
        {
            MenuController menuController = menu.GetComponent<MenuController>();
            SquareData data = GetComponentInParent<BigSpawns>().data;
            if (data.content == "HouseFinished")
            {
                if (menuController.gamePaused)
                {
                    GameObject.FindGameObjectWithTag("TransferOptions").GetComponent<TransferScript>().Hide();
                    inv.SetTransfereable(false);
                    menu.GetComponent<BaseScript>().Active(false);
                    inv.Active(false);
                    menuController.gamePaused = false;
                    GameObject.FindGameObjectWithTag("TransferOptions").GetComponent<TransferScript>().Hide();
                }
                else
                {
                    inv.SetTransfereable(true);
                    menu.GetComponent<BaseScript>().Active(true);
                    inv.Active(true);
                    menuController.gamePaused = true;
                    menuController.lastPressed = KeyCode.I;
                }
            }
            else if (data.content.Contains("House1"))
            {
                int woodIndex = inv.FindItem(inv.items[0]);
                if (woodIndex != -1)
                {
                    int wood = int.Parse(data.content.Split('-')[1]);
                    wood += inv.playerItemsQuantities[woodIndex];
                    if (wood > 450)
                    {
                        inv.RemoveItem(inv.items[0], inv.playerItemsQuantities[woodIndex] - (wood - 450));
                    }
                    else
                    {
                        inv.RemoveItem(inv.items[0], inv.playerItemsQuantities[woodIndex]);
                    }
                    if (wood >= 450)
                    {
                        data.content = "HouseFinished";
                        Destroy(GetComponentInParent<BigSpawns>().buildingSpawn.transform.GetChild(0));
                        Instantiate(GetComponentInParent<BigSpawns>().playerHouseFinished, GetComponentInParent<BigSpawns>().buildingSpawn.transform);
                    }
                    else if (wood > 200)
                    {
                        data.content = "House2-" + wood + "-" + data.content.Split('-')[2];
                        Destroy(GetComponentInParent<BigSpawns>().buildingSpawn.transform.GetChild(0));
                        GameObject h = Instantiate(GetComponentInParent<BigSpawns>().playerHouse2, GetComponentInParent<BigSpawns>().buildingSpawn.transform);
                        h.GetComponentInChildren<BuildingScript>().wood = wood;
                        h.GetComponentInChildren<BuildingScript>().rock = int.Parse(data.content.Split('-')[2]);
                        GetComponentInParent<BigSpawns>().buildingSpawn.GetComponentInChildren<BuildingScript>().wood = wood;
                    }
                    else
                    {
                        data.content = data.content.Split('-')[0] + "-" + wood + "-" + data.content.Split('-')[2];
                        GetComponentInParent<BigSpawns>().buildingSpawn.GetComponentInChildren<BuildingScript>().wood = wood;
                    }
                    GetComponentInParent<BigSpawns>().SaveData();
                }
                int rockIndex = inv.FindItem(inv.items[2]);
                if (rockIndex != -1)
                {
                    int rock = int.Parse(data.content.Split('-')[2]);
                    rock += inv.playerItemsQuantities[rockIndex];
                    if (rock > 200)
                    {
                        inv.RemoveItem(inv.items[2], inv.playerItemsQuantities[rockIndex] - (rock - 200));
                    }
                    else
                    {
                        inv.RemoveItem(inv.items[2], inv.playerItemsQuantities[rockIndex]);
                    }
                    if (rock >= 200)
                    {
                        data.content = "HouseFinished";
                        Destroy(GetComponentInParent<BigSpawns>().buildingSpawn.transform.GetChild(0).gameObject);
                        Instantiate(GetComponentInParent<BigSpawns>().playerHouseFinished, GetComponentInParent<BigSpawns>().buildingSpawn.transform);
                    }
                    else if (rock > 100)
                    {
                        data.content = "House2-" + data.content.Split('-')[1] + "-" + rock;
                        Destroy(GetComponentInParent<BigSpawns>().buildingSpawn.transform.GetChild(0));
                        GameObject h = Instantiate(GetComponentInParent<BigSpawns>().playerHouse2, GetComponentInParent<BigSpawns>().buildingSpawn.transform);
                        h.GetComponentInChildren<BuildingScript>().wood = int.Parse(data.content.Split('-')[1]);
                        h.GetComponentInChildren<BuildingScript>().rock = rock;
                        GetComponentInParent<BigSpawns>().buildingSpawn.GetComponentInChildren<BuildingScript>().rock = rock;
                    }
                    else
                    {
                        data.content = data.content.Split('-')[0] + "-" + data.content.Split('-')[1] + "-" + rock;
                        GetComponentInParent<BigSpawns>().buildingSpawn.GetComponentInChildren<BuildingScript>().rock = rock;
                    }
                    GetComponentInParent<BigSpawns>().SaveData();
                }
            }
            else if (data.content == "Shop")
            {
                Shop s = transform.parent.parent.gameObject.GetComponentInChildren<Shop>();
                menu.GetComponent<ShopScript>().shop = s;
                menu.GetComponent<ShopScript>().RefreshInventory();
                if (menuController.gamePaused)
                {
                    inv.SetSellable(false);
                    menu.GetComponent<ShopScript>().Active(false);
                    inv.Active(false);
                    menuController.gamePaused = false;
                    GameObject.FindGameObjectWithTag("TransferOptions").GetComponent<TransferScript>().Hide();
                }
                else
                {
                    inv.SetSellable(true);
                    menu.GetComponent<ShopScript>().Active(true);
                    menu.GetComponent<InventoryScript>().Active(true);
                    menuController.gamePaused = true;
                    menuController.lastPressed = KeyCode.I;
                }
            }
            else if (data.content == "Altar")
            {
                if (menuController.gamePaused)
                {
                    menu.GetComponent<AltarScript>().Active(false);
                    inv.Active(false);
                    menuController.gamePaused = false;
                }
                else
                {
                    menu.GetComponent<AltarScript>().Active(true);
                    inv.Active(true);
                    menuController.gamePaused = true;
                    menuController.lastPressed = KeyCode.I;
                }

            }
            else if (data.content == "PirateBase")
            {
                if(GetComponentInParent<BigSpawns>().pirateSpawn.transform.childCount == 0)
                {
                    data.content = "HouseFinished";
                    GetComponentInParent<BigSpawns>().SaveData();
                    Destroy(GetComponentInParent<BigSpawns>().buildingSpawn.transform.GetChild(0).gameObject);
                    Instantiate(GetComponentInParent<BigSpawns>().playerHouseFinished, GetComponentInParent<BigSpawns>().buildingSpawn.transform);
                }
            }
        }
        else if (type.Contains("medium"))
        {
            if (inv.AvailableWeight() > 0)
            {
                MediumSpawns m = GetComponentInParent<MediumSpawns>();
                if (m.data.content == "Wood")
                {
                    inv.AddItem(inv.items[0], Random.Range(30, 50));
                    m.data.content = "Empty";
                    Destroy(m.resourceSpawn.transform.GetChild(0).gameObject);
                }
                else if (m.data.content == "Rock")
                {
                    inv.AddItem(inv.items[2], Random.Range(20, 40));
                    m.data.content = "Empty";
                    Destroy(m.resourceSpawn.transform.GetChild(0).gameObject);
                }
                DataManager.SaveSquareData(m.data, m.x, m.z);
            }
        }
    }
}
