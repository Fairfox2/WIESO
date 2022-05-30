using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_opjekt
{
    public int Rohstoffe_ID = 1000;
    public int Fix_Rohstoffe_ID = 1000;
    public bool buildmode = false;
    public bool Gebäude = false;
    [SerializeField] public Transform Rohstoff;
    [SerializeField] public Transform Boden;
    [SerializeField] public Transform Building;
    [SerializeField] public Transform Mine;
    public Transform streed;
    public Transform not_plaecd;
    public bool boden_Rohstoff = false;
    public bool rohstoff = false;
    public bool Building_placed = false;
    public float value = 2;
    public float Element_hight = 2;
    public float Building_Winkel = 0;
    public int rot = 0;
    public int asrot = 0;
    public bool canBuild()
    {
        if (rohstoff == false && boden_Rohstoff == false && Building_placed == false)
        {
            return true;
        }
        else if (rohstoff || boden_Rohstoff || Building_placed)
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
            asrot = Winkel;
            return;
        }
        rot = Winkel;
    }
    public void Set_Rotation_Random()
    {
        if (Building_placed == true)
        {
            asrot = 90 * Random.Range(0, 4);
            return;
        }
        rot = 90 * Random.Range(0, 4);

    }

}
