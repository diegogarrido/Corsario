using UnityEngine.SceneManagement;
using UnityEngine;

public class LabyEnd : MonoBehaviour {

    public GameObject panelLose;

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "PuzzleBall")
        {
            GameObject.FindGameObjectWithTag("Puzzle").GetComponent<Puzzle>().Win(1.8f);
            Confirm();
        }
    }

    public void Confirm()
    {
        GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().ShowMainUI();
        GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuController>().gamePaused = false;
        SceneManager.UnloadSceneAsync("Puzzle_Laberinto1");
    }

    public void Lose()
    {
        GameObject.FindGameObjectWithTag("Puzzle").GetComponent<Puzzle>().Lose();
        panelLose.SetActive(true);
    }
}
