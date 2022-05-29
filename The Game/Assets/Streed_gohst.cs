using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
public class Streed_gohst : MonoBehaviour
{
    public static Streed_gohst singleton { set; get; }
    [SerializeField] Transform go;
    Transform newTile;
    public void Awake()
    {
        singleton = this;
        newTile = Instantiate(go, Vector3.zero, Quaternion.Euler(0, 0, 0)) as Transform;
    }
    // Update is called once per frame
    private void Update()
    {

        Plane plane = new Plane(Vector3.up, Vector3.zero * 4);

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (plane.Raycast(ray, out float distance))
        {
            if (Global.buildmoide == 1)
            {
                Vector3 Target = ray.GetPoint(distance);
                Target.y = 3;
                Target.z = Mathf.Floor(Target.z) ;
                Target.x = Mathf.Floor(Target.x) ;
                newTile.position = Vector3.Lerp(newTile.position, Target, Time.deltaTime * 8);
                
            }
            if(Global.buildmoide != 1)
            {
       
                newTile.position = Vector3.zero;
            }

        }

 
    }
}
