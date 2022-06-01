using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
public class Streed_gohst : MonoBehaviour
{
    public static Streed_gohst singleton { set; get; }
    [SerializeField] Transform go;
    [SerializeField] Transform Mine;
    Transform newTile;
    Transform newr;
    public void Awake()
    {
        singleton = this;
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


                if (transform.Find("curser"))
                {
                    DestroyImmediate(transform.Find("curser").gameObject);
                }
                

                
                Transform courser = new GameObject("curser").transform;
               
                   
                courser.parent = transform;
                courser.position = transform.position;
                Mine = Miene.singelton.getcourser(ray.GetPoint(distance));
                newr = Instantiate(Mine, transform.position, Quaternion.Euler(0, 0, 0)) as Transform;
                newr.parent = courser;
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
