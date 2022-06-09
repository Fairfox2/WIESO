using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
public class Streed_gohst : MonoBehaviour
{
    public static Streed_gohst singleton { set; get; }
    [SerializeField] Transform go;
    [SerializeField] Miene s;
    [SerializeField] Transform Mine;
    [SerializeField] Transform passt;
    [SerializeField] Transform passtnicht;
    Transform newTile;
    Transform newr;
    public void Awake()
    {
        singleton = this;
        Global.Mine_Focus = s;
    }
    // Update is called once per frame
    private void Update()
    {
        
        Plane plane = new Plane(Vector3.up, Vector3.zero * 4);

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (plane.Raycast(ray, out float distance))
        {
            if (transform.Find("curser"))
            {
                DestroyImmediate(transform.Find("curser").gameObject);
            }
            Transform courser = new GameObject("curser").transform;
            courser.parent = transform;
            courser.position = transform.position;

            Vector3 Target1 = ray.GetPoint(distance);
            Target1.y = 3;
            Target1.z = Mathf.Floor(Target1.z);
            Target1.x = Mathf.Floor(Target1.x);
            if (Global.buildmoide == 1)
            {
        

                if (straﬂe.singleton.Passt(ray.GetPoint(distance)))
                {
                    Mine = passt;
                }
                else
                {
                    Mine = passtnicht;
                }
                if(Mine != null)newr = Instantiate(Mine, transform.position, Quaternion.Euler(0, 0, 0)) as Transform;
                if(newr != null)newr.parent = courser; 
            }
            if (Global.buildmoide == 2)
            {

             

                Mine = go;
                for (float x = 0; x < Global.Mine_Focus.GrˆsseX; x++) // float da minus zahlen
                {
                    for (float y = 0; y < Global.Mine_Focus.GrˆsseY; y++)
                    {
                        float F=y, G = x;
                        if (Global.Buildingrotation == 90)
                        {
                            G = -y;
                            F = x;
                        }
                        if (Global.Buildingrotation == 0)
                        {
                            G = -x;
                            F = -y;
                        }
                        if (Global.Buildingrotation == 270)
                        {
                            G = y;
                            F = -x;
                        }
                        if (Global.Mine_Focus.getcourser(new Vector3(ray.GetPoint(distance).x+G,0,ray.GetPoint(distance).z+F)))
                        {
                            Mine = Global.Mine_Focus.CourserPasst;
                        }
                        else
                        {
                            Mine = Global.Mine_Focus.CourserPasstnicht;
                        }
                        if(Mine != null)newr = Instantiate(Mine, new Vector3(transform.position.x+G,transform.position.y,transform.position.z +F), Quaternion.Euler(0, Global.Buildingrotation, 0)) as Transform;
                        if(newr != null)newr.parent = courser;
                    }
                }
            }
            if(Global.buildmoide == 3)
            {
                if (straﬂe.singleton.Passt(ray.GetPoint(distance)))
                {
                    Mine = passt;
                }
                else
                {
                    Mine = passtnicht;
                }
                if (Mine != null) newr = Instantiate(Mine, new Vector3(transform.position.x , transform.position.y, transform.position.z ), Quaternion.Euler(0, Global.Buildingrotation, 0)) as Transform;
                if (newr != null) newr.parent = courser;
            }
            if (Global.buildmoide == 0)
            {
                if (transform.Find("curser"))
                {
                    DestroyImmediate(transform.Find("curser").gameObject);
                }
                Mine = null;
            }
            
            transform.position = Vector3.Lerp(transform.position, Target1, Time.deltaTime * 8);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Global.Buildingrotation,0), Time.deltaTime * 8);
        }

 
    }
}
