using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float y = 15;
    public Transform player;
    public float rotationSpeed;
    public string looking;

    private void Update()
    {
        transform.position = new Vector3(GetComponentInParent<Transform>().position.x, y, GetComponentInParent<Transform>().position.z);
        Rotation();
        Direction();
    }

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            bool canRotate = false;
            if (Input.GetAxis("Mouse X") < 0 && transform.localRotation.y > -0.8)
            {
                canRotate = true;
            }
            else if (transform.localRotation.y < 0.8 && Input.GetAxis("Mouse X") > 0)
            {
                canRotate = true;
            }
            if (canRotate)
            {
                float angle = rotationSpeed * (Input.GetAxis("Mouse X"));
                transform.RotateAround(player.position, Vector3.up, angle);
            }
        }
    }

    private void Direction()
    {
        if (transform.localRotation.y > 0.5 && transform.localRotation.y < 0.7)
        {
            looking = "Right";
        }
        else if (transform.localRotation.y < -0.5 && transform.localRotation.y > -0.7)
        {
            looking = "Left";
        }
        else if (transform.localRotation.y >= -0.5 && transform.localRotation.y <= 0.5)
        {
            looking = "Front";
        }
        else
        {
            looking = "Back";
        }
    }
}
