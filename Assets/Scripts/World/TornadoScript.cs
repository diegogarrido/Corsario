using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{

    public float speed;
    public float area;
    public float timer;

    private Vector3 spawnerPosition;
    private Vector3 destination;

    void Start()
    {
        spawnerPosition = transform.position;
        destination = CalculateNewDestination();
        timer = Random.Range(0, 5);
    }

    void Update()
    {
        Roam();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ship")
        {
            other.GetComponentInParent<BoatScript>().health -= 100;
        }
    }

    void Roam()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            destination = CalculateNewDestination();
            timer = Random.Range(0, 5);
        }
    }

    private Vector3 CalculateNewDestination()
    {
        return spawnerPosition + new Vector3(Random.Range(-area, area), 0, Random.Range(-area, area));
    }

}
