using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



public class Junk : MonoBehaviour
{
    [SerializeField] Transform default_Boden;
    [SerializeField] Transform Grass;
    [SerializeField] Transform Grass1;
    Transform mapMolder;
    public bool buildmode = false;
    bool erstellt = false;
    int[,] Rohstoff = new int[Map.chunck_gr�sse, Map.chunck_gr�sse];
    private Grid_script<Grid_opjekt> grid;

    //
    int X_POS,Y_POS;



    public void default_Fl�che()
    {
        for (int x = 0; x < Map.chunck_gr�sse - 1; x++)                                                                   // Mins eins da  wir mit dem index null anfangen
        {
            for (int y = 0; y < Map.chunck_gr�sse - 1; y++)
            {
                Grid_opjekt gr = grid.GetGridOpjekt(x, y);
                if (gr != null)
                {
                    gr.Boden = default_Boden;
                }
            }
        }

    }
    public void Rrohstoffe_erstellen()
    {
     
        
    }

    public void load()
    {
        int X = 0;
        int Y = 0;


        if (transform.position.x >= 0)
        {
            X = System.Convert.ToInt32(((transform.position.x )  + (Map.Map_gr�sse * Map.chunck_gr�sse) / 2)- 17 );
        }
        if (transform.position.z >= 0)
        {
            Y = System.Convert.ToInt32(((transform.position.z ) + (Map.Map_gr�sse * Map.chunck_gr�sse) / 2) -17 );
        }

        if (transform.position.x < 0)
        {
            X = System.Convert.ToInt32(((transform.position.x ) + (Map.Map_gr�sse * Map.chunck_gr�sse) / 2) -17 );
        }
        if (transform.position.z < 0)
        {
            Y = System.Convert.ToInt32(((transform.position.z ) + (Map.Map_gr�sse * Map.chunck_gr�sse) / 2) -17);
        }
      
        Save.Lode_Chunck(X, Y, grid);
        Save.Relod_Chunk(X,Y,grid);
        Save.Save_Array();
        Relod(X,Y);

    }
    public void Map_update()
    {

        if (transform.Find("world"))
        {
            DestroyImmediate(transform.Find("world").gameObject);
        }

        Transform mapMolder = new GameObject("world").transform;
        GameObject.Find("world").isStatic = true;

        if (transform.Find("streed"))
        {
            DestroyImmediate(transform.Find("streed").gameObject);
        }

        Transform streed = new GameObject("streed").transform;
        GameObject.Find("streed").isStatic = true;

        streed.parent = transform;
        streed.position = transform.position;
        mapMolder.parent = transform;
        mapMolder.position = transform.position;

        for (int x = 0; x < Map.chunck_gr�sse - 1; x++)
        {
            for (int y = 0; y < Map.chunck_gr�sse - 1; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null)
                {
                    // grassfl�che erstellen 
                    Vector3 tilePosition = new Vector3(-Map.chunck_gr�sse / 2 + x, ga.value, -Map.chunck_gr�sse / 2 + y) + transform.position;
                    Transform newTile = Instantiate(ga.Boden, tilePosition, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                    newTile.parent = mapMolder; // noch umbennenen
                    if (ga.Boden != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_gr�sse / 2 + x, ga.value, -Map.chunck_gr�sse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Boden, Position, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                        Rohstoff.parent = mapMolder;
                        if (ga.Boden == Grass1)
                        {
                            Vector3 Positio = new Vector3(-Map.chunck_gr�sse / 2 + x, ga.value + 1.2f, -Map.chunck_gr�sse / 2 + y) + transform.position;
                            Transform Gras = Instantiate(Grass, Positio, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                            Gras.parent = mapMolder;
                        }
  
                    }
                    if (ga.Rohstoff != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_gr�sse / 2 + x, ga.value + 1, -Map.chunck_gr�sse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Rohstoff, Position, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                        Rohstoff.parent = mapMolder;
                    }
                    if (ga.buildmode == true)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_gr�sse / 2 + x, ga.value + 1, -Map.chunck_gr�sse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Building, Position, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                        Rohstoff.parent = mapMolder;
                    }
                    if (ga.streed != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_gr�sse / 2 + x, ga.Element_hight + 1, -Map.chunck_gr�sse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.streed, Position, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                        Rohstoff.parent = streed;
                    }

                }


            }
        }
    }
    public void Awake()
    {
        grid = new Grid_script<Grid_opjekt>(Map.chunck_gr�sse, Map.chunck_gr�sse, 1, () => new Grid_opjekt());


        if (transform.position.x >= 0)
        {
            X_POS = System.Convert.ToInt32(((transform.position.x) + (Map.Map_gr�sse * Map.chunck_gr�sse) / 2) - 17);
        }
        if (transform.position.z >= 0)
        {
            Y_POS = System.Convert.ToInt32(((transform.position.z) + (Map.Map_gr�sse * Map.chunck_gr�sse) / 2) - 17);
        }

        if (transform.position.x < 0)
        {
            X_POS = System.Convert.ToInt32(((transform.position.x) + (Map.Map_gr�sse * Map.chunck_gr�sse) / 2) - 17);
        }
        if (transform.position.z < 0)
        {
            Y_POS = System.Convert.ToInt32(((transform.position.z) + (Map.Map_gr�sse * Map.chunck_gr�sse) / 2) - 17);
        }
    }
    public void Start()
    {
        default_Fl�che();
        load();
    }

    private int Give_Circel()
    {
        if (Mathf.Abs(transform.position.x) >= 20 || Mathf.Abs(transform.position.y) >= 20)                         // der h�chste ring muss oben sein
        {
            return 2;
        }
        if (Mathf.Abs(transform.position.x) >= 5 || Mathf.Abs(transform.position.y) >= 5)
        {
            return 1;
        }
        return 0;
    }



    /*------------ Erstellungs regeln ------------
     * 
     *  5 H�ufig 
     *  4 mittel 
     *  3 Selten 
     *  2 super selten 
     *  1 episch 
     *  
     *  
     *  Es gibt verschide arten von Chuncks 
     * 
     *  Vorkommen:
     *      Erster ring:
     *          noramler chunck 
     *          stein chunck 
     *          Wald chunck 
     *      
     *      Zweiter Rinf:
     *          Erter Ring
     *          Lehm Chunck
     *      
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */

    bool geladen= false;
    int X_render = 40;
    int Y_render = 40;
    void Update()
    {
        if (Map.Camara_body != null)
        { 
            if (Map.Camara_body.position.x < transform.position.x + X_render && Map.Camara_body.position.x > transform.position.x - X_render && Map.Camara_body.position.z < transform.position.z + Y_render && Map.Camara_body.position.z > transform.position.z - Y_render && geladen == false)
            {
                Map_update();
                geladen = true;
            }
            else if ((Map.Camara_body.position.x > transform.position.x + X_render || Map.Camara_body.position.x < transform.position.x - X_render && Map.Camara_body.position.z > transform.position.z + Y_render || Map.Camara_body.position.z < transform.position.z - Y_render && geladen == true))
            {

                if (transform.Find("RE"))
                {
                    DestroyImmediate(transform.Find("RE").gameObject);
                }

                geladen = false;
            }
            if (Global.buildmoide > 0 && geladen == true)
            {
                mous_erstellen();
            }
        }
        
    }

    private void update()
    {

    }
    bool Load = false;
    private void mous_erstellen()
    {
            int gr�sse = 2+ Map.chunck_gr�sse / 2;
        
            Plane plane = new Plane(Vector3.up, Vector3.zero * 4);

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (plane.Raycast(ray, out float distance))
        {
            if (ray.GetPoint(distance).x > transform.position.x - gr�sse && ray.GetPoint(distance).x < transform.position.x + gr�sse && ray.GetPoint(distance).z > transform.position.z - gr�sse && ray.GetPoint(distance).z < transform.position.z + gr�sse)
            {
                stra�e.singleton.hm(X_POS, Y_POS, ray.GetPoint(distance),grid);
                Relod_strasse(X_POS, Y_POS);
                streed_update();
            }

        }
    }
    private void Relod_strasse(int X, int Y)
    {
        Save.Lode_Chunck(X, Y, grid);
        for (int x = 0; x < Map.chunck_gr�sse; x++)
        {
            for (int y = 0; y < Map.chunck_gr�sse; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null) // prufen ob sich etwas verender hat 
                {
                    if (ga.Rohstoffe_ID >= 1700 && ga.Rohstoffe_ID < 1800)
                    {
                        stra�e.singleton.Stra�e_setzen(ga, X + x, Y + y);
                    }
                    if (ga.Rohstoffe_ID >= 11800 && ga.Rohstoffe_ID < 11900)
                    {

                       Miene.singelton.Mine_setzen(ga, X + x, Y + y);
                    }
                }

            }
        }
    }

    public void streed_update()
    {

        if (transform.Find("streed"))
        {
            DestroyImmediate(transform.Find("streed").gameObject);
        }

        Transform streed = new GameObject("streed").transform;
        GameObject.Find("streed").isStatic = true;

        streed.parent = transform;
        streed.position = transform.position;

        for (int x = 0; x < Map.chunck_gr�sse - 1; x++)
        {
            for (int y = 0; y < Map.chunck_gr�sse - 1; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null)
                {
                    if (ga.streed != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_gr�sse / 2 + x, ga.Element_hight + 1, -Map.chunck_gr�sse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.streed, Position, Quaternion.Euler(0, ga.asrot, 0)) as Transform;
                        Rohstoff.parent = streed;
                    }
                    if  (ga.Mine != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_gr�sse / 2 + x, ga.Element_hight + 1, -Map.chunck_gr�sse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Mine, Position, Quaternion.Euler(0, ga.asrot, 0)) as Transform;
                        Rohstoff.parent = streed;
                    }

                }


            }
        }
    }
    private void Relod(int X,int Y)
    {
        for (int x = 0; x < Map.chunck_gr�sse; x++)
        {
            for (int y = 0; y < Map.chunck_gr�sse; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null)
                { 

                    if (ga.Rohstoffe_ID == 2000)
                    {
                        Lehm.singleton.Lehm_Boden(ga);
                    }
                    if (ga.Rohstoffe_ID == 1000)
                    {
                        Wald.singleton.Wiese_Boden(ga);
                    }
                    if (ga.Rohstoffe_ID == 1050)
                    {
                        Berg.singleton.Berg_Boden(ga);
                    }
                    if (ga.Rohstoffe_ID == 200)
                    {
                        Wald.singleton.Baum(ga);
                        Wald.singleton.Wald_boden(ga);
                    }
                    if (ga.Rohstoffe_ID >= 300 && ga.Rohstoffe_ID < 400)
                    {
                        Berg.singleton.Berg_setzen(Map.Map_Rohstoffe, ga, X + x, Y + y);
                       
                    }
                    else if (ga.Rohstoffe_ID == 2100)
                    {
                        Lehm.singleton.Lehm_Baum( ga);
                        Lehm.singleton.Lehm_Boden(ga); // hierauch den boden �ndern
                    }
                    else if (ga.Rohstoffe_ID == 2400)
                    {
                        Lehm.singleton.Lehm_setzen(Map.Map_Rohstoffe, ga, X + x, Y + y);
                    }
                }

            }
        }
    }
}





