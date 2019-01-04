using UnityEngine;

public class BoatSpyGlass : MonoBehaviour
{

    public GameObject SpyglassFilter;
    public GameObject mainCamera;
    public GameObject zoomCamera;
    public float zoomLvl;
    public bool isZooming;

    private Vector3 start;

    void Start()
    {
        isZooming = false;
        zoomCamera.SetActive(false);
        SpyglassFilter.SetActive(false);
        start = zoomCamera.transform.localPosition;
    }

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
            zoomCamera.transform.localPosition = start;
            mainCamera.SetActive(true);
            zoomCamera.SetActive(false);
            SpyglassFilter.SetActive(false);
            isZooming = false;
        }
    }
}
