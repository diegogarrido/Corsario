using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public int number;
    public Riddle ridd;

    private GameObject lose;
    private InventoryScript inv;

	void Start () {
        lose = transform.parent.parent.parent.GetChild(transform.parent.parent.parent.childCount - 1).gameObject;
        lose.SetActive(false);
        inv = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
    }

    public void Click( )
    {
        if (ridd.correct == number)
        {
            Win();
        }else
        {
            Lose();
        }
    }
    public void Win()
    {
        Item i = RollItem();
        float ammount = Random.Range(1, 200 / (i.rarity * 10));
        ammount *= ridd.difficulty;
        inv.AddItem(i, int.Parse(""+ammount));
        transform.parent.parent.parent.gameObject.GetComponent<ExitScript>().Confirm();
    }

    public Item RollItem()
    {
         Item item = inv.items[Random.Range(0, inv.items.Length)];
         if (Random.Range(0, 100) >= (item.rarity * 10) - 10)
         {
            return item;
         }
         else
         {
             return RollItem();
         } 
    }

     public void Lose()
     {
        lose.SetActive(true);
     }


}
