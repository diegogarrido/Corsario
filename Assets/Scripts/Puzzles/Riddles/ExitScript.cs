using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitScript : MonoBehaviour
{

    public GameObject ExitOption;
    public GameObject lose;

    void Start()
    {
        ExitOption.SetActive(false);
        lose.SetActive(false);
    }

    public void Exit()
    {
        ExitOption.SetActive(true);
    }

    public void Confirm()
    {
        SceneManager.UnloadSceneAsync("Acertijos");
    }

    public void Deny()
    {
        ExitOption.SetActive(false);
    }
}
