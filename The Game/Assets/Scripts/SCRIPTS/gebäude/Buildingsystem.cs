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
            print(Global.Buildingrotation);
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
        World_pos = Get_World_Postion(World);

        int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        int Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);

        if (Global.buildmoide == 2 && Global.Mine_Focus.Mine_Can_build(X, Y))
        {
            for (int r = 0; r < Global.Mine_Focus.GrösseX; r++)
            {
                for (int t = 0; t < Global.Mine_Focus.GrösseY; t++)
                {
                    int F = t, G = r;
                    if (Global.Buildingrotation == 90)
                    {
                        G = -t;
                        F = r;
                    }
                    if (Global.Buildingrotation == 180)
                    {
                        G = -r;
                        F = -t;
                    }
                    if (Global.Buildingrotation == 270)
                    {
                        G = t;
                        F = -r;
                    }
                    if (Global.Mine_Focus.Mine_Can_build(X + G, Y + F) == false) { return; }
                }
            }
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
                    Map.Map_Rohstoffe[System.Convert.ToInt16(X + G), System.Convert.ToInt16(Y + F)] = 350;
                }
            }
            Map.Map_Rohstoffe[System.Convert.ToInt16(X), System.Convert.ToInt16(Y)] = 399;
            // sicher heit ein bauen
        }
        else if (Global.buildmoide == 1 && straße.singleton.Passt(World_pos))
        {
            Map.Map_Rohstoffe[X, Y] = 1710;
        }
    }
    void Switch()
    {
        Global.buildmoide = 2;
    }
    private Vector3 Get_World_Postion(Vector3 world)
    {
        Vector3 position;
        position.x = Mathf.Floor(world.x);
        position.y = Mathf.Floor(world.y);
        position.z = Mathf.Floor(world.z);
        return position;

    }
    public void hm( Vector3 World)  //Namme muss verbesser werden ZK Otto
    {
        World_pos = Get_World_Postion(World);
 
        X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);


    }
}
