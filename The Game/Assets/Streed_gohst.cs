using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Streed_gohst : MonoBehaviour
{
    public static Streed_gohst singleton { set; get; }
    [SerializeField] Transform go;
    [SerializeField] Miene s;
    [SerializeField] Lager Lager_Global;
    [SerializeField] Transform Mine;
    [SerializeField] Transform Courser;
    [SerializeField] Transform passt;
    [SerializeField] Transform passtnicht;
    bool leftbuttonpressed = false;

    private Bauen BuildingsystemsAktions;

    Transform newr;
    public void Awake()
    {
        singleton = this;
        Global.Mine.Add(s);
        Global.Lager.Add( Lager_Global);
        BuildingsystemsAktions = new Bauen();
        BuildingsystemsAktions.Buildings.Build.performed += _ => Build(_.ReadValueAsButton());
        BuildingsystemsAktions.Buildings.Build.Enable();
    }

    void Build(bool bu)
    {
        X1= X;
        Y1 = Y;
        leftbuttonpressed = bu;
        if (bu == false && Global.buildmoide == 1)
        {
            foreach ( Vector2 vec2 in d)
            {
                Map.Map_Rohstoffe[System.Convert.ToInt16(vec2.x), System.Convert.ToInt16(vec2.y)] = 100100000 + Map.Map_Rohstoffe[System.Convert.ToInt16(vec2.x), System.Convert.ToInt16(vec2.y)]%100;  //wie speicher ich das ich zk Otto hier kˆnnnte man nioch eine funktion machen 
            }
            d.Clear();
        }
    }
    new List<Vector2> d = new List<Vector2>();

    int X1 = 0;
    int Y1 = 0;
    int helpco = 0;
    int X, Y;
    private void streed_Build(Vector3 World)
    {
      
        Vector3 World_pos = straﬂe.singleton.Get_World_Postion(World);

        X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);

        if (leftbuttonpressed == true )
        {
            int ‹bersprungene_tieles = Mathf.Abs(X1 - X) + Mathf.Abs(Y1 - Y);
            for (int i = 0; i < ‹bersprungene_tieles + 1; i++)
            {
                if (Mathf.Abs(X1 - X) > Mathf.Abs(Y1 - Y) && X1 != X)
                {
                    if (X > X1) X1++;
                    else X1--;
                }
                else if (Mathf.Abs(X1 - X) <= Mathf.Abs(Y1 - Y) && Y1 != Y)
                {
                    if (Y > Y1) Y1++;
                    else Y1--;
                }
                if(Map.Map_Rohstoffe.GetLength(0) > X1 && X1 > 0 && Map.Map_Rohstoffe.GetLength(1) > Y1 && Y1 > 0)
                {
                    if (Rohstoffe.singleton.Rohstoff_test(Map.Map_Rohstoffe[X1, Y1], 10)) // falls schon eine strasse auf dem objekt/tail ist 
                    {
                        Map.Map_Rohstoffe[X1, Y1] = 100010000 + Map.Map_Rohstoffe[X1, Y1] % 100;
                        helpco = 0;
                    }
                    else if (straﬂe.singleton.Can_build(World))
                    {
                        if (!d.Contains(new Vector2(X1, Y1)))
                        {
                            d.Add(new Vector2(X1, Y1));
                        }
                        Map.Map_Rohstoffe[X1, Y1] = 100010000 + Map.Map_Rohstoffe[X1, Y1] % 100;
                    }
                }

            }         
        }                                                                                                                                             

    }
    private void Lager_Build(Vector3 World)
    {

        Vector3 World_pos = straﬂe.singleton.Get_World_Postion(World);
        X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);

        if (leftbuttonpressed == true && Global.Lager[Global.Building_index].Can_build(World))
        {
           
            
                for (int x1 = 0; x1 < Global.Lager[Global.Building_index].GrˆsseX; x1++) // noch eigene funktion f¸r schˆneheit zukunfts Otto
                {
                    for (int y1 = 0; y1 < Global.Lager[Global.Building_index].GrˆsseY; y1++)
                    {
                        float F = y1, G = x1;
                        if (Global.Buildingrotation == 90)
                        {
                            G = -y1;
                            F = x1;
                        }
                        if (Global.Buildingrotation == 0)
                        {
                            G = -x1;
                            F = -y1;
                        }
                        if (Global.Buildingrotation == 270)
                        {
                            G = y1;
                            F = -x1;
                        }
                        if (Global.Lager[Global.Building_index].Plase[(x1 * (Global.Lager[Global.Building_index].GrˆsseY)) + y1] == 2)
                        {
                            Map.Map_Rohstoffe[System.Convert.ToInt32(X + G), System.Convert.ToInt16(Y + F)] = 0100039900;

                        }//Muss noch durch ID variable der MIne erstzt weerden ZK Otto und mach achu eine funktion drasu
                        if (Global.Lager[Global.Building_index].Plase[(x1 * (Global.Lager[Global.Building_index].GrˆsseY)) + y1] == 1)
                        {
                            int b1 = 0;
                            int b2 = 0;
                            int b4 = 0;
                            int b8 = 0;
                            if ((x1 + 1) * (Global.Lager[Global.Building_index].GrˆsseY) + (y1) < Global.Lager[Global.Building_index].Plase.Count && 0 <= (x1 + 1))
                            {
                                if (Global.Lager[Global.Building_index].Plase[((x1 + 1) * (Global.Lager[Global.Building_index].GrˆsseY)) + (y1)] == 2)
                                {
                                    
                                    b1 = 1;
                                }
                            }
                            if ((x1) * (Global.Lager[Global.Building_index].GrˆsseY) + (y1 + 1) < Global.Lager[Global.Building_index].Plase.Count && 0 <= (y1 + 1))
                            {

                                if (Global.Lager[Global.Building_index].Plase[((x1) * (Global.Lager[Global.Building_index].GrˆsseY)) + (y1 + 1)] == 2)
                                {
                                    
                                    b2 = 1;
                                }
                            }
                            if ((x1 - 1) * (Global.Lager[Global.Building_index].GrˆsseY) + (y1) < Global.Lager[Global.Building_index].Plase.Count && 0 <= (x1 - 1))
                            {
                                if (Global.Lager[Global.Building_index].Plase[((x1 - 1) * (Global.Lager[Global.Building_index].GrˆsseY)) + (y1)] == 2)
                                {
                                 
                                    b4 = 1;
                                }
                            }

                            if ((x1) * (Global.Lager[Global.Building_index].GrˆsseY) + (y1 - 1) < Global.Lager[Global.Building_index].Plase.Count && 0 <= (y1 - 1))
                            {
                                if (Global.Lager[Global.Building_index].Plase[((x1) * (Global.Lager[Global.Building_index].GrˆsseY)) + (y1 - 1)] == 2)
                                {
                                    b8 = 1;
                                }
                            }
                            // je nach rotation rotieren wir die zahlen
                            int bsafe = b1;
                            if (Global.Buildingrotation == 0)
                            {
                                b1 = b8;
                                b8 = b4;
                                b4 = b2;
                                b2 = bsafe;
                            }
                            if (Global.Buildingrotation == 90)
                            {
                                bsafe = b2;
                                int bsafe_2 = b4;
                                b2 = b8;
                                b4 = b1;
                                b8 = bsafe;
                                b1 = bsafe_2;

                            }
                            if (Global.Buildingrotation == 270)
                            {
                                b1 = b2;
                                b2 = b4;
                                b4 = b8;
                                b8 = bsafe;
                            }
                            int sum = (b1 * 1) + (b2 * 2) + (b4 * 4) + (b8 * 8);
                            print(Global.Buildingrotation + " summe " + sum + "kord:" + x1 + "," + y1);
                            Map.Map_Rohstoffe[System.Convert.ToInt32(X + G), System.Convert.ToInt16(Y + F)] = 100100000 + sum;

                        }
                    }
                }
                Map.Map_Rohstoffe[System.Convert.ToInt16(X), System.Convert.ToInt16(Y)] = 0100030000;

                // sicher heit ein bauen
        }

    }
    // Update is called once per frame

    private void Update()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero * 4);
                        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit distance))
        {


            if (transform.Find("curser"))
            {
                DestroyImmediate(transform.Find("curser").gameObject);
            }
            Transform courser = new GameObject("curser").transform;
            courser.parent = transform;
            courser.position = transform.position;

            if (Global.buildmoide == 1)
            {

                streed_Build(distance.point);
                if (straﬂe.singleton.Passt(distance.point))
                {
                    Mine = passt; 
                }
                else
                {
                    Mine = passtnicht;
                }

                
                if (Mine != null) newr = Instantiate(Mine, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, Global.Buildingrotation, 0)) as Transform;
                if (newr != null) newr.parent = courser;


            }
            if (Global.buildmoide == 2)
            {
                Mine = go;
                for (float x = 0; x < Global.Mine[Global.Building_index].GrˆsseX; x++) // float da minus zahlen
                {
                    for (float y = 0; y < Global.Mine[Global.Building_index].GrˆsseY; y++)
                    {
                        float F = y, G = x;

                        if (Global.Buildingrotation == 90)
                        {
                            G = -y;
                            F = x;
                        }
                        if (Global.Buildingrotation == 0)
                        {
                            G = -x;
                            F = -y;
                        }
                        if (Global.Buildingrotation == 270)
                        {
                            G = y;
                            F = -x;
                        }


                        Mine = Global.Mine[Global.Building_index].getcourser(new Vector3(distance.point.x + G, 0, distance.point.z + F), System.Convert.ToInt32(x), System.Convert.ToInt32(y)); // hier musss ich x und 


                        if (Mine != null) newr = Instantiate(Mine, new Vector3(transform.position.x + G, transform.position.y, transform.position.z + F), Quaternion.Euler(0, Global.Buildingrotation, 0)) as Transform;
                        if (newr != null) newr.parent = courser;

                       
                        Mine = Global.Mine[Global.Building_index].getcourser(new Vector3(distance.point.x + G, 0, distance.point.z + F),System.Convert.ToInt32(x), System.Convert.ToInt32(y)); // hier musss ich x und 
                        if(Mine != null)newr = Instantiate(Mine, new Vector3(transform.position.x+G,transform.position.y,transform.position.z +F), Quaternion.Euler(0, Global.Buildingrotation, 0)) as Transform;
                        if(newr != null)newr.parent = courser;

                    }
                }
            }
            if (Global.buildmoide == 3)
            {
                Global.Buildingrotation = 0;
                Lager_Build(distance.point);
                for (float x = 0; x < Global.Lager[Global.Building_index].GrˆsseX; x++) // float da minus zahlen
                {
                    for (float y = 0; y < Global.Lager[Global.Building_index].GrˆsseY; y++)
                    {
                        float F = y, G = x;

                        if (Global.Buildingrotation == 90)
                        {
                            G = -y;
                            F = x;
                        }
                        if (Global.Buildingrotation == 0)
                        {
                            G = -x;
                            F = -y;
                        }
                        if (Global.Buildingrotation == 270)
                        {
                            G = y;
                            F = -x;
                        }

                        Courser = Global.Lager[Global.Building_index].getcourser(new Vector3(distance.point.x + G, 0, distance.point.z + F), System.Convert.ToInt32(x), System.Convert.ToInt32(y)); // hier musss ich x und 

                        if (Courser != null) newr = Instantiate(Courser, new Vector3(transform.position.x + G, transform.position.y, transform.position.z + F), Quaternion.Euler(0, Global.Buildingrotation, 0)) as Transform;
                        if (newr != null) newr.parent = courser;
                    }
                }
                if (Mine != null) newr = Instantiate(Mine, new Vector3(transform.position.x , transform.position.y, transform.position.z ), Quaternion.Euler(0, Global.Buildingrotation, 0)) as Transform;
                if (newr != null) newr.parent = courser;
            }
            if (Global.buildmoide == 0)
            {
                Vector3 World_pos = straﬂe.singleton.Get_World_Postion(distance.point);
                X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
                Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);
                //Map.grid.GetGridOpjekt(X, Y).Update_World(Global.MAP.Chuncks[1]);



                if (transform.Find("curser"))
                {
                    DestroyImmediate(transform.Find("curser").gameObject);
                }
                Mine = null;
            }





            Vector3 Target1 = distance.point;
                Target1.x = Mathf.Floor(Target1.x + 0.5f);
                Target1.z = Mathf.Floor(Target1.z + 0.5f);
                Target1.y = 3;
                transform.position = Vector3.Lerp(transform.position, Target1, Time.deltaTime * 8);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Global.Buildingrotation, 0), Time.deltaTime * 8);
            
        }

 
    }
}
