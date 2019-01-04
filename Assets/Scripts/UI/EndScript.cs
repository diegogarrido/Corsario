using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class EndScript : MonoBehaviour {

    public GameObject[] texts;
    public GameObject backButton;
    public GameObject textPanel;
	
	void Start () {
        textPanel.SetActive(false);
        backButton.SetActive(false);
        StartCoroutine(RunText());
	}
	
	
	void Update () {
		
	}

    public void GoBack()
    {
        PlayerPrefs.SetString("Load", "Menu");
        SceneManager.LoadScene("LoadingScreen");
    }

    public IEnumerator RunText()
    {
        textPanel.SetActive(true);
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].SetActive(true);
            yield return new WaitForSeconds(5);
            texts[i].SetActive(false);
        }
        backButton.SetActive(true);
        textPanel.SetActive(false);
    }
}
