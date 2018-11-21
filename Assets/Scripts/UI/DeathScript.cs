using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour {

    public GameObject deathPanel;
    public BoatScript player;

    private void Start()
    {
        deathPanel.SetActive(false);
    }

    private void Update()
    {
        if(player.health <= 0)
        {
            deathPanel.SetActive(true);
            Animate();
        }
    }

    public void Animate()
    {
        StartCoroutine(Anim());
    }

    private IEnumerator Anim() {
        for(float i = 0; i < 0.8f; i+=0.1f)
        {
            deathPanel.GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void ReloadScene()
    {
        PlayerPrefs.SetString("Load", "Game");
        SceneManager.LoadScene(3);
    }
}
