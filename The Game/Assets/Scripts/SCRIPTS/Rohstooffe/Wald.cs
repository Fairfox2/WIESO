using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wald : Rohstoffe
{
    #region Wald
    public static bool random = true;
    public WeightedRandomList<Transform> Wälder;
    public WeightedRandomList<Transform> Boden;
    public WeightedRandomList<Transform> Wald_Boden;
    public static Wald singleton { set; get; }
    public void Awake()
    {
        singleton = this;
    }
    public void Wiese_Boden(Grid_opjekt Objekt, bool Random)
    {
       random = Random;
        if (random == true)
        {
            Objekt.Index_Boden = Boden.GetRandom();
            Objekt.Set_Rotation_Random(true);
        }
        Objekt.Boden = Boden.Get(Objekt.Index_Boden);

    }
    public void Wald_boden(Grid_opjekt Objekt, bool Random)
    {
        random = Random;
        if (random == true)
        {
            Objekt.Index_Boden = Wald_Boden.GetRandom();
            Objekt.Set_Rotation_Random(true);
        }
        Objekt.Boden = Wald_Boden.Get(Objekt.Index_Boden);

    }
    public void Wald_erstellen( int ZONE, int ZONE_MAX, int anzahl)
    { 
        bool voll = false;
        int xm = 12;            //Die Maximal grösse des Waldes in de x kordinate 
        int ym = 12;            //Die Maximal grösse des Waldes in de Y kordinate 
        int Y ;              //dise Variablen werden später noch verwendent           
        int X ;
        int[] Chunck_position = new int[4];
        int[,] Bäume = new int[xm, ym];                                                                     // nur positnin steinfelck 
        if (!voll)
        {
            int Zonepos = System.Convert.ToInt32((((Map.Map_grösse - 1) / 2) - ZONE_MAX) * Map.chunck_grösse) - Map.chunck_grösse / 2; // hier brechne ich wie vile zone es gesamt gibt und schau wo mein moentane zone im arra liegt 
            int Zonepos_Max = ((3 + (2 * (ZONE_MAX - 1))) * Map.chunck_grösse);
            int Zonepos_Min = ((3 + (2 * (ZONE - 1))) * Map.chunck_grösse);
            if (ZONE == 0)
            {
                Zonepos_Min = 20;
            }
            if (Zonepos <= 0)
            {
                print("Fehler: Map zu klein um wald zu spanen");
                Zonepos_Min = 9;
            }
            int max = 25;
            int min = 14;
            voll = false;
            int[] Grösse = new int[5];

            if (!voll)
            {
                for (int z = 0; z < anzahl; z++)
                {
                    generieren(min, max, Bäume);
                    Grösse = größe_feststellen(Bäume);

                    bool passt;
                    int Kor = 0;
                    int zähler = 0;
                    do
                    {
                        passt = true;

                        Y = Random.Range(-Grösse[3] + 1, 1 + Zonepos_Max - Grösse[1]);      // 3 weil die erste zone 3 Chunks und für jede zuone die dazu kommt zwei weter chunkcs
                        X = Random.Range(-Grösse[2] + 1, 1 + Zonepos_Max - Grösse[0]);

                        for (int i = 0; i < Bäume.GetLength(0); i++)
                        {
                            for (int r = 0; r < Bäume.GetLength(1); r++)
                            {
                                if (Bäume[i, r] != 0)
                                {

                                    if (Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] != 00100000000)
                                    {

                                        passt = false;
                                        zähler++;
                                    }

                                    double XK = i - 1 + X - (Zonepos_Max) / 2;
                                    double YK = r - 1 + Y - (Zonepos_Max) / 2;
                                    double Radius = (Zonepos_Max / 2);
                                    double Radius_min = (Zonepos_Min / 2);
                                    if (ZONE > 1)
                                    {
                                        Radius = (Zonepos_Max / 2) + 1;
                                    }
                                    if (ZONE_MAX > 1)
                                    {
                                        Radius_min = (Zonepos_Min / 2) + 1;
                                    }
                                    double F = XK * XK + YK * YK - Radius * Radius;
                                    double Fmin = XK * XK + YK * YK - Radius_min * Radius_min;
                                    if (F > 0)
                                    {
                                        zähler++;
                                        passt = false;
                                    }
                                    if (Fmin < 0)
                                    {
                                        zähler++;
                                        passt = false;
                                    }

                                }
                            }
                        }
                        if (zähler >= 600)
                        {
                            Kor++;
                            element_rücksetzen(Bäume);
                            generieren(min - Kor, max - Kor, Bäume);
                            Grösse = größe_feststellen(Bäume);
                            zähler = 0;
                        }
                    } while (passt == false && voll == false);

                    for (int i = 0; i < Bäume.GetLength(0) && !voll; i++)
                    {
                        for (int r = 0; r < Bäume.GetLength(1); r++)
                        {
                            if (Bäume[i, r] != 0)
                            {

                                Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] = 00102000000; //200 weil essicher ein walt wird
                                Map.Map_Rohstoffe_Boden[i + Zonepos + X, r + Zonepos + Y] = 00102000000;
                            }
                        }
                    }
                    //++++ ins grid setzen 

                }

            }
        }
    }
    public void Baum(Grid_opjekt Objekt, bool Random)
    {
        random = Random;
        if (random == true)
        {
            Objekt.Index_Top = Wälder.GetRandom();
            Objekt.Set_Rotation_Random(false);
        }
        Objekt.Rohstoff = Wälder.Get(Objekt.Index_Top);


    }
    #endregion
}
