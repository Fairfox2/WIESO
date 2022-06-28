using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "new Building", menuName = "Building/Basic")]
public class Building_base : ScriptableObject
{

    [SerializeField] Transform Building;

    public Transform CourserPasstnicht;
    public Transform CourserPasst;
    public int ID;
    public int Level;
    public int Rotation;
    [SerializeField] public int GrösseX;
    [SerializeField] public int GrösseY;
    public int[,] plase;
    [SerializeField] public List<int> Plase ;
    public void Awake()
    {

    }
    virtual public void setzen(Grid_opjekt Objekt, bool Setzen)
    {

    }
    virtual public Transform getcourser(Vector3 world, int X,int Y)
    {
        return null;
    }
    virtual public bool Can_build(Vector3 world)
    {
        return true;
    }

}

