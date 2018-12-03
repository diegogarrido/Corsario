using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{

    public string itemName;
    public Sprite sprite;
    public bool equipable;
    public string equipableType;
    public bool usable;
    public float weight;
    [Range(1, 10)]
    public int rarity;
    public int price;

}
