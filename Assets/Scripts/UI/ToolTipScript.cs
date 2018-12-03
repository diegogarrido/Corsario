using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipScript : MonoBehaviour {

    public GameObject stats;
    public Text title;
    public GameObject statPrefab;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void AddStat(string stat,string value)
    {
        GameObject s = Instantiate(statPrefab,stats.transform);
        s.GetComponent<Text>().text = stat + ": " + value;
    }
}
