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
            float angle = rotationSpeed * (Input.GetAxis("Mouse X"));
            transform.RotateAround(player.position, Vector3.up, angle);
        }
    }

    private void Direction()
    {
        float angle = transform.localRotation.eulerAngles.y;
        if (angle < 101)
        {
            looking = "Right";
        }
        else if (angle > 259)
        {
            looking = "Left";
        }
        else if((angle <= 240 && angle >= 120))
        {
            looking = "Front/Back";
        }
    }
}
