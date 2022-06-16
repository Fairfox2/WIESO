using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="new Building",menuName ="Building/Mine")]
public class Miene : Building_base
{
    public static Miene singelton { set; get; }
    [SerializeField]Transform Building;
    [SerializeField] Transform trans;

    [SerializeField] Transform Courser_passt;
    [SerializeField] Transform Courser_passt_nicht;

    [SerializeField] Transform Courser_straße_passt;
    [SerializeField] Transform Courser_straße_passt_nicht;

    [SerializeField] Transform Courser_Building_passt;
    [SerializeField] Transform Courser_Building_passt_nicht;

    int Resurce = 00010100000;




    public void Awake()
    {
        singelton = this;
       
    }
    override public void setzen(Grid_opjekt Objekt,bool Setzen)
    {

        Objekt.Rohstoff = null;
        if (Objekt.Building_placed != true)     // fals es zum esrsten mal plziert wwird rotaion setzen 
        {
            Rotation = Global.Buildingrotation + 90;
            Objekt.Building_placed = true;// Muss alls erstes pasieren sonst wird die rotaion nicht gesetzz
            Objekt.Setrotation(Rotation, false);
        } 
        if(Setzen == true)
        {
            Objekt.Building = trans;
        }
        
    }
    override public Transform getcourser(Vector3 World,int x,int y)
    {
        Vector3 World_pos = Get_World_Postion(World);
        int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        int Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);
        int x2 = 0, x1 =0, y1 = 0, y2 = 0;

        int sum = 0;

        if (Plase[(x * (GrösseY)) + y] == 1) // prüfe ob es grass
        {
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X, Y], 00010000000))
            {
                return Courser_straße_passt;
            }
            return Courser_straße_passt_nicht;
            //return Courser_straße_passt_nicht;
        }
        if (Plase[(x * (GrösseY)) + y] == 2)
        {
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X +1, Y], 00010100000))
            {
                x2 = 1;
            }
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X - 1, Y], 00010100000))
            {
                x1 = 1;
            }
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X , Y+1], 00010100000))
            {
                y2 = 1;
            }
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X , Y-1], 00010100000))
            {
                y1 = 1;
            }
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X, Y + 1], 00010100000))
            {
                sum = +x2 + x1 + y1 + y2;
            }
            if (sum == 3)
            {
                return Courser_Building_passt;
            }
            return Courser_Building_passt_nicht;
        }
        if (Plase[(x * (GrösseY)) + y] == 3) // prüfe ob es strasse ist 
        {
            if (!straße.singleton.Passt(new Vector3(X, 3, Y)))
            {
                return Courser_straße_passt;
            }
            return Courser_straße_passt_nicht;
        }
    
        return CourserPasst;
    }
    override public bool Mine_Can_build(int X,int Y)
    {
       
        for (int x = 0; x < Global.Mine_Focus.GrösseX; x++) // float da minus zahlen
        {
            for (int y = 0; y < Global.Mine_Focus.GrösseY; y++)
            {
                int F = x, G = y;

                if (Global.Buildingrotation == 90)
                {
                    G =  - y;
                    F = x;
                }
                if (Global.Buildingrotation == 0)
                {
                    G =  - x;
                    F = - y;
                }
                if (Global.Buildingrotation == 270)
                {
                    G = y;
                    F =  - x;
                }

                if (Plase[(x * (GrösseY)) + y] == 1) // prüfe ob es grass
                {
                    
                    if(Map.Map_Rohstoffe[X + F, Y + G] == 00010000000) // hajskdghfucjuaaaaaaaaaa help
                    {
                        return false;
                    }
                   

                }
                else if (Plase[(x * (GrösseY)) + y] == 2)
                {
                    
                    int x2 = 0, x1 = 0, y1 = 0, y2 = 0;
                    int sum = 0;
                    if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X + F+1, Y + G ], 00010100000))
                    {
                        x2 = 1;
                    }
                    if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X + F-1, Y + G  ], 00010100000))
                    {
                        x1 = 1;
                    }
                    if( Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X + F, Y + G - 1], 00010100000))
                    {
                        y2 = 1;
                    }
                    if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X + F, Y + G + 1], 00010100000))
                    {
                        y1 = 1;
                    }
                    if (Map.Map_Rohstoffe[X+F, Y+G] >= 300 && Map.Map_Rohstoffe[X + F,Y+ G] < 00010100000)
                    {
                        
                        sum = x2 + x1 + y1 + y2;

                    }
                    if(sum != 3)
                    {
                        return false;
                    }

                }
                else if (Plase[(x * (GrösseY)) + y] == 3) // prüfe ob es strasse ist 
                {
                    if (!straße.singleton.Passt(new Vector3(X, 3, Y)))
                    {
                        return false;
                    }
                }

            }
        }
        return true;
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

[CustomEditor(typeof(Miene))]
class MieneEditor: Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Miene Mine = (Miene)target;

        var style = new GUIStyle(GUI.skin.button);
        while (Mine.Plase.Count <=  Mine.GrösseX  * Mine.GrösseY +1)
        {
            Mine.Plase.Add(0);
        }
        while (Mine.Plase.Count > Mine.GrösseX * Mine.GrösseY  +1)
        {
            Mine.Plase.RemoveAt(Mine.Plase.Count-1);
        }
        for (int i = 0; i < Mine.GrösseX; i++)
        {
            GUILayout.BeginHorizontal();
            for (int s = 0; s < Mine.GrösseY; s++)
            {
                
                GUI.backgroundColor = Color.black;
                if (Mine.Plase[(i * (Mine.GrösseY)) + s] == 1)
                {
                    GUI.backgroundColor = Color.green;
                }
                if (Mine.Plase[(i * (Mine.GrösseY)) + s] == 2)
                {
                    GUI.backgroundColor = Color.red;
                }
                if (Mine.Plase[(i * (Mine.GrösseY)) + s] == 3)
                {
                    GUI.backgroundColor = Color.gray;
                }
                
                if (GUILayout.Button(" ", style, GUILayout.Height(100)))
                {

                    Mine.Plase[(i * (Mine.GrösseY)) + s] = 1+ Mine.Plase[(i * (Mine.GrösseY)) + s];
                    if (Mine.Plase[(i * (Mine.GrösseY)) + s] == 4)
                    {
                        Mine.Plase[(i * (Mine.GrösseY)) + s] = 0;
                    }
                    
                }
                
            }
            GUILayout.EndHorizontal();
        }
      
        
    }

}
