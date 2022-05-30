using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Buildingsystem : MonoBehaviour
{
    // Variablen 
    Vector3 World_pos;
    public static Buildingsystem singleton { set; get; }
    private void Awake()
    {
        singleton = this;
    }
    public void R(float x, float y, Vector3 World)
    {
        World_pos = Get_World_Postion(World);


        int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map); // X und y Kordinate berechnen
        int Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);

        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        if (Mouse.current.leftButton.isPressed) // strasse setzen
        {
            Map.Map_Rohstoffe[X, Y] = 1710;
        }


    }
    private Vector3 Get_World_Postion(Vector3 world)
    {
        Vector3 position;
        position.x = Mathf.Floor(world.x);
        position.y = Mathf.Floor(world.y);
        position.z = Mathf.Floor(world.z);
        return position;

    }

}
