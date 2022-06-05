using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



public class Junk : MonoBehaviour
{

    public static Junk singleton { set; get; }

    bool focus = false;

    [SerializeField] Transform default_Boden;
    [SerializeField] Transform Grass;
    [SerializeField] Transform Grass1;
    Transform mapMolder;
    public bool buildmode = false;
    bool erstellt = false;
    int[,] Rohstoff = new int[Map.chunck_grösse, Map.chunck_grösse];
    private Grid_script<Grid_opjekt> grid;

    //
    int X_POS,Y_POS;


    public void default_Fläche()
    {
        for (int x = 0; x < Map.chunck_grösse - 1; x++)                                                                   // Mins eins da  wir mit dem index null anfangen
        {
            for (int y = 0; y < Map.chunck_grösse - 1; y++)
            {
                Grid_opjekt gr = grid.GetGridOpjekt(x, y);
                if (gr != null)
                {
                    gr.Boden = default_Boden;
                }
            }
        }

    }
    

    public void load()
    {
        int X = 0;
        int Y = 0;


        if (transform.position.x >= 0)
        {
            X = System.Convert.ToInt32(((transform.position.x )  + (Map.Map_grösse * Map.chunck_grösse) / 2)- 17 );
        }
        if (transform.position.z >= 0)
        {
            Y = System.Convert.ToInt32(((transform.position.z ) + (Map.Map_grösse * Map.chunck_grösse) / 2) -17 );
        }

        if (transform.position.x < 0)
        {
            X = System.Convert.ToInt32(((transform.position.x ) + (Map.Map_grösse * Map.chunck_grösse) / 2) -17 );
        }
        if (transform.position.z < 0)
        {
            Y = System.Convert.ToInt32(((transform.position.z ) + (Map.Map_grösse * Map.chunck_grösse) / 2) -17);
        }
      
        Save.Lode_Chunck(X, Y, grid);
        Save.Relod_Chunk(X,Y,grid);
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

        for (int x = 0; x < Map.chunck_grösse - 1; x++)
        {
            for (int y = 0; y < Map.chunck_grösse - 1; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null)
                {
                    // grassfläche erstellen 
                    Vector3 tilePosition = new Vector3(-Map.chunck_grösse / 2 + x, ga.value, -Map.chunck_grösse / 2 + y) + transform.position;
                    Transform newTile = Instantiate(ga.Boden, tilePosition, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                    newTile.parent = mapMolder; // noch umbennenen
                    if (ga.Boden != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.value, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Boden, Position, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                        Rohstoff.parent = mapMolder;
                        if (ga.Boden == Grass1)
                        {
                            Vector3 Positio = new Vector3(-Map.chunck_grösse / 2 + x, ga.value + 1.2f, -Map.chunck_grösse / 2 + y) + transform.position;
                            Transform Gras = Instantiate(Grass, Positio, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                            Gras.parent = mapMolder;
                        }
  
                    }
                    if (ga.Rohstoff != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.value + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Rohstoff, Position, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                        Rohstoff.parent = mapMolder;
                    }
                    if (ga.buildmode == true)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.value + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Building, Position, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                        Rohstoff.parent = mapMolder;
                    }
                    if (ga.streed != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.Element_hight + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.streed, Position, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                        Rohstoff.parent = streed;
                    }

                }


            }
        }
    }

    private Bauen BuildingsystemsAktions;
    void Build()
    {
        print("dd");
        if(focus == true && Global.buildmoide == 2 && Global.buildmoide == 1)
        {
            Map_update();
            streed_update();
        }

    }
    private void OnEnable()
    {
        BuildingsystemsAktions.Buildings.Build.performed += _ => Build();
        BuildingsystemsAktions.Buildings.Build.Enable();

    }
    public void Awake()
    {
        BuildingsystemsAktions = new Bauen();

        grid = new Grid_script<Grid_opjekt>(Map.chunck_grösse, Map.chunck_grösse, 1, () => new Grid_opjekt());

        singleton = this;

        if (transform.position.x >= 0)
        {
            X_POS = System.Convert.ToInt32(((transform.position.x) + (Map.Map_grösse * Map.chunck_grösse) / 2) - 17);
        }
        if (transform.position.z >= 0)
        {
            Y_POS = System.Convert.ToInt32(((transform.position.z) + (Map.Map_grösse * Map.chunck_grösse) / 2) - 17);
        }

        if (transform.position.x < 0)
        {
            X_POS = System.Convert.ToInt32(((transform.position.x) + (Map.Map_grösse * Map.chunck_grösse) / 2) - 17);
        }
        if (transform.position.z < 0)
        {
            Y_POS = System.Convert.ToInt32(((transform.position.z) + (Map.Map_grösse * Map.chunck_grösse) / 2) - 17);
        }
    }
    public void Start()
    {
        default_Fläche();
        load();
    }


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


    bool Load = false;
    private void mous_erstellen()
    {
            int grösse = 2+ Map.chunck_grösse / 2;
        
            Plane plane = new Plane(Vector3.up, Vector3.zero * 4);

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (plane.Raycast(ray, out float distance))
        {
            if (ray.GetPoint(distance).x > transform.position.x - grösse && ray.GetPoint(distance).x < transform.position.x + grösse && ray.GetPoint(distance).z > transform.position.z - grösse && ray.GetPoint(distance).z < transform.position.z + grösse)
            {
                focus = true;
                Buildingsystem.singleton.hm(X_POS, Y_POS, ray.GetPoint(distance),grid);
                Relod_strasse(X_POS, Y_POS);
            }
            else
            {
                focus=false;
            }

        }
    }
    private void Relod_strasse(int X, int Y)
    {
        Save.Lode_Chunck(X, Y, grid);
        for (int x = 0; x < Map.chunck_grösse; x++)
        {
            for (int y = 0; y < Map.chunck_grösse; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null) // prufen ob sich etwas verender hat 
                {
                    if (ga.Rohstoffe_ID >= 1700 && ga.Rohstoffe_ID < 1800)
                    {
                        straße.singleton.Straße_setzen(ga, X + x, Y + y);
                    }
                    if (ga.Rohstoffe_ID >= 11820 && ga.Rohstoffe_ID < 11900)
                    {
                        Global.Mine_Focus.Mine_setzen(ga, X + x, Y + y);
             
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

        for (int x = 0; x < Map.chunck_grösse - 1; x++)
        {
            for (int y = 0; y < Map.chunck_grösse - 1; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null)
                {
                    if (ga.streed != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.Element_hight + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.streed, Position, Quaternion.Euler(0, ga.asrot, 0)) as Transform;
                        Rohstoff.parent = streed;
                    }
                    if  (ga.Mine != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.Element_hight + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Mine, Position, Quaternion.Euler(0, ga.asrot, 0)) as Transform;
                        Rohstoff.parent = streed;
                    }

                }


            }
        }
    }
    private void Relod(int X,int Y)
    {
        for (int x = 0; x < Map.chunck_grösse; x++)
        {
            for (int y = 0; y < Map.chunck_grösse; y++)
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
                        Lehm.singleton.Lehm_Boden(ga); // hierauch den boden ändern
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





