using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Building",menuName ="Building/Mine")]
public class Miene : ScriptableObject
{
    public static Miene singelton { set; get; }
    [SerializeField]Transform Building;
    [SerializeField] Transform trans;
    public int ID;
    public int Rotation;
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
    public bool getcourser(Vector3 World)
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
            return true;
        }
        return false;
    }
    public bool Mine_Can_build(int X,int Y)
    {
        int x2 = 0, x1 = 0, y1 = 0, y2 = 0;
        int sum = 0;
        for (int x = 0; x < GrösseX; x++)
        {
            for (int y = 0; y < GrösseY; y++)
            {
                if (Map.Map_Rohstoffe[X + x, Y] >= 300 && Map.Map_Rohstoffe[X + x, Y] < 400)
                {
                    x2 = 1;
                }
                if (Map.Map_Rohstoffe[X - x, Y] >= 300 && Map.Map_Rohstoffe[X - x, Y] < 400)
                {
                    x1 = 1;
                }
                if (Map.Map_Rohstoffe[X, Y + y] >= 300 && Map.Map_Rohstoffe[X, Y + y] < 400)
                {
                    y2 = 1;
                }
                if (Map.Map_Rohstoffe[X, Y - y] >= 300 && Map.Map_Rohstoffe[X, Y - y] < 400)
                {
                    y1 = 1;
                }
               
                if (Map.Map_Rohstoffe[X, Y] >= 300 && Map.Map_Rohstoffe[X, Y] < 400)
                {
                    sum +=  + x2 + x1 + y1 + y2;
                }
            }
        }    
   
 
        if (sum >= 3 * (GrösseX * GrösseY) ) // formel zur berechnug der nachberteile 
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
