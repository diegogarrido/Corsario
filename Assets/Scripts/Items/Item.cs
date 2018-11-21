using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{

    public string itemName;
    public Sprite sprite;
    public bool equipable;
    public bool usable;
    public float weight;

}
