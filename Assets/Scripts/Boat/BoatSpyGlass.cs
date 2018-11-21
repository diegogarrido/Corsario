using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatSpyGlass : MonoBehaviour
{

    public GameObject SpyglassFilter;
    public GameObject mainCamera;
    public GameObject zoomCamera;
    public float zoomLvl;

    public bool isZooming;

    void Start()
    {
        isZooming = false;
        zoomCamera.SetActive(false);
        SpyglassFilter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            mainCamera.SetActive(false);
            zoomCamera.SetActive(true);
            if (!isZooming)
            {
                zoomCamera.transform.localPosition += new Vector3(0, -1 * zoomLvl, 0);
            }
            SpyglassFilter.SetActive(true);
            isZooming = true;
        }
        else
        {
            mainCamera.SetActive(true);
            zoomCamera.SetActive(false);
            SpyglassFilter.SetActive(false);
            isZooming = false;
        }
    }
}
