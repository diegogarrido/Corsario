using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour {

    public bool open;
    public bool failed;
    public bool active;
	
	void Start () {
        failed = open = active = false;
	}

	void Update () {
        if (open && active)
        {
            GetComponent<Animator>().SetBool("Opened",true);
        }
        if(GameObject.FindGameObjectWithTag("Puzzle") != null && active)
        {
            open = GameObject.FindGameObjectWithTag("Puzzle").GetComponent<Puzzle>().completed;
            failed = GameObject.FindGameObjectWithTag("Puzzle").GetComponent<Puzzle>().failed;
        }
    }

    public void ChoosePuzzle()
    {
        int type = Random.Range(0,2);
        switch (type)
        {
            case 0:
                PlayerPrefs.SetString("Load", "Acertijos");
                SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
                GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().HideMainUI();
                break;
            case 1:
                PlayerPrefs.SetString("Load", "Puzzle_Laberinto1");
                SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
                GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().HideMainUI();
                break;
        }
    }
}
