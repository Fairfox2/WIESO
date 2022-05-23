using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rohstoffe : MonoBehaviour
{
    public static Rohstoffe singleton { set; get; }
    bool voll = false;
    //Wald wiese BOden 
    [Space(2.5f)]
    [SerializeField] private Transform Wise0;
    [SerializeField] private Transform Wise1;
    [SerializeField] private Transform Wise2;
    [SerializeField] private Transform Wise3;
    [Space(10)]



    [Space(10)]
    [Header("Stein")]
    [Space(5)]
    [SerializeField] private Transform LehmPrefab_Ecke;
    [SerializeField] private Transform LehmPrefab_Kurve;
    [SerializeField] private Transform LehmPrefab_Rand;
    [SerializeField] private Transform LehmPrefab_Rand1;
    [SerializeField] private Transform LehmPrefab_Rand2;
    [SerializeField] private Transform LehmPrefab_Rand3;
    [SerializeField] private Transform LehmPrefab_Mitte;
    [SerializeField] private Transform LehmPrefab_Baum;
    [SerializeField] private Transform LehmPrefab_Baum1;
    [SerializeField] private Transform LehmPrefab_Boden;
    [SerializeField] private Transform lul;
    [Space(20)]
    Grid_script<Grid_opjekt> grid;


    /*
     * Regeln für Rohstoff array 
     *  die Hundertestelle gibt an welsches elementes ist 
     *      100  : Stein
     *      200  : Wald
     *      300  : Lehm
     *      400  : Straße
     *      500  :
     * 
     *  die Zehnerstelle gibt an ob ein gebäde drauf steht 
     *      10  : Gebäude
     *      20  : Gebäude 2
     *      30  : 
     *      40  :
     *      50  :
     * 
     */





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
    #region Lehm
    public void Lehm_Biom( int Radius_des_Biomes, double Zone, int toleranz)
    {
        int Radius1 = Radius_des_Biomes;// der radius von dem Biom 
        double Zone_Spawn = Zone; // in welcher zone es spanen kann 
        if(Zone_Spawn* Map.chunck_grösse + 8 < Radius1)
        {
            print("Fehler : Lehm Biom ist zu gross.");
            return;
        }
        int Biompos = System.Convert.ToInt32((((Map.Map_grösse - 1) / 2) - Zone_Spawn) * Map.chunck_grösse) - 1; // hier brechne ich wie vile zone es gesamt gibt und schau wo mein moentane zone im arra liegt 
        bool passt;
        int X_sp;
        int Y_sp;
        do
        {

            passt = true;
            int Zonepos_Max_spwan = System.Convert.ToInt32((3 + (2 * (Zone_Spawn - 1))) * Map.chunck_grösse) - 1; // hier wirde der eusere ring brehnent 

            Y_sp = Random.Range(0, Zonepos_Max_spwan);
            X_sp = Random.Range(0, Zonepos_Max_spwan);

            double XK = X_sp - (Zonepos_Max_spwan) / 2;
            double YK = Y_sp - (Zonepos_Max_spwan) / 2;
            double Radius = Zonepos_Max_spwan / 2;
            
            if (Zone_Spawn > 1)
            {
                Radius = (Zonepos_Max_spwan / 2) + 1;
            }
            double Radius_min = Radius + toleranz;  // minus  ist die toleranz  darf nie null sein 
            double Fmin  = XK * XK + YK * YK - Radius * Radius;
            double F   = XK * XK + YK * YK - Radius_min * Radius_min;
            if (F > 0)   // fasle es über dem kreis ist 
            {
                passt = false;
            }
            if (Fmin < 0)   // fasle es über dem kreis ist 
            {
                passt = false;
            }


        } while (passt == false && voll == false);
        Map.Biom[0, 0] = X_sp + Biompos -Radius1;
        Map.Biom[0, 1] = Y_sp + Biompos -Radius1;
        Map.Biom[0, 2] =  Radius1;

        for (int x = 0; x < Radius1*2; x++)
        {
            for (int y = 0; y < Radius1*2; y++)
            {
              
                    int XK = x - Radius1;
                    int YK = y - Radius1;
                    

                    double F = XK * XK + YK * YK - Radius1 * Radius1;
                if (F < 0)
                {
                    if(Map.Map_Rohstoffe[X_sp + Biompos + YK, Y_sp + Biompos + XK] < 2000)
                    {
                        Map.Map_Rohstoffe[X_sp + Biompos + YK, Y_sp + Biompos + XK] = 2000;
                    }
                }

                
            }
        }
    }
    public void Lehm_setzen(int[,] Lehm_block, Grid_opjekt gr, int X, int Y)
    {
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

        }
    }
    public void Lehm_Genarator(int anzahl, int Radius_Biom, double Zone, int toleranz)
    {
        voll = false;

        int[] Chunck_position = new int[4];
        int xm = 42;
        int ym = 42;
        int Y = 0;
        int X = 0;

        bool passt;
        int[,] Bäume = new int[xm, ym];                                                                     // nur positnin steinfelck 
                                                                                                            // Alle Steine in diesem Chunck  

        Lehm_Biom(Radius_Biom, Zone, toleranz);
        if (!voll)
        {

            int max = 160;
            int min = 123;
            bool voll = false;
            int[] Grösse = new int[5];

            if (!voll)
            {
                for (int w = 0; w < anzahl && !voll; w++)
                {
                    
                    generieren_spezial(min, max, Bäume);
                    Grösse = größe_feststellen(Bäume);

                    passt = false;
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
                        if (zähler >= 600)
                        {
                            Kor++;
                            element_rücksetzen(Bäume);
                            generieren_spezial(min - Kor, max - Kor, Bäume);
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
                                Map.Map_Rohstoffe[i + X + X_sp, r + Y + Y_sp] = 2400;
                            }

                        }
                    }
                    //++++ ins grid setzen 
                }
                for (int i = 0; i <= Radius_Biom *2; i++)
                {
                    for (int t = 0; t  <=Radius_Biom * 2;t ++)
                    {
                        if (BiomVereinfacher(i + Map.Biom[0, 0] , t + Map.Biom[0, 1] , 2000,2999,3,3))
                        {
                            Map.Map_Rohstoffe[i + Map.Biom[0, 0], t + Map.Biom[0, 1]] = 1000;
                        }
                        if(Map.Map_Rohstoffe[i + Map.Biom[0, 0], t + Map.Biom[0, 1]] == 2000) // iher setzen wir noch den boden random
                        {
                            int x = Random.Range(0, 2);
                        }
                    }
                }
                Lehm_bäume(4); // hier im nachinein die bäue platzieren da es so besser ist 
            }
        }





    }
  
    private void Lehm_bäume(int anzahl) 
    {
        voll = false;

        int[] Chunck_position = new int[4];
        int xm = 42;
        int ym = 42;
        int Y = 0;
        int X = 0;

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
                    passt = false;
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
    public void Lehm_Baum(Grid_opjekt Objekt)
    {
        int r;
        r = Random.Range(0, 2);
        switch (r)
        {
            case 0:
                Objekt.Rohstoff = LehmPrefab_Baum;
                break;
            case 1:
                Objekt.Rohstoff = LehmPrefab_Baum1;
                break;

        }
        Objekt.Set_Rotation_Random();
        Objekt.rohstoff = true;
    }
    public void Lehm_Boden(Grid_opjekt Objekt)
    {
        int r;
        r = Random.Range(0, 2);
        switch (r)
        {
            case 0:
                Objekt.Boden = LehmPrefab_Boden;
                break;
            case 1:
                // hier pasiert nichts
                break;

        }
        Objekt.Set_Rotation_Random();

    }
    private void Lehm_Ecke(Grid_opjekt Objekt, int winkel)
    {
        int r;
        r = Random.Range(0, 1);
        switch (r)
        {
            case 0:
                Objekt.Boden = LehmPrefab_Ecke;
                break;

        }
        Objekt.Setrotation(winkel);
        Objekt.boden_Rohstoff = true;
    }
    private void Lehm_Mitte(Grid_opjekt Objekt)
    {
        int r;
        r = Random.Range(0, 1);
        switch (r)
        {
            case 0:
                Objekt.Boden = LehmPrefab_Mitte;
                break;

        }
        Objekt.Set_Rotation_Random();
        Objekt.boden_Rohstoff = true;
    }
    private void Lehm_Rand(Grid_opjekt Objekt, int winkel)
    {
        int r;
        int summe = 0;
        int anzahl = 4;
        int protzent = 0;
        int max;
        int min;
        int fertig = 1;
        r = Random.Range(0, 10001);
        int[] Teile = new int[anzahl];

        // seltenheits system
        Teile[0] = 5;
        Teile[1] = 5;
        Teile[2] = 5;
        Teile[3] = 5;

        for (int i = 0; i < anzahl; i++)
        {
            summe += Teile[i];
        }
        protzent = 10000 / summe;
        for (int i = 0; i < anzahl; i++)
        {
            if (i == 0)
            {
                min = 0;
                max = (protzent * Teile[i]) - 1;
            }
            else
            {
                min = Teile[i - 1] * protzent;
                max = (protzent * Teile[i]) - 1;
            }
            if (r > min && r < max)
            {
                fertig = i;
            }
        }

        switch (fertig)
        {
            case 0:
                Objekt.Boden = LehmPrefab_Rand;    // Die Mitte kann auch als rand verwendet werden 
                break;
            case 1:
                Objekt.Boden = LehmPrefab_Rand1;
                break;
            case 2:
                Objekt.Boden = LehmPrefab_Rand2;
                break;
            case 3:
                Objekt.Boden = LehmPrefab_Rand3;
                break;


        }
        Objekt.Setrotation(winkel);
        Objekt.boden_Rohstoff = true;
    }
    private void Lehm_Kurve(Grid_opjekt Objekt, int winkel)
    {
        int r;
        r = Random.Range(0, 1);
        switch (r)
        {
            case 0:
                Objekt.Boden = LehmPrefab_Kurve;
                break;
            case 1:
                Objekt.Boden = LehmPrefab_Kurve;
                break;
        }
        Objekt.Setrotation(winkel);
        Objekt.boden_Rohstoff = true;
    }

    #endregion
    #endregion
}  


