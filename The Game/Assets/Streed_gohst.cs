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
    [SerializeField] Transform Mine;
    [SerializeField] Transform passt;
    [SerializeField] Transform passtnicht;
    bool leftbuttonpressed = false;

    private Bauen BuildingsystemsAktions;

    Transform newr;
    public void Awake()
    {
        singleton = this;
        Global.Mine_Focus = s;
        BuildingsystemsAktions = new Bauen();
        BuildingsystemsAktions.Buildings.Build.performed += _ => Build(_.ReadValueAsButton());
        BuildingsystemsAktions.Buildings.Build.Enable();
    }

    void Build(bool bu)
    {
        X1= X;
        Y1 = Y;
        leftbuttonpressed = bu;
        if (bu == false)
        {
         
            foreach ( Vector2 vec2 in d)
            {
                
                Map.Map_Rohstoffe[System.Convert.ToInt16(vec2.x), System.Convert.ToInt16(vec2.y)] = 100100000 + Map.Map_Rohstoffe[System.Convert.ToInt16(vec2.x), System.Convert.ToInt16(vec2.y)]%100;  //wie speicher ich das ich zk Otto hier kˆnnnte man nioch eine funktion machen
                
            }
            d.Clear();
        }
    }
    new List<Vector2> d = new List<Vector2>();
    new List<Vector2> f = new List<Vector2>();

    int X1 = 0;
    int Y1 = 0;
    int helpco = 0;
    int X, Y;
    private void streed_Build(Vector3 World)
    {
      
        Vector3 World_pos = straﬂe.singleton.Get_World_Postion(World);

        X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);
        if(X1==0 || Y1 == 0)
        {
            X1 = X;
            Y1 = Y;
        }
        if (leftbuttonpressed == true && !(X1 == X &&  Y1 == Y))
        {
            int ‹bersprungene_tieles = Mathf.Abs(X1 - X) + Mathf.Abs(Y1 - Y);
            for (int i = 0; i < ‹bersprungene_tieles; i++)
            {
                if(Mathf.Abs(X1 - X) > Mathf.Abs(Y1 - Y) && X1 != X)
                {
                    if(X>X1) X1 ++;
                    else X1--;
                }
                else if (Mathf.Abs(X1 - X) <= Mathf.Abs(Y1 - Y) && Y1 != Y)
                {
                    if (Y > Y1) Y1++;
                    else Y1--;
                }
                if (Rohstoffe.singleton.Rohstoff_test(Map.Map_Rohstoffe[X1, Y1], 10))
                {
                    Map.Map_Rohstoffe[X, Y] = 100010000 + Map.Map_Rohstoffe[X1, Y1] % 100;
                    helpco = 0;
                }
                else
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
    // Update is called once per frame
    private void Update()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero * 4);
                        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (plane.Raycast(ray, out float distance))
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

                Vector3 World_pos = straﬂe.singleton.Get_World_Postion(ray.GetPoint(distance));

                int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
                int Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);

                streed_Build(ray.GetPoint(distance));
                if (straﬂe.singleton.Passt(ray.GetPoint(distance)))
                {
                    Mine = passt;
                    
                }
                else
                {
                    Mine = passtnicht;
                }
                if(Mine != null)newr = Instantiate(Mine, transform.position, Quaternion.Euler(0, 0, 0)) as Transform;
                if(newr != null)newr.parent = courser;    
            }
            if (Global.buildmoide == 2)
            {

                Mine = go;
                for (float x = 0; x < Global.Mine_Focus.GrˆsseX; x++) // float da minus zahlen
                {
                    for (float y = 0; y < Global.Mine_Focus.GrˆsseY; y++)
                    {
                        float F=y, G = x;
                       
                        if (Global.Buildingrotation == 90)
                        {
                            G =  - y;
                            F = x;
                        }
                        if (Global.Buildingrotation == 0)
                        {
                            G = - x;
                            F =  - y;
                        }
                        if (Global.Buildingrotation == 270)
                        {
                            G = y;
                            F = - x;
                        }
                       
                         Mine = Global.Mine_Focus.getcourser(new Vector3(ray.GetPoint(distance).x + G, 0, ray.GetPoint(distance).z + F),System.Convert.ToInt32(x), System.Convert.ToInt32(y)); // hier musss ich x und 
                        if(Mine != null)newr = Instantiate(Mine, new Vector3(transform.position.x+G,transform.position.y,transform.position.z +F), Quaternion.Euler(0, Global.Buildingrotation, 0)) as Transform;
                        if(newr != null)newr.parent = courser;
                    }
                }
            }
            if(Global.buildmoide == 3)
            {
                if (straﬂe.singleton.Passt(ray.GetPoint(distance)))
                {
                    Mine = passt;
                }
                else
                {
                    Mine = passtnicht;
                }
                if (Mine != null) newr = Instantiate(Mine, new Vector3(transform.position.x , transform.position.y, transform.position.z ), Quaternion.Euler(0, Global.Buildingrotation, 0)) as Transform;
                if (newr != null) newr.parent = courser;
            }
            if (Global.buildmoide == 0)
            {
                if (transform.Find("curser"))
                {
                    DestroyImmediate(transform.Find("curser").gameObject);
                }
                Mine = null;
            }
            Vector3 Target1 = ray.GetPoint(distance);
            Target1.x = Mathf.Floor(Target1.x);
            Target1.z = Mathf.Floor(Target1.z);
            Target1.y = 3;

            transform.position = Vector3.Lerp(transform.position, Target1, Time.deltaTime * 8);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Global.Buildingrotation,0), Time.deltaTime * 8);
        }

 
    }
}
