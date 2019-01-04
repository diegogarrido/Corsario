using UnityEngine;

public class BoatPirate : EnemyIA
{

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
        else if (other.gameObject.tag.Contains("Officer"))
        {
            chasing = null;
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && boat.health > 0)
        {
            if(chasing == null)
            {
                chasing = "Player";
            }
            else if(chasing == "Player")
            {
                other.gameObject.GetComponent<BoatController>().chased = true;
                Follow(other.gameObject);
            }
        }
        else if (other.gameObject.tag.Contains("Officer") && boat.health > 0)
        {
            if (chasing == null)
            {
                chasing = "Officer";
            }
            else if (chasing == "Officer")
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
