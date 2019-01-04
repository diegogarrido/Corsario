using UnityEngine.UI;
using UnityEngine;

public class TutorialMapScript : MonoBehaviour
{

    public GameObject map;
    public GameObject panel;
    public GameObject section;
    public GameObject playerDot;

    void Start()
    {
        map.SetActive(false);
        panel.SetActive(false);
    }

    public void Active(bool active)
    {
        map.SetActive(active);
        panel.SetActive(active);
    }
}
