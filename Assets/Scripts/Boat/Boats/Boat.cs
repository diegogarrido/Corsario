using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boat", menuName = "Boat")]
public class Boat : ScriptableObject
{

    public string boatName;
    public float speed;
    public float turnSpeed;
    public float health;
    public float capacity;
    public int cannonsPerSide;
    public int price;
    public Sprite icon;

}
