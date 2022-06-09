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
    public int Rotation;
    [SerializeField] public int GrösseX;
    [SerializeField] public int GrösseY;
    public void Awake()
    {

    }
    virtual public void setzen(Grid_opjekt Objekt, bool Setzen)
    {

    }
    virtual public bool getcourser(Vector3 World)
    {
        return true;
    }
    virtual public bool Mine_Can_build(int X, int Y)
    {
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


