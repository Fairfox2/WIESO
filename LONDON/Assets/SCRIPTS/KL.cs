using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KL : MonoBehaviour
{

    int Junkx = 10, Junky = 10;
    public Transform tilePrefab;
    public Transform baumPrefab;
    //Stein
    public Transform SteinPrefab_lose;
    public Transform SteinPrefab_Ecke;
    public Transform SteinPrefab_Rand;
    public Transform SteinPrefab_Mitte;
    public Transform SteinPrefab_Brücke;

    public Vector2 mapSize;
    // Postitions_Arrays // Alle postitionen im junk
    public int[,] Junk = new int[10,10];
    public int[,] baum;
    public int[,] Stein; 
    

    [Range(0, 1)]
    public float outLinePercent;
    public void GeneratMao()
    {
        string holdename = "Generated Junk";
        if(transform.Find (holdename))
        {
            DestroyImmediate(transform.Find(holdename).gameObject);
        }

        Transform mapMolder = new GameObject(holdename).transform;
       
        mapMolder.parent = transform;
        mapMolder.position = transform.position;
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                   
                    Vector3 tilePosition = new Vector3(-mapSize.x/2  +0.5f + x, -1.5f , -mapSize.y/2 + 0.5f+  y) + transform.position;
                    Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right)) as Transform;
                    newTile.localScale = Vector3.one * (1 - outLinePercent);
                    newTile.parent = mapMolder;
                
            }
        }
        for (int i = 0; i < Random.Range(0,3); i++)
        {
            Wald();
        }
        Stein_Genarator(3,3);

    }
    private void Awake()
    {

        GeneratMao();
        System.Console.Write("dsa");
    }

     public void Wald()
     {
   
        int xm = 5;
        int ym = 5;

        baum = new int[xm, ym];
        baum[2, 2] = 1;
        int sum = 1;
        int sum_z = 1;

        string holdename1 = "Generated Trees";
        if (transform.Find(holdename1))
        {
            DestroyImmediate(transform.Find(holdename1).gameObject);
        }

        Transform mapMolder1 = new GameObject(holdename1).transform;
        mapMolder1.parent = transform;
        mapMolder1.position = transform.position;



        for (int i = 0; i < Random.Range(10, 10); i++)
        { 
            int x = 0;
            int y = 0;

            do
            {

                int h = Random.Range(0,4);


                if (h == 3 && x != xm - 1) { x++; }
                if (h == 2 && y != ym - 1) { y++; }

                if (h == 1 && x != 0) { x--; }
                if (h == 0 && y != 0) { y--; }

                if (baum[x, y] != 1)
                {
                    baum[x, y] = 1;
                    
                    sum++;
                }


            } while (sum <= sum_z);

            sum_z = sum;

        }

        // größe erfassen
   
        int X_G= 0,Y_G=0;
        for (int i = 0; i < xm; i++)
        {
            for (int a = 0; a < ym; a++)
            {
                if (baum[i, a] != 0)
                {

                    if (a > Y_G)
                    {
                        Y_G = a;
                    }
                    if (i > X_G)
                    {
                        X_G = i;
                    }


                }
            }    
        }

        // Postitions BReeich ERrechnnen Und Erstellen
        bool passst = true;
        int X, Y;
        do
        {
            passst = true;
             Y = Random.Range(Junky / -2, (Junky / 2) - Y_G);
             X = Random.Range(Junky / -2, (Junkx / 2) - X_G);

            for (int i = 0; i < xm; i++)
            {
                for (int a = 0; a < ym; a++)
                {
                    if (baum[i, a] != 0)
                    {
                        if (Junk[i + X+5, a + Y + 5] != 0)
                        {
                            print("passt nicht");
                            passst = false;
                        }

                    }

                }
            }
        }while (passst == false);  

        for (int i = 0; i < xm; i++)
        {
            for (int a = 0; a < ym; a++)
            {
                if (baum[i, a] != 0)
                {
                    Vector3 baumposition = new Vector3(X + i + 0.5f, 1, Y + a + 0.5f) + transform.position;

                    Transform newTile = Instantiate(baumPrefab, baumposition, Quaternion.Euler(0, 90 * Random.Range(0, 4), 0)) as Transform;
                    newTile.parent = mapMolder1;
                }

            }
        }


     }

    public void Stein_Genarator( int X_Größe,int Y_Größe)
    {
        
        int xm = X_Größe+2;
        int ym = Y_Größe+2;

        Stein = new int[xm, ym];
        Stein [2, 2] = 1;
        
        int sum = 1;
        int sum_z = 1;

        string holdenameStone = "Generated Stone";
        if (transform.Find(holdenameStone))
        {
            DestroyImmediate(transform.Find(holdenameStone).gameObject);
        }

        Transform mapMolder1 = new GameObject(holdenameStone).transform;
        mapMolder1.parent = transform;
        mapMolder1.position = transform.position;

        for (int i = 0; i < Random.Range(6,8); i++) // Random defiiniert wei viele bäume erstellt werden 
        {


            int x = 2;
            int y = 2;

            do
            {

                
                int h = Random.Range(0, 4);


                if (h == 3 && x != xm -2 ) { x++; }
                if (h == 2 && y != ym - 2) { y++; }

                if (h == 1 && x != 1) { x--; }
                if (h == 0 && y != 1) { y--; }

                if (Stein[x, y] != 1)
                {
                    Stein[x, y] = 1;
                    sum++;
                }



            } while (sum <= sum_z);

            sum_z = sum;

        }

        // größe erfassen

        int X_G = 0, Y_G = 0;
        for (int i = 0; i < xm; i++)
        {
            for (int a = 0; a < ym; a++)
            {
                if (Stein[i, a] != 0)
                {

                    if (a > Y_G)
                    {
                        Y_G = a;
                    }
                    if (i > X_G)
                    {
                        X_G = i;
                    }


                }
            }
        }

        // Postitions BReeich ERrechnnen Und Erstellen
        int Y = Random.Range(Junky / -2, (Junky / 2) - Y_G);
        int X = Random.Range(Junky / -2, (Junkx / 2) - X_G);
        //Test erstellen

    


    
        for (int q = 1; q < xm-1; q++)
        {
            for (int e = 1; e < ym-1; e++)
            {                           //   ?
                if (Stein[q, e] != 0)   // ? # ?
                {                       //   ?
                    
                                          //    ?
                    if(Stein[q-1,e] != 0) // '# # ?
                    {                     //    ?

                                                    //    ?
                        if (Stein[q + 1, e] != 0)   //  # # '#
                        {                           //    ?
                            
                                                    //   ?
                            if(Stein[q,e-1] != 0)   // # # #
                            {                       //  '#

                                                        //  '#
                                if(Stein[q,e+1] != 0)   // # # #
                                {                       //   #
                                    Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                                    Transform newTile = Instantiate(SteinPrefab_Mitte, Steinpos, Quaternion.Euler(0, 90 * Random.Range(0,4), 0)) as Transform;
                                    newTile.parent = mapMolder1;
                                }
                                else
                                {
                                    Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                                    Transform newTile = Instantiate(SteinPrefab_Rand, Steinpos, Quaternion.Euler(0,180 , 0)) as Transform;
                                    newTile.parent = mapMolder1;
                                }
                            }
                            else if(Stein[q,e+1] != 0)
                            {
                                Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                                Transform newTile = Instantiate(SteinPrefab_Rand, Steinpos, Quaternion.Euler(0, 0, 0)) as Transform;
                                newTile.parent = mapMolder1;
                            }
                            else
                            {
                                Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                                Transform newTile = Instantiate(SteinPrefab_Brücke, Steinpos, Quaternion.Euler(0, 180, 0)) as Transform;
                                newTile.parent = mapMolder1;
                            }


                        }                               //    ?
                        else if (Stein[q, e - 1] != 0)  //  # # -
                        {                               //   '#

                                                    //  '#
                            if(Stein[q,e+1] != 0)   // # # -
                            {                       //   #

                                Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                                Transform newTile = Instantiate(SteinPrefab_Rand, Steinpos, Quaternion.Euler(0, 270, 0)) as Transform;
                                newTile.parent = mapMolder1;
                            }
                            else
                            {
                                
                                Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                                Transform newTile = Instantiate(SteinPrefab_Ecke, Steinpos, Quaternion.Euler(0, 270, 0)) as Transform;
                                newTile.parent = mapMolder1;
                            }
       
                        }

                                                        //  '#
                        else if (Stein[q, e + 1] != 0)  // # # -
                        {                               //   -
                           
                            Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                            Transform newTile = Instantiate(SteinPrefab_Ecke, Steinpos, Quaternion.Euler(0, 0, 0)) as Transform;
                            newTile.parent = mapMolder1;
                        }
                    
                        else
                        {
                            Vector3 Steinpos = new Vector3(X + q + 0.5f, 1,0.5f + Y + e) + transform.position;
                            Transform newTile = Instantiate(SteinPrefab_lose, Steinpos, Quaternion.Euler(0, 180, 0)) as Transform;
                            newTile.parent = mapMolder1;
                        }


                    }                          //   ?
                    else if(Stein[q+1,e] != 0) // - # '#
                    {                          //   ?

                                                    //   ?
                        if (Stein[q, e - 1] != 0)   // - # #
                        {                           //  '#

                                                        //  '#
                            if (Stein[q, e + 1] != 0)   // - # #
                            {                           //   #
                                Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                                Transform newTile = Instantiate(SteinPrefab_Rand, Steinpos, Quaternion.Euler(0, 90 , 0)) as Transform;
                                newTile.parent = mapMolder1;
                            }
                            else
                            {
                                
                                Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                                Transform newTile = Instantiate(SteinPrefab_Ecke, Steinpos, Quaternion.Euler(0, 180, 0)) as Transform;
                                newTile.parent = mapMolder1;
                            }
                        }                               //  '#
                        else if (Stein[q, e + 1] != 0)  // - # #
                        {                               //   -
                            
                            Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                            Transform newTile = Instantiate(SteinPrefab_Ecke, Steinpos, Quaternion.Euler(0, 90, 0)) as Transform;
                            newTile.parent = mapMolder1;
                        }
                        else
                        {
                            Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                            Transform newTile = Instantiate(SteinPrefab_lose, Steinpos, Quaternion.Euler(0, 0, 0)) as Transform;
                            newTile.parent = mapMolder1;
                        }
                    }                                //   ?
                    else if (Stein[q, e-1] != 0)   // - # -
                    {                                //  '#


                                                    //  '#
                        if (Stein[q, e + 1] != 0)   // - # -
                        {                           //   #

                            Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                            Transform newTile = Instantiate(SteinPrefab_Brücke, Steinpos, Quaternion.Euler(0, 180, 0)) as Transform;
                            newTile.parent = mapMolder1;
                        }
                        else
                        {
                            print("asd");
                            Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                            Transform newTile = Instantiate(SteinPrefab_lose, Steinpos, Quaternion.Euler(0, 90, 0)) as Transform;
                            newTile.parent = mapMolder1;
                        }

                    }
                    else if (Stein[q,e+1] != 0)
                    {
                        print("scccccc");
                        Vector3 Steinpos = new Vector3(X + q + 0.5f, 1, 0.5f + Y + e) + transform.position;
                        Transform newTile = Instantiate(SteinPrefab_lose, Steinpos, Quaternion.Euler(0, 270, 0)) as Transform;
                        newTile.parent = mapMolder1;
                    }

                }

            }
        }
        // muss noch ins junk array eintragne´
       
        
    }


}
