using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChoosePuzzle()
    {
        int type = 0;//Random.Range(0,4);
        if (type == 0)
        {
            SceneManager.LoadScene("Acertijos",LoadSceneMode.Additive);

        }
    }
}
