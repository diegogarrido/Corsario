using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cannon", menuName = "Cannon")]
public class Cannon : ScriptableObject
{
    
    public string cannonName;
    public float shootForce;
    public float coolDown;
    public float weigth;
    public int shoots;

    public GameObject Shoot(GameObject cannonBall, GameObject cannon, GameObject shootPoint, GameObject shooter)
    {
      
        GameObject ball = Instantiate(cannonBall);
        ball.GetComponent<CannonBallScript>().shooter = shooter;
        ball.GetComponent<Rigidbody>().mass *= cannon.transform.localScale.x;
        ball.transform.localScale = cannon.transform.localScale * 1.5f;
        ball.transform.position = shootPoint.transform.position;
        Vector3 direction = cannon.transform.forward + (cannon.transform.up * 0.3f);
        ball.GetComponent<Rigidbody>().AddRelativeForce(direction * shootForce, ForceMode.Impulse);
        return ball;
    }
}
