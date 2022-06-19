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
        int safe = 0;                                                               // safevariable dass wir nicht in der while schleife hängenbleiben 
        if (min < 1) { min = 0; }                                                   // Sicherheits vorkehrung 
        if (max < 2) { max = 2; }                                                   // Sicherheits vorkehrung 
        if (min == 0 && max == 1)                                                   // Fals min gleich null ist und max == 1 kann man nicht generieren 
        {
            voll = true;
        }

        if (max > ((element.GetLength(0) - 2) * (element.GetLength(0) - 2)))        // Sicherheits vorkehrung max darf nmicht grösser sein wie es pltz gibt im Array
        {
            print("max erreicht ");
            max = ((element.GetLength(0) - 2) * (element.GetLength(0) - 2));
        }

        element_rücksetzen(element);                                                // rücksetzen des elment fals es schon beschriebenen ist

   
        int x = element.GetLength(0) / 2;
        int y = element.GetLength(1) / 2;
        element[x, y] = 1;                                      // Genau in der mit den erste punkt setzen 
        int sum = 1;
        int sum_z = 1;                                         // die Summe ist nun 1
        int Ra = Random.Range(min, max + 1);                   // diese variable gibt an wie viele pixel gesetzt werden 
        for (int i = 0; i < Ra; i++)                           // Random defiiniert wei viele pixels erstellt werden 
        {
            do
            {
                safe++;                                                            // safe variable hochzächlen 
                int h = Random.Range(0, 4);                                        // in eine Random richtung gehen
                if (h == 3 && x != element.GetLength(0) - 2) { x++; }              // je nach der random zahl gehen wir in eine Richtung 
                if (h == 2 && y != element.GetLength(1) - 2) { y++; }
                if (h == 1 && x != 1) { x--; }
                if (h == 0 && y != 1) { y--; }
                if (element[x, y] != 1)                                            //wir sind in die richtung gegangen und fals dieser pixel noch micht 1 ist setzen wir es ein und die summe wird um 1 erhöcht
                {
                    element[x, y] = 1;
                    sum++;
                }
            } while (sum == sum_z && safe >= 1000);                          //  dieser Vorgang wird so lang gemacht bis die summe um 1 erhöcht wird oder wir die safevariable überschritten haben 
            safe = 0;           //safe variable rücksetzen 
            sum_z = sum;        //summe gleich der summesafe setzen
        }
        if (voll)   
        {
            element_rücksetzen(element);
        }
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

        int[] cords = new int[4]; // hier verwende ich ein arry damit ich nicht vier variablen habe 
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
    public bool BiomVereinfacher(long[,] Map ,int X, int Y, long element, int radius, int anzahl,int diffident)
    {
        int elemnte = 0;
        for (int i = 0; i < radius * 2 + 1; i++) //radius mal 2 da durchmesser 
        {
            for (int r = 0; r < radius * 2 + 1; r++)
            {
                if ((Map[X + i - radius, Y + r - radius]% 1000000000) - (Map[X + i - radius, Y + r - radius]% diffident) == element )
                {
                    elemnte++;
                }
                    
                if (elemnte == anzahl)
                {
                    
                    return false;
                }
            }
        }
     
        return true;
    }
    public bool BiomRostoff_test(long ID, int Biom,int Rohstoff) // diese Funktion pass hier nicht her änder dass ZK Otto
    {
        if ((ID % 10000000000) / 100000000 == Biom && (ID % 100000000) / 1000000 == Rohstoff)
        {
            return true;
        }
        return false;
    }
    public bool Rohstoff_test(long ID, int ID_test) // diese Funktion pass hier nicht her änder dass ZK Otto
    {
        if ((ID % 1000000) / 10000 == ID_test)
        {
            return true;
        }
        return false;
    }
    public bool Biom_test(long ID, int ID_test) // diese Funktion pass hier nicht her änder dass ZK Otto
    {
        if ((ID % 10000000000) / 100000000 == ID_test)
        {
            return true;
        }
        return false;
    }
    public bool BiomVereinfacher(int[,] Map, int X, int Y, long element, int radius, int anzahl, int diffident)
    {
        int elemnte = 0;
        for (int i = 0; i < radius * 2 + 1; i++) //radius mal 2 da durchmesser 
        {
            for (int r = 0; r < radius * 2 + 1; r++)
            {
                if ((Map[X + i - radius, Y + r - radius] % 1000000000) - (Map[X + i - radius, Y + r - radius] % diffident) == element)
                {
                    elemnte++;
                }

                if (elemnte == anzahl)
                {

                    return false;
                }
            }
        }

        return true;
    }
    #endregion
}


