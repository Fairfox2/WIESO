using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class straße : MonoBehaviour
{
    public static straße singleton { set; get; }
    // strasse
    public Transform Straße_gsetzt;
    public Transform Straße_passtnicht;
    public Transform StraßeKurve;
    public Transform Straßesplit;
    public Transform StraßeKreuzung;
    public Transform Straße_idel;
    private CameraControlsAktion cameraActions;
    private InputAction movement;
    private Transform cameraTransform;

    [SerializeField] Transform b;

    //last 
    private Vector3 last_pos = Vector2.right;
    

    Vector3 World_pos;
    private void Awake()
    {
        singleton = this;
        cameraActions = new CameraControlsAktion();
        cameraTransform = this.GetComponentInChildren<Camera>().transform;
    }
    public void hm( float x, float y, Vector3 World) 
    {
        int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map); ;
        int Y= System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map); ;
        if (World_pos == Get_World_Postion(World))
        {
            if (Mouse.current.leftButton.isPressed) // strasse setzen
            {
                print("strasse wurde gesetzt");
                Map.Map_Rohstoffe[X, Y] = 1710;
            }
            return;
        }
        else if (World_pos != null)
        {
            X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
            Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);
            if(Map.Map_Rohstoffe[X, Y] != 1710)Map.Map_Rohstoffe[X, Y] = 1799;
            World_pos = Get_World_Postion(World);

            X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
            Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);

            if (Map.Map_Rohstoffe[X, Y] != 1710) Map.Map_Rohstoffe[X, Y] = 1700;
            Keyboard kb = InputSystem.GetDevice<Keyboard>();
            if (Mouse.current.leftButton.isPressed) // strasse setzen
            {
                Map.Map_Rohstoffe[X, Y] = 1710;
            }
        }
        else
        {
            World_pos = Get_World_Postion(World);
        }

    } 
    private Vector3 Get_World_Postion(Vector3 world)
    {
        Vector3 position;
        position.x = Mathf.Floor(world.x) ;
        position.y = Mathf.Floor(world.y);
        position.z = Mathf.Floor(world.z) ;
        return position;

    }

    private void Update()
    {
        
    }
    public void Straße_setzen(Grid_opjekt Objekt, int X, int Y)
    {
        if (Map.Map_Rohstoffe[X, Y] >= 1700 && Map.Map_Rohstoffe[X, Y] < 1800)   // ? # ?
        {                           //   ?
            Strasse_licht(Objekt, 90);
            //    ?
            if ((Map.Map_Rohstoffe[X + 1, Y] >= 1700 && Map.Map_Rohstoffe[X + 1, Y] < 1800)) // '# # ?
            {
                if ((Map.Map_Rohstoffe[X - 1, Y] >= 1700 && Map.Map_Rohstoffe[X - 1, Y] < 1800))
                {
                    Strasse_licht(Objekt, 180);
                    if ((Map.Map_Rohstoffe[X, Y + 1] >= 1700 && Map.Map_Rohstoffe[X, Y + 1] < 1800))
                    {
                        Strasse_split(Objekt,180); // W

                        if ((Map.Map_Rohstoffe[X, Y - 1] >= 1700 && Map.Map_Rohstoffe[X, Y - 1] < 1800))
                        {
                            Strasse_Kreuzung(Objekt);
                        }

                    }
                    else if ((Map.Map_Rohstoffe[X, Y - 1] >= 1700 && Map.Map_Rohstoffe[X, Y - 1] < 1800))
                    {
                        Strasse_split(Objekt, 90); // W
                    }
                    else
                    {
                        Strasse_licht(Objekt, 90);
                    }

                }
                else if ((Map.Map_Rohstoffe[X, Y + 1] >= 1700 && Map.Map_Rohstoffe[X, Y + 1] < 1800))
                {

                    Strasse_Kurve(Objekt, 180);
                }
                else if (Map.Map_Rohstoffe[X, Y - 1] >= 1700 && Map.Map_Rohstoffe[X, Y - 1] < 1800)
                {
                    Strasse_Kurve(Objekt, 180);
                }
                else
                {
                    Strasse_ende(Objekt, 180);
                }
            }
            else
            {
                Strasse_licht(Objekt, 90);
            }

        }
        if(Map.Map_Rohstoffe[X, Y] == 1799)
        {
            Objekt.Rohstoffe_ID = Objekt.Fix_Rohstoffe_ID;
        }



    }
    private void Strasse_licht(Grid_opjekt Objekt, int winkel)
    {
        if(Objekt.canBuild())
        {
            Objekt.streed = Straße_gsetzt;
        }
        else
        {
            Objekt.not_plaecd = Straße_idel;
        }

        Objekt.Setrotation(winkel);
    }
    private void Strasse_Kurve(Grid_opjekt Objekt, int winkel)
    {
        if (Objekt.canBuild()) // ssssssss
        {
            Objekt.streed = StraßeKurve;
        }
        else
        {
            Objekt.not_plaecd = Straße_idel;
        }

        Objekt.Setrotation(winkel);
    }
    private void Strasse_Kreuzung(Grid_opjekt Objekt)
    {
        if (Objekt.canBuild()) // sssssssssssssssssss
        {
            Objekt.streed = StraßeKreuzung;
        }
        else
        {
            Objekt.not_plaecd = Straße_idel;
        }

        Objekt.Set_Rotation_Random();
    }
    private void Strasse_split(Grid_opjekt Objekt, int winkel)
    {
        if (Objekt.canBuild()) // ssssssss
        {
            Objekt.streed = Straßesplit;
        }
        else
        {
            Objekt.not_plaecd = Straße_idel;
        }

        Objekt.Setrotation(winkel);
    }
    private void Strasse_ende(Grid_opjekt Objekt,int winkel)
    {
        if (Objekt.canBuild()) // sssssssssssssssssss
        {
            Objekt.streed = Straße_gsetzt;
        }
        else
        {
            Objekt.not_plaecd = Straße_idel;
        }

        Objekt.Setrotation(winkel);
    }
}
