using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rohstoffe : MonoBehaviour
{
    public static Rohstoffe singleton { set; get; }

    bool voll;

    private void Awake()
    {
        singleton = this;
    }
    #region Funktionen
    public void generieren(int Min, int Max, int[,] element)
    {
        int min = Min, max = Max;
        int safe = 0;                                                               // safevariable dass wir nicht in der whilöe schleife hängenbleiben 
        if (min < 1) { min = 0; }
        if (max < 2) { max = 2; }
        if (min == 0 && max == 1)
        {
            print("Fehler: Chunck ist zu klein element hat keinen platz");
            voll = true;
        }

        if (max > ((element.GetLength(0) - 2) * (element.GetLength(0) - 2)))        // sicherheit
        {
            print("max erreicht ");
            max = ((element.GetLength(0) - 2) * (element.GetLength(0) - 2));
        }

        element_rücksetzen(element);                                                // rücksetzen des elment fals es schon beschriebenen ist

        int sum = 4;
        int sum_z = 4;
        int x = element.GetLength(0) / 2;
        int y = element.GetLength(1) / 2;
        int Ra = Random.Range(min, max + 1);
        for (int i = 0; i < Ra; i++)                           // Random defiiniert wei viele pixels erstellt werden 
        {
            do
            {
                safe++;                                                            // safe variable hochzächlen 
                int h = Random.Range(0, 4);
                if (h == 3 && x != element.GetLength(0) - 2) { x++; }
                if (h == 2 && y != element.GetLength(1) - 2) { y++; }
                if (h == 1 && x != 1) { x--; }
                if (h == 0 && y != 1) { y--; }
                if (element[x, y] != 1)
                {
                    element[x, y] = 1;
                    sum++;
                }
            } while (sum == sum_z && safe >= 1000);
            safe = 0;
            sum_z = sum;
        }
        if (voll)
        {
            element_rücksetzen(element);
        }
        // schauen ob ein teil eingeschlossen ist 


    }
    public void generieren_spezial(int Min, int Max, int[,] element)
    {
        int min = Min, max = Max;
        int safe = 0;
        if (min < 1) { min = 0; }
        if (max < 2) { max = 1; }
        if (min == 0 && max == 1) {
            voll = true;
        }

        if (max > ((element.GetLength(0) - 2) * (element.GetLength(0) - 2))) {
            max = ((element.GetLength(0) - 2) * (element.GetLength(0) - 2));
        }

        element_rücksetzen(element);
        int sum = 4;
        int sum_z = 4;
        int x = element.GetLength(0) / 2;
        int y = element.GetLength(1) / 2;

        for (int i = 0; i < 2; i++)
        {
            for (int e = 0; e < 2; e++)
            {
                element[x + i, y + e] = 1;
            }
        }
        int R = Random.Range(min, max);
        for (int i = 0; i < R; i++) // Random defiiniert wei viele bäume erstellt werden 
        {
            do
            {
                safe++;
                int h = Random.Range(0, 4);
                if (h == 3 && x != element.GetLength(0) - 2) { x++; }
                if (h == 2 && y != element.GetLength(1) - 2) { y++; }
                if (h == 1 && x != 1) { x--; }
                if (h == 0 && y != 1) { y--; }
                if (element[x, y] != 1)
                {
                    if (h == 2 || h == 0)
                    {
                        if (h == 2)
                        {
                            if (element[x + 1, y - 1] != 0)
                            {
                                element[x, y] = 1;
                                element[x + 1, y] = 1;
                                sum = +2;
                            }
                            else if (element[x - 1, y - 1] != 0)
                            {
                                element[x, y] = 1;
                                element[x - 1, y] = 1;
                                sum = +2;
                            }
                        }
                        else if (h == 0)
                        {
                            if (element[x + 1, y + 1] != 0)
                            {
                                element[x, y] = 1;
                                element[x + 1, y] = 1;
                                sum = +2;
                            }
                            else if (element[x - 1, y + 1] != 0)
                            {
                                element[x, y] = 1;
                                element[x - 1, y] = 1;
                                sum = +2;
                            }
                        }
                    }
                    if (h == 3 || h == 1)
                    {
                        if (h == 3)
                        {
                            if (element[x - 1, y + 1] != 0)
                            {
                                element[x, y] = 1;
                                element[x, y + 1] = 1;
                                sum = +2;
                            }
                            else if (element[x - 1, y - 1] != 0)
                            {
                                element[x, y] = 1;
                                element[x, y - 1] = 1;
                                sum = +2;
                            }
                        }
                        if (h == 1)
                        {
                            if (element[x + 1, y - 1] != 0)
                            {
                                element[x, y] = 1;
                                element[x, y - 1] = 1;
                                sum++;
                            }
                            if (element[x + 1, y + 1] != 0)
                            {
                                element[x, y] = 1;
                                element[x, y + 1] = 1;
                                sum++;
                            }
                        }
                    }

                }
            } while (sum == sum_z && safe >= 1000);
            safe = 0;
            sum_z = sum;
        }
        if (voll)
        {
            element_rücksetzen(element);
        }

    }
    public int[] größe_feststellen(int[,] element)
    {

        int[] cords = new int[4];
        cords[0] = 0;
        cords[1] = 0;
        cords[2] = element.GetLength(0);
        cords[3] = element.GetLength(1);
        // hier berchen ich die grössten zahl im array
        for (int i = 0; i < element.GetLength(0) && !voll; i++)
        {
            for (int a = 0; a < element.GetLength(1); a++)
            {
                if (element[i, a] != 0)
                {
                    if (a > cords[1])
                    {
                        cords[1] = a;
                    }
                    if (i > cords[0])
                    {
                        cords[0] = i;
                    }
                }
            }
        }
        for (int i = element.GetLength(0) - 1; i > 0 && !voll; i--)
        {
            for (int a = element.GetLength(1) - 1; a > 0; a--)
            {
                if (element[i, a] != 0)
                {
                    if (a < cords[3])
                    {
                        cords[3] = a;
                    }
                    if (i < cords[2])
                    {
                        cords[2] = i;
                    }
                }
            }
        }
        // Postitions BReeich ERrechnnen Und Erstellen

        return cords;
    }
    public int[,] element_rücksetzen(int[,] element)
    {
        for (int i = 0; i < element.GetLength(0); i++)
        {
            for (int q = 0; q < element.GetLength(1); q++)
            {
                element[i, q] = 0;
            }
        }
        return element;
    }

    #endregion
    public bool BiomVereinfacher(int X, int Y, int element, int element2, int radius, int anzahl)
    {

        int elemnte = 0;
        for (int i = 0; i < radius * 2 + 1; i++) //radius mal 2 da durchmesser 
        {
            for (int r = 0; r < radius * 2 + 1; r++)
            {
                if (Map.Map_Rohstoffe[X + i - radius, Y + r - radius] > element && Map.Map_Rohstoffe[X + i - radius, Y + r - radius] < element2)
                {
                    elemnte++;
                }
                    
                if (elemnte == anzahl)
                {
                    elemnte = 0;
                    return false;
                }
            }
        }
     
        return true;
    }
}  


