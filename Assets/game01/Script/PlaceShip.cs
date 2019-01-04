using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceShip : MonoBehaviour {

    public GameObject[] celda;
    int direc;
    int celdaShip;
    public int[] shipLength;
    int countShip=0;
    int auxV;
    int auxH;
    bool placeError;
    void Start () {
        print(celda.Length);
        LoadShips();

    }
	
    public void LoadShips(){
        while (countShip < 3)
        {

            placeError = false;
            print("se inician los barcos");
            //1 vertical - 2 horizontal
            direc = Random.Range(1, 3);

            celdaShip = Random.Range(0, celda.Length);
            try
            {
                if (direc == 1)
                {
                    //esto se puede optimizar, pero no quiero hacer mas enrredado el codigo
                    //primera columna
                    if (celdaShip >= 0 && celdaShip <= 7)
                    {
                        auxV = celdaShip + (shipLength[countShip] - 1);
                        if (auxV > 7)
                        {
                            LoadShips();
                        }
                        else
                        {

                            for (int i = celdaShip; i <= auxV; i++)
                            {
                                if (celda[i].GetComponent<Cell>().ship)
                                {
                                    placeError = true;
                                    LoadShips();
                                }
                            }
                            if (!placeError)
                            {
                                for (int i = celdaShip; i <= auxV; i++)
                                {
                                    celda[i].GetComponent<Cell>().ship = true;
                                    celda[i].GetComponent<Cell>().NumShip = countShip;
                                }
                                countShip++;
                                LoadShips();

                            }
                        }
                    }
                    //segunda columna
                    if (celdaShip >= 8 && celdaShip <= 15)
                    {
                        auxV = celdaShip + (shipLength[countShip] - 1);
                        if (auxV > 15)
                        {
                            LoadShips();
                        }
                        else
                        {

                            for (int i = celdaShip; i <= auxV; i++)
                            {
                                if (celda[i].GetComponent<Cell>().ship)
                                {
                                    placeError = true;
                                    LoadShips();
                                }
                            }
                            if (!placeError)
                            {
                                for (int i = celdaShip; i <= auxV; i++)
                                {
                                    celda[i].GetComponent<Cell>().ship = true;
                                    celda[i].GetComponent<Cell>().NumShip = countShip;
                                }
                                countShip++;
                                LoadShips();

                            }
                        }
                    }
                    //tercera columna
                    if (celdaShip >= 16 && celdaShip <= 23)
                    {
                        auxV = celdaShip + (shipLength[countShip] - 1);
                        if (auxV > 23)
                        {
                            LoadShips();
                        }
                        else
                        {

                            for (int i = celdaShip; i <= auxV; i++)
                            {
                                if (celda[i].GetComponent<Cell>().ship)
                                {
                                    placeError = true;
                                    LoadShips();
                                }
                            }
                            if (!placeError)
                            {
                                for (int i = celdaShip; i <= auxV; i++)
                                {
                                    celda[i].GetComponent<Cell>().ship = true;
                                    celda[i].GetComponent<Cell>().NumShip = countShip;
                                }
                                countShip++;
                                LoadShips();

                            }
                        }
                    }
                    //cuarta columna
                    if (celdaShip >= 24 && celdaShip <= 31)
                    {
                        auxV = celdaShip + (shipLength[countShip] - 1);
                        if (auxV > 31)
                        {
                            LoadShips();
                        }
                        else
                        {

                            for (int i = celdaShip; i <= auxV; i++)
                            {
                                if (celda[i].GetComponent<Cell>().ship)
                                {
                                    placeError = true;
                                    LoadShips();
                                }
                            }
                            if (!placeError)
                            {
                                for (int i = celdaShip; i <= auxV; i++)
                                {
                                    celda[i].GetComponent<Cell>().ship = true;
                                    celda[i].GetComponent<Cell>().NumShip = countShip;
                                }
                                countShip++;
                                LoadShips();

                            }
                        }
                    }
                    //quinta columna
                    if (celdaShip >= 32 && celdaShip <= 39)
                    {
                        auxV = celdaShip + (shipLength[countShip] - 1);
                        if (auxV > 39)
                        {
                            LoadShips();
                        }
                        else
                        {

                            for (int i = celdaShip; i <= auxV; i++)
                            {
                                if (celda[i].GetComponent<Cell>().ship)
                                {
                                    placeError = true;
                                    LoadShips();
                                }
                            }
                            if (!placeError)
                            {
                                for (int i = celdaShip; i <= auxV; i++)
                                {
                                    celda[i].GetComponent<Cell>().ship = true;
                                    celda[i].GetComponent<Cell>().NumShip = countShip;
                                }
                                countShip++;
                                LoadShips();

                            }
                        }
                    }
                    //sexta columna
                    if (celdaShip >= 40 && celdaShip <= 47)
                    {
                        auxV = celdaShip + (shipLength[countShip] - 1);
                        if (auxV > 47)
                        {
                            LoadShips();
                        }
                        else
                        {

                            for (int i = celdaShip; i <= auxV; i++)
                            {
                                if (celda[i].GetComponent<Cell>().ship)
                                {
                                    placeError = true;
                                    LoadShips();
                                }
                            }
                            if (!placeError)
                            {
                                for (int i = celdaShip; i <= auxV; i++)
                                {
                                    celda[i].GetComponent<Cell>().ship = true;
                                    celda[i].GetComponent<Cell>().NumShip = countShip;
                                }
                                countShip++;
                                LoadShips();

                            }
                        }
                    }
                    //septima columna
                    if (celdaShip >= 48 && celdaShip <= 55)
                    {
                        auxV = celdaShip + (shipLength[countShip] - 1);
                        if (auxV > 55)
                        {
                            LoadShips();
                        }
                        else
                        {

                            for (int i = celdaShip; i <= auxV; i++)
                            {
                                if (celda[i].GetComponent<Cell>().ship)
                                {
                                    placeError = true;
                                    LoadShips();
                                }
                            }
                            if (!placeError)
                            {
                                for (int i = celdaShip; i <= auxV; i++)
                                {
                                    celda[i].GetComponent<Cell>().ship = true;
                                    celda[i].GetComponent<Cell>().NumShip = countShip;
                                }
                                countShip++;
                                LoadShips();

                            }
                        }
                    }
                    //octaba columna
                    if (celdaShip >= 56 && celdaShip <= 63)
                    {
                        auxV = celdaShip + (shipLength[countShip]-1);
                        if (auxV > 63)
                        {
                            LoadShips();
                        }
                        else
                        {

                            for (int i = celdaShip; i <= auxV; i++)
                            {
                                if (celda[i].GetComponent<Cell>().ship)
                                {
                                    placeError = true;
                                    LoadShips();
                                }
                            }
                            if (!placeError)
                            {
                                for (int i = celdaShip; i <= auxV; i++)
                                {
                                    celda[i].GetComponent<Cell>().ship = true;
                                    celda[i].GetComponent<Cell>().NumShip = countShip;
                                }
                                countShip++;
                                LoadShips();

                            }
                        }
                    }


                }
                else
                {
                    auxH = celdaShip + (8 * shipLength[countShip]);
                    if (auxH <= 64)
                    {
                        for (int i = 0; i < shipLength[countShip]; i++)
                        {
                            if (celda[celdaShip + (8 * i)].GetComponent<Cell>().ship)
                            {
                                placeError = true;
                                LoadShips();
                            }
                        }
                        if (!placeError)
                        {
                            for (int i = 0; i < shipLength[countShip]; i++)
                            {
                                celda[celdaShip + (8 * i)].GetComponent<Cell>().ship = true;
                                celda[celdaShip + (8 * i)].GetComponent<Cell>().NumShip = countShip;
                            }
                            countShip++;
                        }
                    }
                }

            }
            catch (System.IndexOutOfRangeException)
            {
                LoadShips();
            }
        }
       
    }
}
