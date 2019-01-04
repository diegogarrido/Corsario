using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Spin90());
        }
    }

     public IEnumerator Spin90()
    {
        
        for (int i = 0; i < 90; i++)
        {
            transform.Rotate(new Vector3(0f, 0f, 1f));
            yield return new WaitForSeconds(0.00001f);
        }
    }

   
}

