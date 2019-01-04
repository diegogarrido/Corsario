using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatOfficer : EnemyIA
{

    public Vector3[] patrolPoints;
    public int currentPoint = 0;

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
        if (boat.loaded)
        {
            if (boat.health <= 0 && !dying)
            {
                StartCoroutine(DieIn(5));
                dying = true;
            }
            else if (patrolPoints != null && patrolPoints.Length > 0)
            {
                if (chasing == null && Vector3.Distance(transform.position, patrolPoints[currentPoint]) > 10 && boat.health > 0)
                {
                    if (transform.InverseTransformPoint(patrolPoints[currentPoint]).x < 0)
                    {
                        TurnLeft(1f);
                    }
                    else if (transform.InverseTransformPoint(patrolPoints[currentPoint]).x > 0)
                    {
                        TurnRight(1f);
                    }
                    if (Vector3.Dot((transform.position - patrolPoints[currentPoint]).normalized, transform.up) > 0.9)
                    {
                        GoForward(1f);
                    }
                    else if (Vector3.Dot((transform.position - patrolPoints[currentPoint]).normalized, transform.up) < -0.9)
                    {
                        GoBackward(1f);
                    }
                }
                else if (Vector3.Distance(transform.position, patrolPoints[currentPoint]) <= 10)
                {
                    currentPoint++;
                    if (currentPoint >= patrolPoints.Length)
                    {
                        currentPoint = 0;
                    }
                }
            }
            else if (chasing == null && Vector3.Distance(transform.position, start) > 10 && boat.health > 0)
            {
                if (transform.InverseTransformPoint(start).x < 0)
                {
                    TurnLeft(1f);
                }
                else if (transform.InverseTransformPoint(start).x > 0)
                {
                    TurnRight(1f);
                }
                if (Vector3.Dot((transform.position - start).normalized, transform.up) > 0.9)
                {
                    GoForward(1f);
                }
                else if (Vector3.Dot((transform.position - start).normalized, transform.up) < -0.9)
                {
                    GoBackward(1f);
                }
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<BoatController>().chased = false;
            chasing = null;
        }
        else if (other.gameObject.tag.Contains("Pirate"))
        {
            chasing = null;
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && boat.health > 0)
        {
            if (chasing == null)
            {
                chasing = "Player";
            }
            else if (chasing == "Player")
            {
                other.gameObject.GetComponent<BoatController>().chased = true;
                Follow(other.gameObject);
            }
        }
        else if (other.gameObject.tag.Contains("Pirate") && boat.health > 0)
        {
            if (chasing == null)
            {
                chasing = "Pirate";
            }
            else if (chasing == "Pirate")
            {
                Follow(other.gameObject);
            }
        }
        else if ((other.gameObject.tag == "Player") && boat.health <= 0)
        {
            other.gameObject.GetComponent<BoatController>().chased = false;
            chasing = null;
        }
    }
}
