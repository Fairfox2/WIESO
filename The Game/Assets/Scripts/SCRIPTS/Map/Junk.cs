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

    public bool buildmode = false;

    private Grid_script<Grid_opjekt> grid;
    bool random = true;
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
        X_POS = X;
        Y_POS = Y;

    }
    public void Map_update()
    {

        if (transform.Find("world"))
        {
            DestroyImmediate(transform.Find("world").gameObject);
        }

        Transform mapMolder = new GameObject("world").transform;
        GameObject.Find("world").isStatic = true;

        if (transform.Find("Berg"))
        {
            DestroyImmediate(transform.Find("Berg").gameObject);
        }

        Transform Berg = new GameObject("Berg").transform;

        GameObject.Find("Berg").isStatic = true;


        if (transform.Find("streed"))
        {
            DestroyImmediate(transform.Find("streed").gameObject);
        }

        Transform streed = new GameObject("streed").transform;
        GameObject.Find("streed").isStatic = true;

        streed.parent = transform;
        streed.position = transform.position;
        Berg.parent = transform;
        Berg.position = transform.position;
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
                    Transform newTile = Instantiate(ga.Boden, tilePosition, Quaternion.Euler(0, ga.Rotation_Boden, 0)) as Transform;
                    newTile.parent = mapMolder; // noch umbennenen
                    if (ga.Boden != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.value, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Boden, Position, Quaternion.Euler(0, ga.Rotation_Boden, 0)) as Transform;
                        Rohstoff.parent = mapMolder;
                        if (ga.Boden == Grass1)
                        {
                            Vector3 Positio = new Vector3(-Map.chunck_grösse / 2 + x, ga.value + 1.2f, -Map.chunck_grösse / 2 + y) + transform.position;
                            Transform Gras = Instantiate(Grass, Positio, Quaternion.Euler(0, ga.Rotation_Boden, 0)) as Transform;
                            Gras.parent = mapMolder;
                        }
  
                    }
                    if (ga.Rohstoff != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.value + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Rohstoff, Position, Quaternion.Euler(0, ga.Rotation_Top, 0)) as Transform;
                        Rohstoff.parent = Berg;
                    }
                    if (ga.Building != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.value + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Building, Position, Quaternion.Euler(0, ga.Rotation_Top, 0)) as Transform;
                        Rohstoff.parent = mapMolder;
                    }
                    if (ga.streed != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.Element_hight + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.streed, Position, Quaternion.Euler(0, ga.Rotation_Top, 0)) as Transform;
                        Rohstoff.parent = streed;
                    }

                }

                
            }
        }
    }

    private Bauen BuildingsystemsAktions;
    void Build()
    {
        if(focus == true && Global.buildmoide >= 1)
        {
           mous_erstellen(); 
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
        random = false;
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
                if (transform.Find("Berg"))
                {
                    DestroyImmediate(transform.Find("Berg").gameObject);
                }
                geladen = false;
            }
            if (Global.buildmoide > 0 && geladen == true)
            {
                mous();
            }

         
        }
        
    }


    bool Load = false;
    private void mous()
    {
            int grösse =  Map.chunck_grösse / 2;
        
            Plane plane = new Plane(Vector3.up, Vector3.zero * 4);

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (plane.Raycast(ray, out float distance))
        {
            if (ray.GetPoint(distance).x > transform.position.x - grösse && ray.GetPoint(distance).x < transform.position.x + grösse && ray.GetPoint(distance).z > transform.position.z - grösse && ray.GetPoint(distance).z < transform.position.z + grösse)
            {
               focus = true;
               Buildingsystem.singleton.hm(ray.GetPoint(distance));
            }
            else
            {
                focus = false;
            }


        }
    }
    private void mous_erstellen()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero * 4);

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (plane.Raycast(ray, out float distance))
        {
            Buildingsystem.singleton.Build1(ray.GetPoint(distance));

            Relod_strasse(X_POS, Y_POS);
            Relod(X_POS, Y_POS);
            streed_update();
            Berg_update();

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
                    if(ga.Biom == 1)
                    {
                        if(ga.Building_Top == 1)
                        {

                            straße.singleton.Straße_setzen(ga, X + x, Y + y, random);
                        }
                        if (ga.Building_Top == 2)
                        {
                            if (ga.Art_Top == 1 && ga.zusatz_Top == 0)
                            {
                                Miene.singelton.setzen(ga, true);
                            }
                        }
                        if (ga.Building_Top == 3)
                        {
                            if (ga.Art_Top == 1 && ga.zusatz_Top == 0)
                            {
                                Lager.singelton.Lager_setzen(ga, true);
                            }
                        }
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
                        Transform Rohstoff = Instantiate(ga.streed, Position, Quaternion.Euler(0, ga.Rotation_Boden, 0)) as Transform;
                        Rohstoff.parent = streed;
                    }
                    if  (ga.Building != null)
                    {
                        ga.Rohstoff = null;
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.Element_hight + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Building, Position, Quaternion.Euler(0, ga.Rotation_Boden, 0)) as Transform;
                        Rohstoff.parent = streed;
                    }

                }


            }
        }
    }
    public void Berg_update()
    {

        if (transform.Find("Berg"))
        {
            DestroyImmediate(transform.Find("Berg").gameObject);
        }

        Transform Berg = new GameObject("Berg").transform;
        GameObject.Find("Berg").isStatic = true;

        Berg.parent = transform;
        Berg.position = transform.position;

        for (int x = 0; x < Map.chunck_grösse - 1; x++)
        {
            for (int y = 0; y < Map.chunck_grösse - 1; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null)
                {
                    if (ga.Rohstoff != null)
                    {
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.value + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.Rohstoff, Position, Quaternion.Euler(0, ga.Rotation_Top, 0)) as Transform;
                        Rohstoff.parent = Berg;
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

                    ga.Id_Load(Map.Map_Rohstoffe[X + x, Y + y]);
                    ga.Id_Boden(Map.Map_Rohstoffe_Boden[x + X, Y + y]);
                    print("sp_top " + Map.Map_Rohstoffe[X + x, Y + y]);
                    print("sp_top " + Map.Map_Rohstoffe[X + x, Y + y]%100);

                    if (ga.Biom == 1)
                    {
            
                        if (ga.Art_Boden == 1)
                        {
                         Berg.singleton.Berg_Boden(ga, random);
                        }
                        if (ga.Art_Boden == 0)
                        {
                            Wald.singleton.Wiese_Boden(ga, random);
                        }
                        else if (ga.Art_Boden == 2)
                        {
                            Wald.singleton.Wald_boden(ga, random);
                        }

                        if (ga.Art_Top == 1)
                        {
                            Berg.singleton.Berg_setzen(Map.Map_Rohstoffe, ga, X + x, Y + y, random);
                        }
                        if (ga.Art_Top == 2)
                        {
                            Wald.singleton.Baum(ga, random);
                        }

                    }
                    if (ga.Biom == 2)
                    {
                        if (ga.Art_Boden == 0)
                        {
                            Lehm.singleton.Lehm_Boden(ga);
                        }
                        if (ga.Art_Boden == 1)
                        {
                            Lehm.singleton.Lehm_setzen(Map.Map_Rohstoffe, ga, X + x, Y + y, random);
                        }

                        if (ga.Art_Top == 2)
                        {
                            Lehm.singleton.Lehm_Boden(ga);
                        }
                    }
    


                }

            }
        }
    }

}





