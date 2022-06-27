using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Buildingsystem : MonoBehaviour
{
    // Variablen 
    Vector3 World_pos;
    public static Buildingsystem singleton { set; get; }
    private CameraControlsAktion cameraActions;
    private Bauen BuildingsystemsAktions;
    private InputAction Builds;

    int X = 0; 
    int Y = 0;  
    private void Awake()
    {
        singleton = this;

        BuildingsystemsAktions = new Bauen();

        cameraActions = new CameraControlsAktion();
    }
    private void OnEnable()
    {
        
        BuildingsystemsAktions.Buildings.Rotate.performed += z => Rotate(z.ReadValue<Vector2>().y / 100f);
        BuildingsystemsAktions.Buildings.Rotate.Enable();
        
        BuildingsystemsAktions.Buildings.@switch.performed += agdfg => Switch();
        BuildingsystemsAktions.Buildings.@switch.Enable();

    }
    void Rotate(float inputValue)
    {

        if(Mathf.Abs(inputValue) > 0.1f && Global.buildmoide == 2)
        {

            if (inputValue >0)
            {
                Global.Buildingrotation += 90;
                if (Global.Buildingrotation >= 360)
                {
                    Global.Buildingrotation = 0;
                }
            }
            else if (inputValue < 0)
            {
                Global.Buildingrotation -= 90;
                if(Global.Buildingrotation < 0)
                {
                    Global.Buildingrotation = 270;
                }
            }

        }
    }
    public void Build1(Vector3 World)
    {
        World_pos =(World);

        int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        int Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);

        if (Global.buildmoide == 2 && Global.Mine_Focus.Can_build(World))
        {
            for (int x1 = 0; x1 < Global.Mine_Focus.GrösseX; x1++) // noch eigene funktion für schöneheit zukunfts Otto
            {
                for (int y1 = 0; y1 < Global.Mine_Focus.GrösseY; y1++)
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
                    if (Global.Mine_Focus.Plase[(x1 * (Global.Mine_Focus.GrösseY)) + y1] == 2)
                    {
                        Map.Map_Rohstoffe[System.Convert.ToInt32(X + G), System.Convert.ToInt16(Y + F)] = 0101029900;

                    }//Muss noch durch ID variable der MIne erstzt weerden ZK Otto und mach achu eine funktion drasu
                    if (Global.Mine_Focus.Plase[(x1 * (Global.Mine_Focus.GrösseY)) + y1] == 1)
                    {
                        int b1 = 0;
                        int b2 = 0;
                        int b4 = 0;
                        int b8 = 0;
                        if((x1 + 1) * (Global.Mine_Focus.GrösseY) + (y1 ) < Global.Mine_Focus.Plase.Count && 0 <= (x1 + 1))
                        {
                            if (Global.Mine_Focus.Plase[((x1 + 1) * (Global.Mine_Focus.GrösseY)) + (y1 )] == 2)
                            {
                                print("1");
                                b1 = 1;
                            }
                        }
                        if ((x1 ) * (Global.Mine_Focus.GrösseY) + (y1 + 1) < Global.Mine_Focus.Plase.Count && 0 <=  (y1 + 1))
                        {

                            if (Global.Mine_Focus.Plase[((x1 ) * (Global.Mine_Focus.GrösseY)) + (y1 + 1)] == 2)
                            {
                                print("2");
                                b2 = 1;
                            }
                        }
                        if ((x1 - 1) * (Global.Mine_Focus.GrösseY) + (y1 ) < Global.Mine_Focus.Plase.Count && 0 <= (x1 - 1) )
                        {
                            if (Global.Mine_Focus.Plase[((x1 - 1) * (Global.Mine_Focus.GrösseY)) + (y1 )] == 2)
                            {
                                print("4" + ((x1 - 1) * (Global.Mine_Focus.GrösseY)) + (y1));
                                b4 = 1;
                            }
                        }
                        
                        if( (x1 ) * (Global.Mine_Focus.GrösseY) + (y1 - 1) < Global.Mine_Focus.Plase.Count && 0 <=  (y1 - 1) )
                        {
                            if (Global.Mine_Focus.Plase[((x1 ) * (Global.Mine_Focus.GrösseY)) + (y1 - 1)] == 2)
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
                        int sum = (b1 * 1) + (b2 * 2) + (b4 * 4 )+ (b8 * 8);
                        print(Global.Buildingrotation + " summe " + sum + "kord:" + x1+ "," + y1);
                        Map.Map_Rohstoffe[System.Convert.ToInt32(X + G), System.Convert.ToInt16(Y + F)] = 100100000 + sum;

                    }
                }
            }
            Map.Map_Rohstoffe[System.Convert.ToInt16(X), System.Convert.ToInt16(Y)] = 0101020000;

            // sicher heit ein bauen
        }
        else if (Global.buildmoide == 1 && straße.singleton.Passt(World_pos))
        {
            //Map.Map_Rohstoffe[X, Y] = 100010000;
        }
        else if (Global.buildmoide == 3)
        {
            Map.Map_Rohstoffe[X, Y] = 100030000;
        }
    }
    void Switch()
    {
        Global.buildmoide ++;
        if(Global.buildmoide == 4)
        {
            Global.buildmoide = 0;
        }
    }
    public Vector3 Get_World_Postion(Vector3 world)
    {
        Vector3 position;
        position.x = Mathf.Floor(world.x + 0.5f);
        position.y = Mathf.Floor(world.y);
        position.z = Mathf.Floor(world.z + 0.5f);
        return position;

    }
    public void hm( Vector3 World)  //Namme muss verbesser werden ZK Otto
    {
        World_pos = Get_World_Postion(World);
 
        X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);


    }
}
