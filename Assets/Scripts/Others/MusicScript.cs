using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

    public float maxVolume;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        StartCoroutine(FadeIn());
    }

    public void Stop()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        GetComponent<AudioSource>().volume = 0;
        GetComponent<AudioSource>().Play();
        for (float i = 0; i <= maxVolume; i+=0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            GetComponent<AudioSource>().volume = i; 
        }
    }

    private IEnumerator FadeOut()
    {
        for (float i = GetComponent<AudioSource>().volume; i > 0; i -= 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            GetComponent<AudioSource>().volume = i;
        }
        GetComponent<AudioSource>().Pause();
    }
}
