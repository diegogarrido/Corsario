using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionInPlayer : MonoBehaviour {

    private GameObject playerCamera;
    public Vector3 offset;

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update () {
        transform.rotation = playerCamera.transform.rotation;
        transform.position = playerCamera.transform.position + offset;
    }

}
