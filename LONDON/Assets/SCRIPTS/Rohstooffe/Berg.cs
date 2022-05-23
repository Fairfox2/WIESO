using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berg : Rohstoffe 
{
    public static Berg singleton { set; get; }


    public WeightedRandomList<Transform> Stein_ecke;
    public WeightedRandomList<Transform> Stein_ecke_höche;
    public WeightedRandomList<Transform> Stein_rand;
    public WeightedRandomList<Transform> Stein_rand_höche;
    public WeightedRandomList<Transform> Stein_mitte;
    public WeightedRandomList<Transform> Stein_Lose;
    public WeightedRandomList<Transform> Stein_kurve;
    public WeightedRandomList<Transform> Stein_spitze;
    public WeightedRandomList<Transform> Stein_brücke;
    public WeightedRandomList<Transform> Stein_boden;
    public WeightedRandomList<Transform> Berg_wiese;

    public void Awake()
    {
        singleton = this;
    }
    public void Berg_Boden(Grid_opjekt Objekt)
    {
        Objekt.Boden = Berg_wiese.GetRandom();
    }
    public void Stein_Genarator(double ZONE, int ZONE_MAX, int anzahl)
    {
        // man könnte noch die postionen vonn allen bergen speichern

        bool voll = false;

        int[] Chunck_position = new int[4];
        int xm = 40;
        int ym = 40;

        int[,] Steine = new int[xm, ym];                                                                     // nur positnin steinfelck 
        int[,] Wald_block = new int[40 + 2, 40 + 2];      // Alle Steine in diesem Chunck  

        int X;
        int Y;

        if (!voll)
        {
            int Zonepos = System.Convert.ToInt32((((Map.Map_grösse - 1) / 2) - ZONE_MAX) * Map.chunck_grösse) - Map.chunck_grösse / 2; // hier brechne ich wie vile zone es gesamt gibt und schau wo mein moentane zone im arra liegt 
            int Zonepos_Max = ((3 + (2 * (ZONE_MAX - 1))) * Map.chunck_grösse);
            int Zonepos_Min = System.Convert.ToInt32((3 + (2 * (ZONE - 1))) * Map.chunck_grösse);
            if (ZONE == 0)
            {
                Zonepos_Min = 20;
            }
            if (Zonepos <= 0)
            {
                print("Fehler: Map zu klein um wald zu spanen");
                Zonepos_Min = 9;
            }
            int max = 160;
            int min = 100;
            voll = false;
            int[] Grösse = new int[5];

            if (!voll)
            {
                for (int z = 0; z < anzahl; z++)
                {
                    Rohstoffe.singleton.generieren_spezial(min, max, Steine);
                    Grösse = Rohstoffe.singleton.größe_feststellen(Steine);

                    bool passt = false;
                    int Kor = 0;
                    int zähler = 0;
                    do
                    {
                        passt = true;

                        Y = Random.Range(-Grösse[3] + 1, 1 + Zonepos_Max - Grösse[1]);      // 3 weil die erste zone 3 Chunks und für jede zuone die dazu kommt zwei weter chunkcs
                        X = Random.Range(-Grösse[2] + 1, 1 + Zonepos_Max - Grösse[0]);

                        for (int i = 0; i < Steine.GetLength(0); i++)
                        {
                            for (int r = 0; r < Steine.GetLength(1); r++)
                            {
                                if (Steine[i, r] != 0)
                                {

                                    if (Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] > 1001)
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
                            Rohstoffe.singleton.element_rücksetzen(Steine);
                            Rohstoffe.singleton.generieren_spezial(min - Kor, max - Kor, Steine);
                            Grösse = Rohstoffe.singleton.größe_feststellen(Steine);
                            zähler = 0;
                        }
                    } while (passt == false && voll == false);

                    for (int i = 0; i < Steine.GetLength(0) && !voll; i++)
                    {
                        for (int r = 0; r < Steine.GetLength(1); r++)
                        {
                            if (Steine[i, r] != 0)
                            {

                                Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] = 300; //200 weil essicher ein walt wird
                            }
                        }
                    }
                    for (int i = 0; i < Steine.GetLength(0) && !voll; i++) // zusätzliche recgel damit keine komichen löcher entstehen
                    {
                        for (int r = 0; r < Steine.GetLength(1); r++)
                        {
                            if (Rohstoffe.singleton.BiomVereinfacher(i + Zonepos + X, r + Zonepos + Y, 299, 399, 1, 7) == false)
                            {
                                if (Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] == 1000 || Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] == 0)
                                    Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] = 300;
                                Steine[i, r] = 1;
                            }


                        }
                    }


                    for (int i = 0; i < Steine.GetLength(0) + 4 && !voll; i++) // in einem radius von 2 blöcken wird der boden random verändert
                    {
                        for (int r = 0; r < Steine.GetLength(1) + 4; r++)
                        {
                            if (Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] == 1000 || Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] == 0)
                            {
                                if (BiomVereinfacher(i + Zonepos + X, r + Zonepos + Y, 299, 399, 2, 3) == false)
                                    Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] = 1050;

                            }

                        }
                    }
                    for (int b = 0; b < 100; b++)   // hir verwenden wir eine for schleife mit fix hundert schritten wenn der berg mehr wie hundert ebene aht blöd keine while schleife aus sicher heits gründe
                    {
                        for (int i = 0; i < Steine.GetLength(0) && !voll; i++)
                        {
                            for (int r = 0; r < Steine.GetLength(1); r++)
                            {
                                if (Rohstoffe.singleton.BiomVereinfacher(i + Zonepos + X, r + Zonepos + Y, 299 + b, 350, 1, 9) == false) //hier werden dier ebenen bestimmt 
                                {
                                    Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] = 301 + b;
                                }
                            }
                        }
                    }
                    //++++ ins grid setzen 

                }

            }
        }
    }
    public void Berg_setzen(int[,] Stein_block, Grid_opjekt gr, int X, int Y)
    {

        //   ?
        int q = X;
        int e = Y;
        int ebene = gr.Rohstoffe_ID - 300;
        if (Stein_block[q, e] == 300 + ebene)   // ? # ?
        {                           //   ?

            //    ?
            if (Stein_block[q - 1, e] >= 300 + ebene && Stein_block[q - 1, e] < 400) // '# # ?
            {                     //    ?

                //    ?
                if (Stein_block[q + 1, e] >= 300 + ebene && Stein_block[q + 1, e] < 400)   //  # # '#
                {                           //    ?

                    //   ?
                    if (Stein_block[q, e - 1] >= 300 + ebene && Stein_block[q, e - 1] < 400)   // # # #
                    {                           //  '#

                        //  '
                        if (Stein_block[q, e + 1] >= 300 + ebene && Stein_block[q, e + 1] < 400)   // # # #
                        {


                            if (Stein_block[q + 1, e + 1] >= 300 + ebene && Stein_block[q + 1, e + 1] < 400)
                            {
                                if (Stein_block[q - 1, e + 1] >= 300 + ebene && Stein_block[q - 1, e + 1] < 400)
                                {
                                    if (Stein_block[q + 1, e - 1] >= 300 + ebene && Stein_block[q + 1, e - 1] < 400)
                                    {
                                        if (Stein_block[q - 1, e - 1] >= 300 + ebene && Stein_block[q - 1, e - 1] < 400)
                                        {
                                            Stein_Mitte(gr);
                                        
                                        }
                                        else
                                        {
                                            Stein_Kurve(gr, 0);
                                        }
                                         
                                    }
                                    else
                                    {
                                        Stein_Kurve(gr, 270);
                                    }

                                }
                                else
                                {
                                    Stein_Kurve(gr, 90);
                                }
                            }
                            else
                            {
                                Stein_Kurve(gr, 180);
                            }

                        }
                        else
                        {
                            if (Stein_block[q - 1, e - 1] >= 300 + ebene && Stein_block[q - 1, e - 1] < 400 || ebene == 0)
                            {
                                if (Stein_block[q + 1, e - 1] >= 300 + ebene && Stein_block[q + 1, e - 1] < 400 || ebene == 0)
                                {
                                    
                                        Stein_Rand(gr, 180, ebene);
                                    
                                }
                                else
                                {

                                    Stein_Ecke(gr, 270, ebene); //
                                }
                            }
                            else
                            {

                                Stein_Ecke(gr, 180, ebene);    //
                            }
                        }
                    }
                    else if (Stein_block[q, e + 1] >= 300 + ebene && Stein_block[q, e + 1] < 400)
                    {
                        if (Stein_block[q - 1, e + 1] >= 300 + ebene && Stein_block[q - 1, e + 1] < 400 || ebene == 0)
                        {
                            if (Stein_block[q + 1, e + 1] >= 300 + ebene && Stein_block[q + 1, e + 1] < 400 || ebene == 0)
                            {
                             
                                    Stein_Rand(gr, 0, ebene);
                                 
                            }
                            else
                            {

                                Stein_Ecke(gr, 0, ebene); //
                            }
                        }
                        else
                        {

                            Stein_Ecke(gr, 90, ebene); //
                        }
                    }
                    else if (ebene < 1)  // nicht mehr in eben 1 und drüber 
                    {
                        Stein_Brücke(gr, 180);
                    }
                    else
                    {
                        Stein_Mitte(gr);
                    }


                }                               //    ?
                else if (Stein_block[q, e - 1] >= 300 + ebene && Stein_block[q, e - 1] < 400)  //  # # -
                {                               //   '#

                    //  '#
                    if (Stein_block[q, e + 1] >= 300 + ebene && Stein_block[q, e + 1] < 400)     // # # -
                    {                                                                           //   #
                        if (Stein_block[q - 1, e - 1] >= 300 + ebene && Stein_block[q - 1, e - 1] < 400 || ebene == 0)
                        {
                            if (Stein_block[q - 1, e + 1] >= 300 + ebene && Stein_block[q - 1, e + 1] < 400 || ebene == 0)
                            {
                                 Stein_Rand(gr, 270, ebene);
                                
                            }
                            else
                            {

                                Stein_Ecke(gr, 270, ebene);
                            }
                        }
                        else
                        {

                            Stein_Ecke(gr, 0, ebene);
                        }
                    }
                    else
                    {
                        Stein_Ecke(gr, 270, ebene);
                    }

                }

                //  '#
                else if (Stein_block[q, e + 1] >= 300 + ebene && Stein_block[q, e + 1] < 400)  // # # -
                {                                                                             //   -
                    Stein_Ecke(gr, 0, ebene);
                }

                else if (ebene < 1) // nicht mehr in eben 1 und drüber 
                {
                    Stein_lose(gr, 180);

                }
                else
                {
                    Stein_Mitte(gr);
                }


            }                          //   ?
            else if (Stein_block[q + 1, e] >= 300 + ebene && Stein_block[q + 1, e] < 400) // - # '#
            {                          //   ?

                //   ?
                if (Stein_block[q, e - 1] >= 300 + ebene && Stein_block[q, e - 1] < 400)   // - # #
                {                           //  '#

                    //  '#
                    if (Stein_block[q, e + 1] >= 300 + ebene && Stein_block[q, e + 1] < 400)     // - # #
                    {                                   //   #
                        if (Stein_block[q + 1, e + 1] >= 300 + ebene && Stein_block[q + 1, e + 1] < 400 || ebene == 0)
                        {
                            if (Stein_block[q + 1, e - 1] >= 300 + ebene && Stein_block[q + 1, e - 1] < 400)
                            {
                            
                                    Stein_Rand(gr, 90, ebene);
                                
                            }
                            else
                            {

                                Stein_Ecke(gr, 90, ebene);
                            }
                        }
                        else
                        {

                            Stein_Ecke(gr, 00, ebene);
                        }
                    }
                    else
                    {
                        Stein_Ecke(gr, 180, ebene);
                    }
                }                                       //  '#
                else if (Stein_block[q, e + 1] >= 300 + ebene && Stein_block[q, e + 1] < 400)    // - # #
                {                                       //   -
                    Stein_Ecke(gr, 90, ebene);
                }
                else if (ebene < 1) // nicht mehr in eben 1 und drüber 
                {
                    Stein_lose(gr, 0);
                }
                else
                {
                    Stein_Mitte(gr);
                }
            }                                //   ?
            else if (Stein_block[q, e - 1] >= 300 + ebene && Stein_block[q, e - 1] < 400)    // - # -
            {                                //  '#

                //  '#
                if (Stein_block[q, e + 1] >= 300 + ebene && Stein_block[q, e + 1] < 400 && ebene < 1)   // - # -
                {                           //   #
                    Stein_Brücke(gr, 180);
                }
                else if (ebene < 1) // nicht mehr in eben 1 und drüber 
                {
                    Stein_lose(gr, 90);
                }
                else
                {
                    Stein_Mitte(gr);
                }

            }
            else if (Stein_block[q, e + 1] >= 300 + ebene && Stein_block[q, e + 1] < 400)
            {

                if (ebene > 0)
                {
                    Stein_Spitze(gr);
                }
                else
                {
                    if (ebene > 0)
                    {
                        Stein_Mitte(gr);
                    }
                    else
                    {
                        Stein_lose(gr, 270);

                    }
                }
            }
            else { Stein_Spitze(gr); }

        }
        gr.value += ebene * 1.3f;
      
        if (ebene > 0)
        {
            gr.Boden = Stein_boden.GetRandom();
        }
        else
        {
            Berg_Boden(gr);
        }
    }
    #region Stein_prefabs

    private void Stein_Ecke(Grid_opjekt Objekt, int winkel, int ebene)
    {
        Transform element = Stein_ecke_höche.GetRandom(); ;
        if (ebene <= 0)
        {
            element = Stein_ecke.GetRandom();
        }
        Objekt.Rohstoff = element;
        Objekt.Setrotation(winkel);
    }
    private void Stein_lose(Grid_opjekt Objekt, int winkel)
    {
        Transform element = Stein_Lose.GetRandom(); 
        Objekt.Rohstoff = element;
        Objekt.Setrotation(winkel);
    }
    private void Stein_Brücke(Grid_opjekt Objekt, int winkel)
    {
        Transform element = Stein_Lose.GetRandom();
        Objekt.Rohstoff = element;
        Objekt.Setrotation(winkel);
    }
    private void Stein_Kurve(Grid_opjekt Objekt, int winkel)
    {
        Objekt.Rohstoff = Stein_kurve.GetRandom();
        Objekt.Setrotation(winkel);
    }

    private void Stein_Spitze(Grid_opjekt Objekt)
    {
        Objekt.Rohstoff = Stein_spitze.GetRandom();
        Objekt.Set_Rotation_Random();
    }
    private void Stein_Mitte(Grid_opjekt Objekt)
    { 
        Objekt.Rohstoff = Stein_mitte.GetRandom();
        Objekt.Set_Rotation_Random();
    }
    private void Stein_Rand(Grid_opjekt Objekt, int winkel,int ebene)
    {
        Transform element = Stein_rand_höche.GetRandom(); 
        if (ebene <= 0)
        {
            element = Stein_rand.GetRandom();
        }
        Objekt.Rohstoff = element;
        Objekt.Setrotation(winkel);
    }
 


    #endregion
}
