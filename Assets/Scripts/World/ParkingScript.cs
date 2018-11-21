using UnityEngine.SceneManagement;
using UnityEngine;

public class ParkingScript : MonoBehaviour {

    public GameObject interactionMenu;

	void Start () {
        interactionMenu.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactionMenu.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactionMenu.SetActive(false);
        }
    }

    public void Interact(string type)
    {
        if (type == "small") {
            PlayerPrefs.SetString("Load", "Acertijos");
            SceneManager.LoadScene("LoadingScreen");
        }
    }
}
