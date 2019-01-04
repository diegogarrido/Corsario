using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Control : MonoBehaviour
{

    public int lives;
    public GameObject[] bombs;
    public GameObject[] shipsIcon;
    public int shipIconCount=2;
    int auxLives;

    GameObject shipL;
    int ship1Lives;
    int ship2Lives;
    int ship3Lives;

    public GameObject Victory;
    public GameObject Lose;
    bool end;
    void Start () {
        lives = bombs.Length;
        auxLives = lives;
        shipL = GameObject.Find("juego");
        ship1Lives = shipL.GetComponent<PlaceShip>().shipLength[0];
        ship2Lives = shipL.GetComponent<PlaceShip>().shipLength[1];
        ship3Lives = shipL.GetComponent<PlaceShip>().shipLength[2];
    }

    public void QuitarVida(){
        if (!end)
        {
            try
            {
                bombs[auxLives - lives].SetActive(false);
                lives--;
                if (lives == 0)
                {
                    GameOver();
                }
            }
            catch (System.IndexOutOfRangeException)
            {

            }
        }

    }
    public void Acierto(int num){
        switch (num)
        {
            case 0:

                ship1Lives--;

                print("se quita vida a barco 1. Tiene " + ship1Lives + " vidas");
                if (ship1Lives == 0)
                {
                    DestroyShip();
                }
                break;
            case 1:

                ship2Lives--;

                print("se quita vida a barco 2. Tiene " + ship2Lives + " vidas");
                if (ship2Lives == 0)
                {
                    DestroyShip();
                }
                break;
            case 2:

                ship3Lives--;
                 
                print("se quita vida a barco 3. Tiene " + ship3Lives + " vidas");
                if (ship3Lives == 0)
                {
                    DestroyShip();
                }
                break;
        }
    }
    public void DestroyShip(){
        if (!end){

            try
            {

                shipsIcon[shipIconCount].SetActive(true);
                shipIconCount--;
                if (shipIconCount < 0)
                {
                    print("juego completado");
                    end = true;
                    Victory.SetActive(true);
                }
            }
            catch (System.IndexOutOfRangeException)
            {

            }
        }

    }
    void GameOver(){
        end = true;
        //shipsIcon[shipIconCount].SetActive(true);
        print("game over");
        Lose.SetActive(true);
    }
    public void Reaload(){
        SceneManager.LoadScene("game1");
    }
}
