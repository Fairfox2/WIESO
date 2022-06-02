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
            
            Vector3 Target1 = ray.GetPoint(distance);
            Target1.y = 3;
            Target1.z = Mathf.Floor(Target1.z);
            Target1.x = Mathf.Floor(Target1.x);
            if (Global.buildmoide == 1)
            {
                if (transform.Find("curser"))
                {
                    DestroyImmediate(transform.Find("curser").gameObject);
                }
                Transform courser = new GameObject("curser").transform;
                courser.parent = transform;
                courser.position = transform.position;

                Mine = go;
                newr = Instantiate(Mine, transform.position, Quaternion.Euler(0, 0, 0)) as Transform;
                newr.parent = courser; 
            }
            if (Global.buildmoide == 2)
            {

                if (transform.Find("curser"))                               //durch das ständige löschen sicher noch bugs aufgeb zukunfts Otto
                {
                    DestroyImmediate(transform.Find("curser").gameObject);
                }
                Transform courser = new GameObject("curser").transform;
                courser.parent = transform;
                courser.position = transform.position;

                Mine = go;
                for (int x = 0; x < Global.Mine_Focus.GrösseX; x++)
                {
                    for (int y = 0; y < Global.Mine_Focus.GrösseY; y++)
                    {
                        int safex = x;
                        if (Global.Buildingrotation == 90)
                        {
                            x = -y;
                            y = safex;
                        }
                        if (Global.Buildingrotation == 180)
                        {
                            x = -x;
                            y = -y;
                        }
                        if (Global.Buildingrotation == 270)
                        {
                            x = y;
                            y = -safex;
                        }
                        if (Global.Mine_Focus.getcourser(new Vector3(ray.GetPoint(distance).x+x,0,ray.GetPoint(distance).z+y)))
                        {
                            Mine = passt;
                        }
                        else
                        {
                            Mine = passtnicht;
                        }
                        if(Mine != null)newr = Instantiate(Mine, new Vector3(transform.position.x+x,transform.position.y,transform.position.z +y), Quaternion.Euler(0, 0, 0)) as Transform;
                        if(newr != null)newr.parent = courser;
                    }
                }
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
        }

 
    }
}
