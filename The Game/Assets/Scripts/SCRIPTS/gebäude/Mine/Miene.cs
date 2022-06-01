using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miene : MonoBehaviour
{
    public static Miene singelton { set; get; }
    [SerializeField]Transform t;
    [SerializeField] Transform trans;

    [SerializeField] public int GrösseX;
    [SerializeField] public int GrösseY;
    public void Awake()
    {
        singelton = this;
    }
    public void Mine_setzen(Grid_opjekt Objekt, int X, int Y)
    {
        if ((Objekt.Rohstoffe_ID >= 300 || Objekt.Rohstoffe_ID < 400))
        {
            Objekt.Mine = trans;
            Objekt.Building_placed = true;
        }
    }
    public Transform getcourser(Vector3 World)
    {
        Vector3 World_pos = Get_World_Postion(World);
        int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        int Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);
        int x = 0, x1 =0, y1 = 0, y = 0;
        
        if (Map.Map_Rohstoffe[X +1, Y] >= 300 && Map.Map_Rohstoffe[X+1 , Y] < 400)
        {
            x = 1;
        }
        if (Map.Map_Rohstoffe[X-1, Y] >= 300 && Map.Map_Rohstoffe[X-1, Y] < 400)
        {
            x1 = 1;
        }
        if (Map.Map_Rohstoffe[X, Y+1] >= 300 && Map.Map_Rohstoffe[X, Y+1] < 400)
        {
            y = 1;
        }
        if (Map.Map_Rohstoffe[X, Y-1] >= 300 && Map.Map_Rohstoffe[X, Y-1] < 400)
        {
            y1 = 1;
        }
        int sum = 0;
        if (Map.Map_Rohstoffe[X, Y] >= 300 && Map.Map_Rohstoffe[X, Y] < 400)
        {
            sum = x + x1 + y1 + y;
        }
        if(sum == 3)
        {
            return t;
        }
        return trans;
    }
    public bool Mine_Can_build(int X,int Y)
    {
        int x = 0, x1 = 0, y1 = 0, y = 0;

        if (Map.Map_Rohstoffe[X + GrösseX, Y] >= 300 && Map.Map_Rohstoffe[X + GrösseX, Y] < 400)
        {
            x = 1;
        }
        if (Map.Map_Rohstoffe[X - GrösseX, Y] >= 300 && Map.Map_Rohstoffe[X - GrösseX, Y] < 400)
        {
            x1 = 1;
        }
        if (Map.Map_Rohstoffe[X, Y + GrösseY] >= 300 && Map.Map_Rohstoffe[X, Y + GrösseY] < 400)
        {
            y = 1;
        }
        if (Map.Map_Rohstoffe[X, Y - GrösseY] >= 300 && Map.Map_Rohstoffe[X, Y - GrösseY] < 400)
        {
            y1 = 1;
        }
        int sum = 0;
        if (Map.Map_Rohstoffe[X, Y] >= 300 && Map.Map_Rohstoffe[X, Y] < 400)
        {
            sum = x + x1 + y1 + y;
        }
        if (sum == 3)
        {
            return true;
        }
        return false;
        print("Du kannst hier nicht báuen");

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
