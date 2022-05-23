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
    public void Rrohstoffe_erstellen()
    {
     
        
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
        Save.Save_Array();
        Relod(X,Y);

    }
    public void Map_update()
    {

        if (transform.Find("RE"))
        {
            DestroyImmediate(transform.Find("RE").gameObject);
        }

        Transform mapMolder = new GameObject("RE").transform;

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
                        Rohstoff.parent = mapMolder;
                    }
                    if (ga.not_plaecd != null)
                    { 
                        Vector3 Position = new Vector3(-Map.chunck_grösse / 2 + x, ga.Element_hight + 1, -Map.chunck_grösse / 2 + y) + transform.position;
                        Transform Rohstoff = Instantiate(ga.not_plaecd, Position, Quaternion.Euler(0, ga.rot, 0)) as Transform;
                        Rohstoff.parent = mapMolder;
                    }
                }


            }
        }
    }
    public void Awake()
    {
        grid = new Grid_script<Grid_opjekt>(Map.chunck_grösse, Map.chunck_grösse, 1, () => new Grid_opjekt());


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
    /*
   private void Random_Chunck()
    {
        // Random system
        int r;
        int summe = 0;
        int anzahl = 3;             // anzahl elementen in demfall Chunck arten
        int protzent = 0;
        int max;
        int min;
        int lastmax = 0;
        int fertig = 1;
        r = Random.Range(0, 10001);
        int[] Teile = new int[100];

        Teile[0] = 100;
        Teile[1] = 40;
        Teile[2] = 40;
        if (Give_Circel() == 2)
        {
            anzahl = 4;
            Teile[0] = 20;
            Teile[3] = 40;
        }
        for (int i = 0; i < anzahl; i++)
        {
            summe += Teile[i];
        }
        protzent = 10000 / summe;
        for (int i = 0; i < anzahl; i++)
        {
            if (i == 0)
            {
                min = 0;
                max = (protzent * Teile[i]) - 1;
                lastmax = max;
            }
            else
            {
                min = lastmax;
                max = lastmax + (protzent * Teile[i]) - 1;
            }
            if (r > min && r < max)
            {
                fertig = i;
            }
        }
        switch (fertig)
        {
            case 0:

                Normal_Chunck();    // Die Mitte kann auch als rand verwendet werden 
                break;
            case 1:
                Wald_Chunck();

                break;
            case 2:

                Stein_Chunck();
                break;
            case 3:
                Map.Lehm_2++;
                if (Map.Lehm_2 <= 2)         // nur zwei Lehm im Ring 2
                {
                    Lehm_Chunck();
                }
                else
                {
                    Normal_Chunck();
                }
                break;

        }
    }
    */
    private int Give_Circel()
    {
        if (Mathf.Abs(transform.position.x) >= 20 || Mathf.Abs(transform.position.y) >= 20)                         // der höchste ring muss oben sein
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
     *  5 Häufig 
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
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */



    // Update is called once per frame
    bool geladen= false;
    int X_render = 40;
    int Y_render = 40;
    void Update()
    {
        if (Map.Camara_body != null) mous_erstellen();
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
        
    }

    private void update()
    {

    }
    bool Load = false;
    private void mous_erstellen()
    {
        int grösse = 2+ Map.chunck_grösse / 2;


        //Keyboard kb = InputSystem.GetDevice<Keyboard>();
        if (true)//Mouse.current.leftButton.wasPressedThisFrame)
        {


            Plane plane = new Plane(Vector3.up, Vector3.zero * 4);

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (plane.Raycast(ray, out float distance))
            {
                if (ray.GetPoint(distance).x > transform.position.x - grösse && ray.GetPoint(distance).x < transform.position.x + grösse  && ray.GetPoint(distance).z > transform.position.z - grösse && ray.GetPoint(distance).z < transform.position.z + grösse )
                {
                    Load = true;
                    straße.singleton.hm(X_POS, Y_POS, ray.GetPoint(distance));
                    Relod_strasse(X_POS, Y_POS);
                    Map_update();
                }
                else if (Load == true) 
                {
                    Relod_basse(X_POS, Y_POS);
                    Map_update();
                    Save.Relod_Chunk(X_POS, Y_POS,grid);
                    Load = false;

                }
            }

        }
    }
    private void Relod_strasse(int X, int Y)
    {
        Save.Lode_Chunck(X, Y, grid);
        Save.Save_Array();
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
                        if (!ga.canBuild()) ga.Element_hight = 2.4f;
                    }
                }

            }
        }
    }
    private void Relod_basse(int X, int Y)
    {
        Save.Lode_Chunck(X, Y, grid);
        Save.Save_Array();
        for (int x = 0; x < Map.chunck_grösse; x++)
        {
            for (int y = 0; y < Map.chunck_grösse; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null && Map.Map_Rohstoffe[X, Y] != ga.Fix_Rohstoffe_ID) // prufen ob sich etwas verender hat 
                {
                    if (ga.Rohstoffe_ID >= 1700 && ga.Rohstoffe_ID < 1800 )
                    {
                        straße.singleton.Straße_setzen(ga, X + x, Y + y);
                        if (!ga.canBuild()) ga.Element_hight = 2.4f;
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
                        Rohstoffe.singleton.Lehm_Boden(ga);
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
                    }
                    if (ga.Rohstoffe_ID >= 300 && ga.Rohstoffe_ID < 400)
                    {
                        Berg.singleton.Berg_setzen(Map.Map_Rohstoffe, ga, X + x, Y + y);
                       
                    }
                    else if (ga.Rohstoffe_ID == 2100)
                    {
                        Rohstoffe.singleton.Lehm_Baum( ga);
                        Rohstoffe.singleton.Lehm_Boden(ga); // hierauch den boden ändern
                    }
                    else if (ga.Rohstoffe_ID == 2400)
                    { 
                        Rohstoffe.singleton.Lehm_setzen(Map.Map_Rohstoffe, ga, X + x, Y + y);
                    }
                }

            }
        }
    }
}





