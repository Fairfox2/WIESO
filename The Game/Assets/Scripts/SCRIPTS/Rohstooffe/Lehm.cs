using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lehm : Rohstoffe
{
    public static Lehm singleton { set; get; }
    bool random = true;
    // transform liesten 
    public WeightedRandomList<Transform> Lehm_ecke;
    public WeightedRandomList<Transform> Lehm_rand;
    public WeightedRandomList<Transform> Lehm_mitte;
    public WeightedRandomList<Transform> Lehm_kurve;
    public WeightedRandomList<Transform> Lehm_bäume;
    public WeightedRandomList<Transform> Lehm_boden;
    public WeightedRandomList<Transform> Lehm_wiese;
    //VAriablen 
    bool voll = false;

    private void Awake()
    {
        singleton = this;
    }
    #region Lehm
    public void Lehm_Biom(int Radius_des_Biomes, double Zone, int toleranz)
    {
        int Radius1 = Radius_des_Biomes;// der radius von dem Biom 
        double Zone_Spawn = Zone; // in welcher zone es spanen kann 
        int Biompos = System.Convert.ToInt32((((Map.Map_grösse - 1) / 2) - Zone_Spawn) * Map.chunck_grösse) - 1; // hier brechne ich wie vile zone es gesamt gibt und schau wo mein moentane zone im arra liegt 
        bool passt;
        int X_sp;
        int Y_sp;
        if (Zone_Spawn * Map.chunck_grösse + 8 < Radius1)  // sicher heit 
        {
            return;
        }
        do
        { 
            passt = true;
            int Zonepos_Max_spwan = System.Convert.ToInt32((3 + (2 * (Zone_Spawn - 1))) * Map.chunck_grösse) - 1; // hier wirde der eusere ring brehnent 

            Y_sp = Random.Range(0, Zonepos_Max_spwan); // X und y postiont random berehnen 
            X_sp = Random.Range(0, Zonepos_Max_spwan);

            double XK = X_sp - (Zonepos_Max_spwan) / 2; // wie weit der bunkt von de mitte entpfernt ist
            double YK = Y_sp - (Zonepos_Max_spwan) / 2;
            double Radius = Zonepos_Max_spwan / 2;      // radius berechnen

            if (Zone_Spawn > 1)                 
            {
                Radius = (Zonepos_Max_spwan / 2) + 1;
            }

            double Radius_min = Radius + toleranz;  // minus  ist die toleranz  darf nie null sein rad 
            double Fmin = XK * XK + YK * YK - Radius * Radius;          // inner kreis berechnent
            double F = XK * XK + YK * YK - Radius_min * Radius_min;     // eusserer 
            if (F > 0)   // fasle es über dem eusserem kreis ist 
            {
                passt = false;
            }
            if (Fmin < 0)   // fasle es in dem inneren kreis ist  
            {
                passt = false;
            }


        } while (passt == false && voll == false);
        // daten speichern 
        Map.Biom[0, 0] = X_sp + Biompos - Radius1;
        Map.Biom[0, 1] = Y_sp + Biompos - Radius1;
        Map.Biom[0, 2] = Radius1;
        // daten ins arra einspeichern 
        for (int x = 0; x < Radius1 * 2; x++)
        {
            for (int y = 0; y < Radius1 * 2; y++)
            {

                int XK = x - Radius1;
                int YK = y - Radius1;


                double F = XK * XK + YK * YK - Radius1 * Radius1;
                if (F < 0)
                {
                    if (Map.Map_Rohstoffe[X_sp + Biompos + YK, Y_sp + Biompos + XK] < 2000)
                    {
                        Map.Map_Rohstoffe[X_sp + Biompos + YK, Y_sp + Biompos + XK] = 2000;
                    }
                }


            }
        }
    }
    public void Lehm_setzen(long[,] Lehm_block, Grid_opjekt gr, int X, int Y,bool Random)
    {
        random = Random;
        int q = X;
        int e = Y;
        //   ?
        if (Lehm_block[q, e] == 2400)   // ? # ?
        {                       //   ?


            //    ?
            if (Lehm_block[q - 1, e] == 2400) // '# # ?
            {                     //    ?

                //    ?
                if (Lehm_block[q + 1, e] == 2400)   //  # # '#
                {                           //    ?

                    //   ?
                    if (Lehm_block[q, e - 1] == 2400)   // # # #
                    {                               //  '#

                        //  '#
                        if (Lehm_block[q, e + 1] == 2400)      // # # #
                        {                                   //   #

                            if (Lehm_block[q + 1, e - 1] != 2400)
                            {

                                Lehm_Kurve(gr, 0);
                            }
                            else if (Lehm_block[q - 1, e + 1] != 2400)
                            {

                                Lehm_Kurve(gr, 180);
                            }
                            else if (Lehm_block[q - 1, e - 1] != 2400)
                            {

                                Lehm_Kurve(gr, 90);
                            }
                            else if (Lehm_block[q + 1, e + 1] != 2400)
                            {
                                Lehm_Kurve(gr, 270);
                            }
                            else
                            {
                                Lehm_Mitte(gr);
                            }

                        }
                        else
                        {
                            Lehm_Rand(gr, 270);
                        }
                    }
                    else if (Lehm_block[q, e + 1] == 2400)
                    {
                        Lehm_Rand(gr, 90);
                    }
                    else
                    {
                        Debug.Log("Fehler: Lehmprefab gib es nicht");
                    }


                }                               //    ?
                else if (Lehm_block[q, e - 1] == 2400)  //  # # -
                {                               //   '#

                    //  '#
                    if (Lehm_block[q, e + 1] == 2400)   // # # -
                    {                                 //   #
                        Lehm_Rand(gr, 0);
                    }
                    else
                    {
                        Lehm_Ecke(gr, 180);
                    }

                }

                //  '#
                else if (Lehm_block[q, e + 1] == 2400)  // # # -
                {                               //   -
                    Lehm_Ecke(gr, 270);
                }

                else
                {
                    Debug.Log("Fehler: Lehmprefab gib es nicht");
                }


            }                          //   ?
            else if (Lehm_block[q + 1, e] == 2400) // - # '#
            {                          //   ?

                //   ?
                if (Lehm_block[q, e - 1] == 2400)          // - # #
                {                                       //  '#

                    //  '#
                    if (Lehm_block[q, e + 1] == 2400)      // - # #
                    {                                   //   #
                        Lehm_Rand(gr, 180);
                    }
                    else
                    {
                        Lehm_Ecke(gr, 90);
                    }
                }                                       //  '#
                else if (Lehm_block[q, e + 1] == 2400)     // - # #
                {                                       //   -
                    Lehm_Ecke(gr, 0);
                }
                else
                {
                    Lehm_Rand(gr, 90);
                }
            }                                //   ?
            else if (Lehm_block[q, e - 1] == 2400)   // - # -
            {                                //  '#

                //  '#
                if (Lehm_block[q, e + 1] == 2400)   // - # -
                {                           //   #
                    Debug.Log("Fehler: Lehmprefab gib es nicht");
                }
                else
                {
                    Debug.Log("Fehler: Lehmprefab gib es nicht");
                }

            }
            else if (Lehm_block[q, e + 1] == 2400)
            {
                Debug.Log("Fehler: Lehmprefab gib es nicht");
            }
            gr.value = 1;

        }
    }
    public void Lehm_Genarator(int anzahl, int Radius_Biom, double Zone, int toleranz)
    {
        bool voll = false;

        int[] Chunck_position = new int[4];
        int xm = 42;
        int ym = 42;
        int Y;
        int X ;

        bool passt;
        int[,] Bäume = new int[xm, ym];   
        
        Lehm_Biom(Radius_Biom, Zone, toleranz); // lehm biom erstellen
        if (!voll)
        {

            int max = 160;
            int min = 123;
            voll = false;
            int[] Grösse = new int[5];

            if (!voll)
            {
                for (int w = 0; w < anzahl && !voll; w++)
                {

                    generieren_spezial(min, max, Bäume);
                    Grösse = größe_feststellen(Bäume);

                    
                    int Kor = 0;
                    int X_sp = Map.Biom[0, 0];
                    int Y_sp = Map.Biom[0, 1];
                    int zähler = 0;
                    // spwanpunkt brechnen

                    do
                    {
                        passt = true;

                        Y = Random.Range(-Grösse[3] + 1, 2 + Map.Biom[0, 2] * 2 - Grösse[1]);      // 3 weil die erste zone 3 Chunks und für jede zuone die dazu kommt zwei weter chunkcs
                        X = Random.Range(-Grösse[2] + 1, 2 + Map.Biom[0, 2] * 2 - Grösse[0]);      // die position in einem das so groS ist wie das gewünste feld
                        // schauen ob position passt
                        for (int i = 0; i < Bäume.GetLength(0); i++)
                        {
                            for (int r = 0; r < Bäume.GetLength(1); r++)
                            {
                                if (Bäume[i, r] != 0)
                                {
                                    if (Map.Map_Rohstoffe[i + X + X_sp, r + Y + Y_sp] < 2000)
                                    {
                                        zähler++;
                                        passt = false;

                                    }
                                }
                            }
                        }
                        if (zähler >= 600) // falls es 600 nicht ging wir die fläche verkleinert 
                        {
                            Kor++;
                            element_rücksetzen(Bäume);
                            generieren_spezial(min - Kor, max - Kor, Bäume);
                            Grösse = größe_feststellen(Bäume);
                            zähler = 0;
                        }
                    } while (passt == false && voll == false);
                    // in array setzen 
                    for (int i = 0; i < Bäume.GetLength(0) && !voll; i++)
                    {
                        for (int r = 0; r < Bäume.GetLength(1); r++)
                        {
                            if (Bäume[i, r] != 0)
                            {
                                Map.Map_Rohstoffe[i + X + X_sp, r + Y + Y_sp] = 2400;
                            }
                        }
                    }
                }
                for (int i = 0; i <= Radius_Biom * 2; i++)
                {
                    for (int t = 0; t <= Radius_Biom * 2; t++)
                    {
                        /*
                        if (BiomVereinfacher(i + Map.Biom[0, 0], t + Map.Biom[0, 1], 2000, 2999, 3, 3)) // hier vereinfachen wir das biom
                        {
                            Map.Map_Rohstoffe[i + Map.Biom[0, 0], t + Map.Biom[0, 1]] = 1000;
                        }
                        */
                    }
                }
                Lehm_Bäume(4); // hier im nachinein die bäue platzieren das tuen wir nach der vereinfachung 
            }
        }
    }

    public void Lehm_Bäume(int anzahl)
    {
        voll = false;

        int[] Chunck_position = new int[4];
        int xm = 42;
        int ym = 42;
        int Y;
        int X;

        bool passt;
        int[,] Bäume = new int[xm, ym];                                                                     // nur positnin steinfelck 
                                                                                                            // Alle Steine in diesem Chunck  


        if (!voll)
        {
            int max = 1;
            int min = 1;
            bool voll = false;
            int[] Grösse = new int[5];

            if (!voll)
            {

                for (int w = 0; w < anzahl && !voll; w++)
                {
                    generieren(min, max, Bäume);
                    Grösse = größe_feststellen(Bäume);
                    
                    int Kor = 0;
                    int zähler = 0;
                    // spwanpunkt brechnen
                    int X_sp = Map.Biom[0, 0];
                    int Y_sp = Map.Biom[0, 1];
                    do
                    {
                        passt = true;

                        Y = Random.Range(-Grösse[3] + 1, 2 + Map.Biom[0, 2] * 2 - Grösse[1]);      // 3 weil die erste zone 3 Chunks und für jede zuone die dazu kommt zwei weter chunkcs
                        X = Random.Range(-Grösse[2] + 1, 2 + Map.Biom[0, 2] * 2 - Grösse[0]);      // die position in einem das so groS ist wie das gewünste feld

                        for (int i = 0; i < Bäume.GetLength(0); i++)
                        {
                            for (int r = 0; r < Bäume.GetLength(1); r++)
                            {
                                if (Bäume[i, r] != 0)
                                {

                                    if (Map.Map_Rohstoffe[i + X + X_sp, r + Y + Y_sp] != 2000)
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
                                Map.Map_Rohstoffe[i + X + X_sp, r + Y + Y_sp] = 2100;
                            }

                        }
                    }
                    //++++ ins grid setzen 
                }

            }
        }
    }

    #region Lehm_prefabs
    public void Lehm_Baum(Grid_opjekt Objekt,bool Random)
    {
        random = Random;
        if(random == true)
        {
            Objekt.Index_Top = Lehm_bäume.GetRandom();
            Objekt.Set_Rotation_Random();
        }
        Objekt.Rohstoff = Lehm_bäume.Get(Objekt.Index_Top);

        Objekt.boden_Rohstoff = true;
    }
    public void Lehm_Boden(Grid_opjekt Objekt)
    {
        if (random == true)
        {
            Objekt.Index_Top = Lehm_boden.GetRandom();
            Objekt.Set_Rotation_Random();
        }
        Objekt.Boden = Lehm_boden.Get(Objekt.Index_Top);

    }
    private void Lehm_Ecke(Grid_opjekt Objekt, int winkel)
    {
        if (random == true)
        {
            Objekt.Index_Top = Lehm_ecke.GetRandom();
        }
        Objekt.Rohstoff = Lehm_ecke.Get(Objekt.Index_Top);
        Objekt.Setrotation(winkel);
        Objekt.boden_Rohstoff = true;
    }
    private void Lehm_Mitte(Grid_opjekt Objekt)
    {
        if (random == true)
        {
            Objekt.Index_Top = Lehm_mitte.GetRandom();
            Objekt.Set_Rotation_Random();
        }
        Objekt.Rohstoff = Lehm_mitte.Get(Objekt.Index_Top);
        Objekt.boden_Rohstoff = true;
    }
    private void Lehm_Rand(Grid_opjekt Objekt, int winkel)
    {
        if (random == true)
        {
            Objekt.Index_Top = Lehm_rand.GetRandom();
        }
        Objekt.Rohstoff = Lehm_rand.Get(Objekt.Index_Top);
        Objekt.Setrotation(winkel);
        Objekt.boden_Rohstoff = true;
    }
    private void Lehm_Kurve(Grid_opjekt Objekt, int winkel)
    {
        if (random == true)
        {
            Objekt.Index_Top = Lehm_kurve.GetRandom();
        }
        Objekt.Rohstoff = Lehm_kurve.Get(Objekt.Index_Top);
        Objekt.Setrotation(winkel);
        Objekt.boden_Rohstoff = true;
    }

    #endregion
    #endregion
}
