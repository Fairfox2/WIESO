using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miene : MonoBehaviour
{
    public static Miene singelton { set; get; }
    [SerializeField]Transform t;
    [SerializeField] Transform trans;
    public void Awake()
    {
        singelton = this;
    }
    public void Mine_setzen(Grid_opjekt Objekt, int X, int Y)
    {
        if ((Objekt.canBuild() == true))
        {
            Objekt.Mine = trans;
            Objekt.Building_placed = true;
        }
    }
}
