using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour {

    public Slider slider;

    private AsyncOperation async;

    private void Start()
    {
        slider.value = 0;
        LoadScene(PlayerPrefs.GetString("Load"));
    }

    public void LoadScene(string name)
    {
        StartCoroutine(Load(name));
    }

    private IEnumerator Load(string scene)
    {
        async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            slider.value = async.progress;
            if(async.progress == 0.9f)
            {
                slider.value = 1;
                async.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
