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
            Objekt.Building_placed = true;
            Objekt.Setrotation(Rotation, false);
        } 
        if(Setzen == true)
        {
            Objekt.Building = trans;
        }
    }
    override public Transform getcourser(Vector3 World,int x,int y)
    {
        Vector3 World_pos = Buildingsystem.singleton.Get_World_Postion(World);
        int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        int Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);
        int x2 = 0, x1 =0, y1 = 0, y2 = 0;

        int sum = 0;

        if (Plase[(x * (GrösseY)) + y] == 1) // prüfe ob es grass
        {
            if (Rohstoffe.singleton.Biom_test(Map.Map_Rohstoffe[X, Y], 1))
            {
                return Courser_straße_passt;
            }
            return Courser_straße_passt_nicht;
            //return Courser_straße_passt_nicht;
        }
        if (Plase[(x * (GrösseY)) + y] == 2)
        {
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X +1, Y], 1,1))
            {
                x2 = 1;
            }
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X - 1, Y], 1, 1))
            {
                x1 = 1;
            }
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X , Y+1], 1, 1))
            {
                y2 = 1;
            }
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X , Y-1], 1, 1))
            {
                y1 = 1;
            }
            if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X, Y ], 1, 1))
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

            
        }
    
        return CourserPasst;
    }
    override public bool Can_build(Vector3 World)
    {
        Vector3 World_pos = Buildingsystem.singleton.Get_World_Postion(World);
        int X = System.Convert.ToInt32(World_pos.x - 8 + Map.halbe_map);
        int Y = System.Convert.ToInt32(World_pos.z - 8 + Map.halbe_map);

        int x2 = 0, x1 = 0, y1 = 0, y2 = 0;

        int sum = 0;

        for (int x = 0; x < GrösseX; x++)
        {
            for (int y = 0; y < GrösseY; y++)
            {

                if (Plase[(x * (GrösseY)) + y] == 1) // prüfe ob es grass
                {
                    if (!Rohstoffe.singleton.Biom_test(Map.Map_Rohstoffe[X, Y], 1))
                    {
                        return false;
                    }
                    
                    //return Courser_straße_passt_nicht;
                }
                if (Plase[(x * (GrösseY)) + y] == 2)
                {
                    if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X + 1, Y], 1, 1))
                    {
                        x2 = 1;
                    }
                    if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X - 1, Y], 1, 1))
                    {
                        x1 = 1;
                    }
                    if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X, Y + 1], 1, 1))
                    {
                        y2 = 1;
                    }
                    if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X, Y - 1], 1, 1))
                    {
                        y1 = 1;
                    }
                    if (Rohstoffe.singleton.BiomRostoff_test(Map.Map_Rohstoffe[X, Y], 1, 1))
                    {
                        sum = +x2 + x1 + y1 + y2;
                    }
                    if (sum != 3)
                    {
                        return false;
                    }
                    
                }
                if (Plase[(x * (GrösseY)) + y] == 3) // prüfe ob es strasse ist 
                {
                    if (straße.singleton.Passt(new Vector3(X, 3, Y)))
                    {
                        return false;
                    }
                    
                }
            }
        }
 

        return true;
    }


}


[CustomEditor(typeof(Miene))]
class MieneEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Miene building = (Miene)target;

        var style = new GUIStyle(GUI.skin.button);
        while (building.Plase.Count <= building.GrösseX * building.GrösseY + 1)
        {
            building.Plase.Add(0);
        }
        while (building.Plase.Count > building.GrösseX * building.GrösseY + 1)
        {
            building.Plase.RemoveAt(building.Plase.Count - 1);
        }
        for (int i = 0; i < building.GrösseX; i++)
        {
            GUILayout.BeginHorizontal();
            for (int s = 0; s < building.GrösseY; s++)
            {

                GUI.backgroundColor = Color.black;
                if (building.Plase[(i * (building.GrösseY)) + s] == 1)
                {
                    GUI.backgroundColor = Color.green;
                }
                if (building.Plase[(i * (building.GrösseY)) + s] == 2)
                {
                    GUI.backgroundColor = Color.red;
                }
                if (building.Plase[(i * (building.GrösseY)) + s] == 3)
                {
                    GUI.backgroundColor = Color.gray;
                }

                if (GUILayout.Button(" ", style, GUILayout.Height(100)))
                {

                    building.Plase[(i * (building.GrösseY)) + s] = 1 + building.Plase[(i * (building.GrösseY)) + s];
                    if (building.Plase[(i * (building.GrösseY)) + s] == 4)
                    {
                        building.Plase[(i * (building.GrösseY)) + s] = 0;
                    }
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}






