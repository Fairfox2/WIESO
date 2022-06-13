using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berg : Rohstoffe 
{
    public static Berg singleton { set; get; }

    public WeightedRandomList<Transform> Stein_ecke;
    public WeightedRandomList<Transform> Stein_ecke_h�che;
    public WeightedRandomList<Transform> Stein_rand;
    public WeightedRandomList<Transform> Stein_rand_h�che;
    public WeightedRandomList<Transform> Stein_mitte;
    public WeightedRandomList<Transform> Stein_Lose;
    public WeightedRandomList<Transform> Stein_kurve;
    public WeightedRandomList<Transform> Stein_kurve_Komisch;
    public WeightedRandomList<Transform> Stein_Komische_Ecke;
    public WeightedRandomList<Transform> Stein_spitze;
    public WeightedRandomList<Transform> Stein_br�cke;
    public WeightedRandomList<Transform> Stein_boden;
    public WeightedRandomList<Transform> Berg_wiese;

    bool random = true;
    public void Awake()
    {
        singleton = this;
    }
    public void Berg_Boden(Grid_opjekt Objekt, bool Random)
    {
        random = Random;
        if(random == true)
        {
            Objekt.Index_Boden = Berg_wiese.GetRandom();
        }
        Objekt.Boden = Berg_wiese.Get(Objekt.Index_Boden);
    }
    public void Stein_Genarator(double ZONE, int ZONE_MAX, int anzahl)
    {
        // man k�nnte noch die postionen vonn allen bergen speichern

        bool voll = false;

        int[] Chunck_position = new int[4];
        int xm = 40;
        int ym = 40;

        int[,] Steine = new int[xm, ym];                                                                     // nur positnin steinfelck 
              // Alle Steine in diesem Chunck  

        int X;
        int Y;

        if (!voll)
        {
            int Zonepos = System.Convert.ToInt32((((Map.Map_gr�sse - 1) / 2) - ZONE_MAX) * Map.chunck_gr�sse) - Map.chunck_gr�sse / 2; // hier brechne ich wie vile zone es gesamt gibt und schau wo mein moentane zone im arra liegt 
            int Zonepos_Max = ((3 + (2 * (ZONE_MAX - 1))) * Map.chunck_gr�sse);
            int Zonepos_Min = System.Convert.ToInt32((3 + (2 * (ZONE - 1))) * Map.chunck_gr�sse);
            if (ZONE == 0)
            {
                Zonepos_Min = 20;
            }
            if (Zonepos <= 0)
            {
                Zonepos_Min = 9;
            }
            int max = 160;
            int min = 100;
            voll = false;
            int[] Gr�sse = new int[5];

            if (!voll)
            {
                for (int z = 0; z < anzahl; z++)
                {
                    Rohstoffe.singleton.generieren_spezial(min, max, Steine);
                    Gr�sse = Rohstoffe.singleton.gr��e_feststellen(Steine);

                    bool passt = false;
                    int Kor = 0;
                    int z�hler = 0;
                    do
                    {
                        passt = true;

                        Y = Random.Range(-Gr�sse[3] + 1, 1 + Zonepos_Max - Gr�sse[1]);      // 3 weil die erste zone 3 Chunks und f�r jede zuone die dazu kommt zwei weter chunkcs
                        X = Random.Range(-Gr�sse[2] + 1, 1 + Zonepos_Max - Gr�sse[0]);

                        for (int i = 0; i < Steine.GetLength(0); i++)
                        {
                            for (int r = 0; r < Steine.GetLength(1); r++)
                            {
                                if (Steine[i, r] != 0)
                                {

                                    if (Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] != 00001000000 && Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] != 0000000000)
                                    {

                                       // passt = false;
                                        ///z�hler++;
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
                                        z�hler++;
                                        passt = false;
                                    }
                                    if (Fmin < 0)
                                    {
                                        z�hler++;
                                        passt = false;
                                    }

                                }
                            }
                        }
                        if (z�hler >= 600)
                        {
                            Kor++;
                            Rohstoffe.singleton.element_r�cksetzen(Steine);
                            Rohstoffe.singleton.generieren_spezial(min - Kor, max - Kor, Steine);
                            Gr�sse = Rohstoffe.singleton.gr��e_feststellen(Steine);
                            z�hler = 0;
                        }
                    } while (passt == false && voll == false);

                    for (int i = 0; i < Steine.GetLength(0) && !voll; i++)
                    {
                        for (int r = 0; r < Steine.GetLength(1); r++)
                        {
                            if (Steine[i, r] != 0)
                            {
                                print("Berg_wurde_gesetzt");
                                Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] = 00010100000;
                            }
                        }
                    }
                    for (int i = 0; i < Steine.GetLength(0) && !voll; i++) // zus�tzliche recgel damit keine komichen l�cher entstehen
                    {
                        for (int r = 0; r < Steine.GetLength(1); r++)
                        {
                            if (Rohstoffe.singleton.BiomVereinfacher(Map.Map_Rohstoffe,i + Zonepos + X, r + Zonepos + Y, 00010100000, 1, 7,100) == false)
                            {
                                //if (Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] == 1000 || Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] == 0)
                                  //  Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] = 00010100000;
                                //Steine[i, r] = 1;
                            }


                        }
                    }

                    
                    for (int i = 0; i < Steine.GetLength(0) + 4 && !voll; i++) // in einem radius von 2 bl�cken wird der boden random ver�ndert
                    {
                        for (int r = 0; r < Steine.GetLength(1) + 4; r++)
                        {

                                if (BiomVereinfacher(Map.Map_Rohstoffe,i + Zonepos + X, r + Zonepos + Y, 00010100000, 2, 3,100) == false)
                                {
                                   ////Map.Map_Rohstoffe_Boden[i + Zonepos + X, r + Zonepos + Y] = 00010100000;
                                }


                            

                        }
                    }
                    
                    for (int b = 0; b < 100; b++)   // hir verwenden wir eine for schleife mit fix hundert schritten wenn der berg mehr wie hundert ebene aht bl�d keine while schleife aus sicher heits gr�nde
                    {
                        for (int i = 0; i < Steine.GetLength(0) && !voll; i++)
                        {
                            for (int r = 0; r < Steine.GetLength(1); r++)
                            {

                                if (Rohstoffe.singleton.BiomVereinfacher(Map.Map_Rohstoffe,i + Zonepos + X, r + Zonepos + Y, 10100000 + b, 1, 8,1) == false) //hier werden dier ebenen bestimmt 
                                {
                                    
                                    Map.Map_Rohstoffe[i + Zonepos + X, r + Zonepos + Y] ++;
                                }
                            }
                        }
                    }
                    
                    //++++ ins grid setzen 

                }

            }
        }
    }
    public void Berg_setzen(long [,] Stein_block, Grid_opjekt gr, int X, int Y,bool Random)
    {
        random = Random;
        //   ?
        int q = X;
        int e = Y;
        int ebene = gr.zusatz_Top ; 

     


        int[,] Umkreis = new int[3,3];
        #region Abtasten

        if (Stein_block[q-1, e - 1] % 100000000 - Stein_block[q - 1, e - 1] % 10000 == 10100000 && Stein_block[q - 1, e - 1] % 100 == gr.zusatz_Top)   // # # #
        {
            Umkreis[0, 0] = 1;
        }
        if (Stein_block[q - 1, e ] % 100000000 - Stein_block[q - 1, e ] % 10000 == 10100000 && Stein_block[q - 1, e - 1] % 100 == gr.zusatz_Top)    // # # #
        {
            Umkreis[0, 1] = 1;
        }
        if (Stein_block[q - 1, e+1] % 100000000 - Stein_block[q - 1, e+1] % 10000 == 10100000 && Stein_block[q - 1, e - 1] % 100 == gr.zusatz_Top)    // # # #
        {
            Umkreis[0, 2] = 1;
        }
        if (Stein_block[q , e - 1] % 100000000 - Stein_block[q , e - 1] % 10000 == 10100000 && Stein_block[q - 1, e - 1] % 100 == gr.zusatz_Top)   // # # #
        {
            Umkreis[1, 0] = 1;
        }


        if (Stein_block[q, e + 1] % 100000000 - Stein_block[q, e + 1] % 10000 == 10100000 && Stein_block[q - 1, e - 1] % 100 == gr.zusatz_Top)  // # # #
        {
            Umkreis[1, 2] = 1;
        }
        if (Stein_block[q+1, e - 1] % 100000000 - Stein_block[q+1, e - 1] % 10000 == 10100000 && Stein_block[q - 1, e - 1] % 100 == gr.zusatz_Top)   // # # #
        {
            Umkreis[2, 0] = 1;
        }
        if (Stein_block[q + 1, e ] % 100000000 - Stein_block[q + 1, e] % 10000 == 10100000 && Stein_block[q - 1, e - 1] % 100 == gr.zusatz_Top)   // # # #
        {
            Umkreis[2, 1] = 1;
        }
        if (Stein_block[q + 1, e+1] % 100000000 - Stein_block[q + 1, e+1] % 10000 == 00010100000 && Stein_block[q - 1, e - 1] % 100 == gr.zusatz_Top)   // # # #
        {
            Umkreis[2, 2] = 1;
        }
        #endregion
        //hier bild ich die summe 
        int Summe = Umkreis[1, 0] + Umkreis[0, 1] + Umkreis[1, 2] + Umkreis[2, 1];
        int Summe2 = 0;
        for (int i = 0; i < Umkreis.GetLength(0); i++)
        {
            for (int f = 0; f < Umkreis.GetLength(1); f++)
            {
                Summe2 += Umkreis[i, f];
            }
        }

        if(Summe == 0 && ebene != 0)
        {
            Stein_Spitze(gr);
        }
        if(Summe == 1)
        {        
            Stein_Mitte(gr);
            
        }
        if(Summe == 2)
        {
         
            //ecken
            if(Umkreis[0, 1] == 1 && Umkreis[1, 0] == 1 && Umkreis[0, 0] == 1)
            {
                Stein_Ecke(gr, 270,ebene);
            }
            else if(Umkreis[0, 1] == 1 && Umkreis[1, 2] == 1 && Umkreis[0, 2] == 1)
            {
                Stein_Ecke(gr, 0, ebene);
            }
            else if(Umkreis[2, 1] == 1 && Umkreis[1, 2] == 1 && Umkreis[2, 2] == 1)
            {
                Stein_Ecke(gr, 90, ebene);
            }
            else if(Umkreis[1, 0] == 1 && Umkreis[2, 1] == 1 && Umkreis[2, 0] == 1)
            {
                Stein_Ecke(gr, 180, ebene);
            }
            else
            {
                Stein_Mitte(gr);
            }

        }
        if(Summe == 3)
        {
            if (Umkreis[2, 1] == 1 && Umkreis[0, 1] == 1 && Umkreis[1, 0] == 1 && Umkreis[1, 2] == 0)
            {
                Stein_Rand(gr, 180, ebene);
                if ((Summe2 == 4 || Summe2 == 5))
                {
                    if (Umkreis[2, 0] == 0)
                    {
                        Stein_Ecke(gr, 270, ebene);
                    }
                    if (Umkreis[0, 0] == 0)
                    {
                        Stein_Ecke(gr, 180, ebene);
                    }
                }
       
            }
            else if (Umkreis[1, 0] == 1 && Umkreis[1, 2] == 1 && Umkreis[2, 1] == 0 && Umkreis[0, 1] == 1)
            {
                Stein_Rand(gr, 270, ebene);
                if ((Summe2 == 4|| Summe2 == 5))
                {
                    if (Umkreis[0, 0] == 0)
                    {
                        Stein_Ecke(gr, 0, ebene);
                    }
                    if (Umkreis[0, 2] == 0)
                    {
                        Stein_Ecke(gr,270, ebene);
                    }
                }
            }
            else if (Umkreis[1, 0] == 0 && Umkreis[1, 2] == 1 && Umkreis[2, 1] == 1 && Umkreis[0, 1] == 1)
            {  
                Stein_Rand(gr, 00, ebene);
                if ( (Summe2 == 4 || Summe2 == 5))
                {
                    if (Umkreis[2, 2] == 0)
                    {
                        Stein_Ecke(gr, 0, ebene);
                    }
                    if(Umkreis[0, 2] == 0)
                    {
                        Stein_Ecke(gr, 90, ebene);
                    }
                }
            }
            
            else if (Umkreis[1, 0] == 1 && Umkreis[1, 2] == 1 && Umkreis[2, 1] == 1 && Umkreis[0, 1] == 0)      
            {
                Stein_Rand(gr, 90, ebene);
                if ((Summe2 == 4 || Summe2 == 5))
                {
                    if (Umkreis[2, 2] == 0)
                    {
                        Stein_Ecke(gr, 180, ebene);
                    }
                    if (Umkreis[0, 2] == 0)
                    {
                        Stein_Ecke(gr, 90, ebene);
                    }
                }
            }

            if (Summe2 == 3)
            {
                Stein_Spitze(gr);
            }

        }
        if(Summe == 4)
        {
            if(Summe2== 8)
            {
                Stein_Mitte(gr);
            }
            else if (Summe2 == 7)
            {
                if (Umkreis[0, 0] == 1 && Umkreis[2, 0] == 1 && Umkreis[2, 2] == 1 && Umkreis[0, 2] == 0)
                {
                    Stein_Kurve(gr, 90);
                }
                if (Umkreis[0, 0] == 1 && Umkreis[2, 0] == 1 && Umkreis[2, 2] == 0 && Umkreis[0, 2] == 1)
                {
                    Stein_Kurve(gr, 180);
                }
                if (Umkreis[0, 0] == 1 && Umkreis[2, 0] == 0 && Umkreis[2, 2] == 1 && Umkreis[0, 2] == 1)
                {
                    Stein_Kurve(gr, 270);
                }
                if (Umkreis[0, 0] == 0 && Umkreis[2, 0] == 1 && Umkreis[2, 2] == 1 && Umkreis[0, 2] == 1)
                {
                    Stein_Kurve(gr, 0);
                }
            }
            else if (Summe2 == 6)
            {

                if (Umkreis[0, 0] == 1 && Umkreis[2, 2] == 1)
                {
                    Stein_Kurve_Komisch(gr, 90);
                }
                else if (Umkreis[0, 2] == 1 && Umkreis[2, 0] == 1)
                {
                    Stein_Kurve_Komisch(gr, 0);
                }
                else if (Umkreis[0, 0] == 1)
                {
                    Stein_Rand(gr, -90, ebene);
                }
                else if (Umkreis[2, 2] == 1)
                {
                    Stein_Rand(gr, 90, ebene);
                }
                else if (Umkreis[0, 2] == 1)
                {
                    Stein_Rand(gr, 10, ebene);
                }
                else if (Umkreis[2, 0] == 1)
                {
                    Stein_Rand(gr, 50, ebene);
                }
            }
            if(Summe2 == 3)
            {
                Stein_Spitze(gr);
            }

        }


        if(random == true) gr.value += ebene * 1.3f; ;
        
      
        if (ebene > 0)
        {
            if(random == true)
            {
                gr.Index_Boden = Stein_boden.GetRandom();
            }
            gr.Boden = Stein_boden.Get(gr.Index_Boden);
        }
        else
        {
            Berg_Boden(gr, random);
        }
        
    }
    #region Stein_prefabs

    private void Stein_Ecke(Grid_opjekt Objekt, int winkel, int ebene)
    {
        if (random == true)
        {
            Objekt.Index_Top = Stein_ecke_h�che.GetRandom();
            if (ebene <= 0)
            {
                Objekt.Index_Top = Stein_ecke.GetRandom();
            }
        }
        Transform element = null; ;

        element = Stein_ecke_h�che.Get(Objekt.Index_Top);
        if (ebene <= 0)
        {
            element = Stein_ecke.Get(Objekt.Index_Top);
        }
        Objekt.Rohstoff = element;
        Objekt.Setrotation(winkel);
    }
    private void Stein_lose(Grid_opjekt Objekt, int winkel)
    {
        if (random == true)
        {
            Objekt.Index_Top = Stein_Lose.GetRandom();
        }
        Objekt.Rohstoff = Stein_Lose.Get(Objekt.Index_Top);
        Objekt.Setrotation(winkel);
    }
    private void Stein_Br�cke(Grid_opjekt Objekt, int winkel)
    {
        if (random == true)
        {
            Objekt.Index_Top = Stein_Lose.GetRandom();
        }
        Objekt.Rohstoff = Stein_Lose.Get(Objekt.Index_Top);
        Objekt.Setrotation(winkel);
    }
    private void Stein_Kurve(Grid_opjekt Objekt, int winkel)
    {
        if (random == true)
        {
            Objekt.Index_Top = Stein_kurve.GetRandom();
        }
        Objekt.Rohstoff = Stein_kurve.Get(Objekt.Index_Top);
        Objekt.Setrotation(winkel);
    }
    private void Stein_Kurve_Komisch(Grid_opjekt Objekt, int winkel)
    {
        if (random == true)
        {
            Objekt.Index_Top = Stein_kurve_Komisch.GetRandom();
        }
        print("kureve ");
        Objekt.Rohstoff = Stein_kurve_Komisch.Get(Objekt.Index_Top);
        Objekt.Setrotation(winkel);
    }

    private void Stein_Spitze(Grid_opjekt Objekt)
    {
        if (random == true)
        {
            Objekt.Index_Top = Stein_spitze.GetRandom();
            Objekt.Set_Rotation_Random();
        }
        Objekt.Rohstoff = Stein_spitze.Get(Objekt.Index_Top);

    }
    private void Stein_Mitte(Grid_opjekt Objekt)
    {
        if (random == true)
        {
            Objekt.Index_Top = Stein_mitte.GetRandom();
            Objekt.Set_Rotation_Random();
        }
        Objekt.Rohstoff = Stein_mitte.Get(Objekt.Index_Top);
        
    }
    private void Stein_Rand(Grid_opjekt Objekt, int winkel,int ebene)
    {
        if (random == true)
        {
            Objekt.Index_Top = Stein_rand_h�che.GetRandom();
            if (ebene <= 0)
            {
                Objekt.Index_Top = Stein_rand.GetRandom();
            }
        }
        Transform element = null;
        if (ebene <= 0)
        {
            element = Stein_rand.Get(Objekt.Index_Top);
        }
        else
        {
            element = Stein_rand_h�che.Get(Objekt.Index_Top);
        }
        Objekt.Rohstoff = element;
        Objekt.Setrotation(winkel);
    }
 


    #endregion
}
