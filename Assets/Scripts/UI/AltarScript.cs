using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AltarScript : MonoBehaviour {

    public Text coins;
    public GameObject altarMenu;
    public GameObject putButton;

    private InventoryScript inv;

	void Start () {
        inv = GetComponent<InventoryScript>();
        coins.text = "" + inv.coinsInAltar + "/250";
        if (inv.gameFinished)
        {
            putButton.SetActive(false);
        }
	}

    public void PutCoins()
    {
        int index = inv.FindItem(inv.items[7]);
        if (index != -1)
        {
            int amount = inv.playerItemsQuantities[index];
            inv.coinsInAltar += amount;
            coins.text = "" + inv.coinsInAltar + "/250";
            inv.RemoveItem(inv.items[7], amount);
            if(amount >= 250 && !inv.gameFinished)
            {
                inv.gameFinished = true;
                inv.SaveInventory();
                LoadEnd();
            }
        }
    }

    public void Active(bool active)
    {
        altarMenu.SetActive(active);
    }

    private void LoadEnd()
    {
        PlayerPrefs.SetString("Load", "EndGame");
        SceneManager.LoadScene("LoadingScreen");
    }
}
