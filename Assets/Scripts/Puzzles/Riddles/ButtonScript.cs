using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public int number;
    public Riddle ridd;

    private GameObject lose;
   

    void Start()
    {
        lose = transform.parent.parent.parent.GetChild(transform.parent.parent.parent.childCount - 1).gameObject;
        lose.SetActive(false);
    
    }

    public void Click()
    {
        if (ridd.correct == number)
        {
            Win();
        }
        else
        {
            Lose();
        }
    }

    public void Win()
    {
        GameObject.FindGameObjectWithTag("Puzzle").GetComponent<Puzzle>().Win(ridd.difficulty);
        transform.parent.parent.parent.gameObject.GetComponent<ExitScript>().Confirm();
    }

    public void Lose()
    {
        GameObject.FindGameObjectWithTag("Puzzle").GetComponent<Puzzle>().Lose();
        lose.SetActive(true);
    }

}
