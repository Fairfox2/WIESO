using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_opjekt
{

    [SerializeField] public Transform Rohstoff;
    [SerializeField] public Transform Boden;
    [SerializeField] public Transform Building;
    [SerializeField] public Transform streed;

    long[] ID = new long[2];

    //index
    public int Index_Top = 0;       // dieser Index zählt für Building streed and Rohstoff
    public int Index_Boden = 0;

    private int Rohstoffe_ID = 1000;
    private int Boden_ID = 1000;

    public int Biom;
    public int Art_Top;
    public int Art_Boden;
    public int Building_Top;
    public int Building_Boden;
    public int Zusatz_Top;
    public int Zusatz_Boden;
    public int Rotation_Top = 0;
    public int Rotation_Boden = 0;
    public int zusatz_Top = 0;
    public int zusatz_Boden = 0;

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
        else if (Rohstoff == true|| Building == true || streed == true ||Building_placed == true)
        {
            return false;
        }
        else
        {
            return false;
        }

    }
    public void Setrotation(int Winkel)
    {
        if(Building_placed == true)
        {
            Rotation_Top = Winkel;
            return;
        }
        Rotation_Boden = Winkel;
    }
    public void Set_Rotation_Random()
    {
        if (Building_placed == true)
        {
            Rotation_Top = 90 * Random.Range(0, 4);
            return;
        }
        Rotation_Boden = 90 * Random.Range(0, 4);

    }


    public void Id_Load(long Id)
    {
        
        ID[1] = Id;
        Rotation_Top = System.Convert.ToInt16(Id / 100000000) * 90;     
        Biom = System.Convert.ToInt16((Id% 100000000) / 10000000);
        Art_Top = System.Convert.ToInt16((Id% 10000000) / 100000);
        Building_Top = System.Convert.ToInt16((Id% 100000) / 10000);
        Index_Top = System.Convert.ToInt16((Id% 10000) / 100);
        Zusatz_Top = System.Convert.ToInt16((Id % 100));
    }
    public long[] Create_ID()
    {
        ID[0] = Rotation_Boden * 100000000 + Biom * 10000000 + Art_Boden * 100000 + Building_Boden * 10000 + Index_Boden*100 + Zusatz_Boden;
        ID[1] = Rotation_Top * 100000000 + Biom * 10000000 + Art_Top * 100000 + Building_Top * 10000 + Index_Top *100 + Zusatz_Top;

        return ID;
    }
    public void Id_Boden(long Id)
    {
        ID[0] = Id;
        Rotation_Boden = System.Convert.ToInt16(Id / 1000000000) * 90;
        Biom = System.Convert.ToInt16((Id % 100000000) / 10000000);
        Art_Boden = System.Convert.ToInt16((Id % 10000000) / 100000);
        Building_Boden = System.Convert.ToInt16((Id % 100000) / 10000);
        Index_Boden = System.Convert.ToInt16((Id % 10000) / 100);
        Zusatz_Boden = System.Convert.ToInt16((Id % 100) );
    }


}
