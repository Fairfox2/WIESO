using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Lager : MonoBehaviour
{
    public static Lager singelton { set; get; }
    [SerializeField] Transform Building;
    [SerializeField] Transform lager;
    public Transform CourserPasstnicht;
    public Transform CourserPasst;
    public int ID;
    public int Rotation;
    public int GrösseX;
    public int GrösseY;
    public void Awake()
    {
        singelton = this;

    }
    public void Lager_setzen(Grid_opjekt Objekt, bool Setzen)
    {

        Objekt.Rohstoff = null;
        if (Objekt.Building_placed != true)     // fals es zum esrsten mal plziert wwird rotaion setzen 
        {
            Rotation = Global.Buildingrotation + 90;
            Objekt.Building_placed = true;// Muss alls erstes pasieren sonst wird die rotaion nicht gesetzz
            Objekt.Setrotation(Rotation,false);
        }
        if (Setzen == true)
        {
            Objekt.Building = lager;
        }

    }

    public bool Lager_Can_build(Vector3 World)
    {
        Vector3 World_pos = Get_World_Postion(World);
        int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        int Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);
        if (Map.Map_Rohstoffe[X, Y] == 100000000)
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
}



