using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class straße : MonoBehaviour
{
    public static straße singleton { set; get; }
    bool Rohstoff = true;
    bool random = true;
    // strasse
    public Transform Straße_licht;
    public Transform Straße_passtnicht;
    public Transform StraßeKurve;
    public Transform Straßesplit;
    public WeightedRandomList<Transform> Kreuzung;
    public Transform Straße_idel;
    private CameraControlsAktion cameraActions;
    private Bauen BuildingsystemsAktions;
    private InputAction movement;
    private Transform cameraTransform;

    [SerializeField] Transform b;

    //last 
    private Vector3 last_pos = Vector2.right;

    private int X = 0;
    private int Y = 0;

    Vector3 World_pos;
    private void Awake()
    {
        singleton = this;
        cameraActions = new CameraControlsAktion();
       
    }
   
    public bool Passt(Vector3 World)
    {
        Vector3 World_pos = Get_World_Postion(World);
        int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        int Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);
        if (Map.Map_Rohstoffe[X,Y] == 00100000000)                 //MIte boden machen 
        {
            return true;
        }
        return false;
    }
    private Vector3 Get_World_Postion(Vector3 world)
    {
        Vector3 position;
        position.x = Mathf.Floor(world.x);
        position.y = Mathf.Floor(world.y);
        position.z = Mathf.Floor(world.z);
        return position;

    }

    private void Update()
    {

    }
    public void Straße_setzen(Grid_opjekt Objekt, int X, int Y,bool random)
    {
        int Rohstoff = 000010000;
        

        if (Rohstoffe.singleton.Rohstoff_test(Map.Map_Rohstoffe[X - 1, Y], Rohstoff))   // ? # ?
        {                           //   ?
            Objekt.Building_placed = true;
            Strasse_licht(Objekt, 90);
            print("streed");

            int x2 = 0, y2 = 0, x = 0, y = 0;
            if (Rohstoffe.singleton.Rohstoff_test(Map.Map_Rohstoffe[X - 1, Y], Rohstoff))
            {
                x2 = 1;
            }
            if (Rohstoffe.singleton.Rohstoff_test(Map.Map_Rohstoffe[X + 1, Y], Rohstoff))
            {
                x = 1;
            }
            if (Rohstoffe.singleton.Rohstoff_test(Map.Map_Rohstoffe[X , Y-1], Rohstoff))
            {
                y2 = 1;
            }
            if (Rohstoffe.singleton.Rohstoff_test(Map.Map_Rohstoffe[X, Y +1], Rohstoff))
            {
                y = 1;
            }
            int sum = x + y + x2 + y2;
            if (sum == 4)
            {
                Strasse_Kreuzung(Objekt, 90);
            }
            if (sum == 3)
            {
                if (x == 1 && x2 == 1 && y == 1)
                {
                    Strasse_split(Objekt, 0);
                }
                if (x == 1 && y2 == 1 && y == 1)
                {
                    Strasse_split(Objekt, 90);
                }
                if (x == 1 && x2 == 1 && y2 == 1)
                {
                    Strasse_split(Objekt, 180);
                }
                if (y2 == 1 && x2 == 1 && y == 1)
                {
                    Strasse_split(Objekt, 270);
                }
            }
            if (sum == 2)
            {
                if (x == 1 && x2 == 1)
                {
                    Strasse_licht(Objekt, 0);
                }
                if (y == 1 && y2 == 1)
                {
                    Strasse_licht(Objekt, 90);
                }
                if (y == 1 && x2 == 1)
                {
                    Strasse_Kurve(Objekt, 90);
                }
                if (y == 1 && x == 1)
                {
                    Strasse_Kurve(Objekt, 180);
                }
                if (y2 == 1 && x2 == 1)
                {
                    Strasse_Kurve(Objekt, 0);
                }
                if (y2 == 1 && x == 1)
                {
                    Strasse_Kurve(Objekt, 270);
                }
            }
            if (sum <= 1)
            {
                if (x == 1 | x2 == 1)
                {
                    Strasse_licht(Objekt, 0);
                }
                if (y == 1 | y2 == 1)
                {
                    Strasse_licht(Objekt, 90);
                }
            }
            
        }


        

    }
    private void Strasse_licht(Grid_opjekt Objekt, int winkel)
    {
      Objekt.streed = Straße_licht;
        

        Objekt.Setrotation(winkel);
    }
    private void Strasse_Kurve(Grid_opjekt Objekt, int winkel)
    {
   
            Objekt.streed = StraßeKurve;
        


        Objekt.Setrotation(winkel);
    }
    private void Strasse_Kreuzung(Grid_opjekt Objekt, int winkel)
    {
      
        if(random == true)
        {
            Objekt.Index_Top = Kreuzung.GetRandom();
        }
        Objekt.streed = Kreuzung.Get(Objekt.Index_Top);
        Objekt.Setrotation(winkel);
    }
    private void Strasse_split(Grid_opjekt Objekt, int winkel)
    {
  
            Objekt.streed = Straßesplit;
       
        
     

        Objekt.Setrotation(winkel);
    }
    private void Strasse_ende(Grid_opjekt Objekt, int winkel)
    {
    
            Objekt.streed = Straße_licht;
    

        Objekt.Setrotation(winkel);
    }
}