using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_opjekt
{
    public Vector3 Position { get; set; }

    [SerializeField] public Transform Rohstoff;
    [SerializeField] public Transform Boden;
    [SerializeField] public Transform Building;
    [SerializeField] public Transform streed;

    long[] ID = new long[2];

    //index
    public int Index_Top = 0;       // dieser Index z�hlt f�r Building streed and Rohstoff
    public int Index_Boden = 0;

    private int Rohstoffe_ID = 1000;
    private int Boden_ID = 1000;
    public int lager_value;
    public int Biom;
    public int Art_Top;
    public int Art_Boden;
    public int Building_Top;
    public int Building_Boden;
    public int Zusatz_Top;
    public int Zusatz_Boden;
    public int Rotation_Top = 0;
    public int Rotation_Boden = 0;


    public int Fix_Rohstoffe_ID = 1000;
    public bool boden_Rohstoff = false;
    public bool Building_placed = false;


    public float value = 2;
    public float Element_hight = 2;
    public float Building_Winkel = 0;

    public bool canBuild()
    {
        if (Rohstoff == false && Building == false)
        {
            return true;
        }
        else if (Rohstoff == true || Building == true || streed == true || Building_placed == true)
        {
            return false;
        }
        else
        {
            return false;
        }

    }
    public void Setrotation(int Winkel, bool oden)
    {
        if (oden == false)
        {
            Rotation_Top = Winkel;
        }
        if (oden == true)
        {
            Rotation_Boden = Winkel;
        }

    }

    public void Set_Rotation_Random(bool oden)
    {

        if (oden == false)
        {
            Rotation_Top = 90 * Random.Range(0, 4);
        }
        if (oden == true)
        {
            Rotation_Boden = 90 * Random.Range(0, 4);
        }
    }

    public void Id_Load(long Id)
    {
        lager_value = System.Convert.ToInt32(Id / 1000000000000) ;
        Rotation_Top = System.Convert.ToInt32((Id% 1000000000000) / 10000000000) * 90;
        Biom = System.Convert.ToInt16((Id % 10000000000) / 100000000);
        Art_Top = System.Convert.ToInt16((Id % 100000000) / 1000000);
        Building_Top = System.Convert.ToInt16((Id % 1000000) / 10000);
        Index_Top = System.Convert.ToInt16((Id % 10000) / 100);
        Zusatz_Top = System.Convert.ToInt16((Id % 100));
    }
    public void Create_ID(int X,int Y)
    {

        Map.Map_Rohstoffe_Boden[X, Y] = (lager_value * 1000000000000) + (Rotation_Boden / 90) * 10000000000 + (Biom * 100000000) + Art_Boden * 1000000 + Building_Boden * 10000 + Index_Boden * 100 + Zusatz_Boden;
        Map.Map_Rohstoffe[X,Y] = (Rotation_Top / 90) * 10000000000 + (Biom * 100000000) + (Art_Top * 1000000) + (Building_Top * 10000) + (Index_Top * 100) + Zusatz_Top;
       
        
    }
    public void Id_Boden(long Id)
    {
        Rotation_Boden = System.Convert.ToInt32(Id / 10000000000) * 90;
        Biom = System.Convert.ToInt16((Id % 10000000000) / 100000000);
        Art_Boden = System.Convert.ToInt16((Id % 100000000) / 1000000);
        Building_Boden = System.Convert.ToInt16((Id % 1000000) / 10000);
        Index_Boden = System.Convert.ToInt16((Id % 10000) / 100);
        Zusatz_Boden = System.Convert.ToInt16((Id % 100));
    }

    public void Set_Map_ID_Boden(long Id)
    {
        Biom = System.Convert.ToInt16((Id % 10000000000) / 100000000);
        Art_Boden = System.Convert.ToInt16((Id % 100000000) / 1000000);
        Building_Boden = System.Convert.ToInt16((Id % 1000000) / 10000);
        Zusatz_Boden = System.Convert.ToInt16((Id % 100));
    }
    public void Set_Map_ID_Top(long Id)
    {
        
        Biom = System.Convert.ToInt16((Id % 10000000000) / 100000000);
        Art_Top = System.Convert.ToInt16((Id % 100000000) / 1000000);
        Building_Top = System.Convert.ToInt16((Id % 1000000) / 10000);
        Index_Top = System.Convert.ToInt16((Id % 10000) / 100);
        Zusatz_Top = System.Convert.ToInt32((Id % 100) / 1);

    }

    public void Update_World(Junk Chunck)
    {
        Chunck.Map_update(this);
    }        
}
