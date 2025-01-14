using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Globale array
    public static long[,] Map_Rohstoffe;
    public static long[,] Map_Rohstoffe_Boden;
    public static int[,] Map_Stra�e;

    // save
    public static int[,] Biom = new int[10,3];
    public static Transform Camara_body;
    //Fix werte
    [SerializeField] private Transform Chunck;
    public static int chunck_gr�sse = 17;
    [SerializeField]  public static float Map_gr�sse = 29;
    public static int halbe_map = System.Convert.ToInt32((Map_gr�sse / 2) * chunck_gr�sse);
    public static Grid_script<Grid_opjekt> grid;

    public static string Mode = "Normal";
    // Globales Chunck Variablen
    public List<Junk> Chuncks = new List<Junk>();
      
    public static int Lehm_2 = 0;

    [Range(0, 1)]
    public float outLinePercent;
    private void Random_Map()
    {
        Berg Berge = new Berg();
        Wald W�lder = new Wald();
        Lehm Lehm = new Lehm();

        Lehm.Lehm_Genarator(6,25,2.3f,4);  // ein lehm der zimlich fix ist das man am anfang lehm hat 
       Lehm.Lehm_Genarator(Random.Range(10,14),Random.Range(34,45), Random.Range(4,4.5f), 5);  //Random Lehm Biom mit 5-8 lehmst�cken und mit dem radius 30-54 auf der Zone 2-3.5 und mit einer Toleranz von 6
       W�lder.Wald_erstellen(0, 4, 100);
       Berge.Stein_Genarator(0.8,4,25);
       W�lder.Wald_erstellen( 4, 6, 400);
       W�lder.Wald_erstellen(6, 7, 2300);
        // Die Mitte kann auch als rand verwendet werden 
    }
    
    public void GeneratMap()
    {
        string holdename = "Chuncks";

        Transform mapMolder = new GameObject(holdename).transform;
        mapMolder.parent = transform;

        for (float x = -Map_gr�sse / 2 ; x < Map_gr�sse / 2; x ++ )
        {
            for (float y = -Map_gr�sse / 2 ; y < Map_gr�sse / 2; y ++ )
            {
                Vector3 tilePosition = new Vector3( x * (chunck_gr�sse - 1) +8 , 0, y * (chunck_gr�sse - 1) +8 ) + transform.position;
                Junk b = new Junk();
                Transform a = Instantiate(Chunck, tilePosition, Quaternion.Euler(Vector3.right)) as Transform;
                a.parent = transform;
                Chuncks.Add(b);
            }
        }
    }
    public void Awake()
    {
        grid = new Grid_script<Grid_opjekt>(System.Convert.ToInt32(Map.Map_gr�sse * Map.chunck_gr�sse), System.Convert.ToInt32(Map.Map_gr�sse * Map.chunck_gr�sse), 1, () => new Grid_opjekt());

        Map_Rohstoffe = Map_Rohstoffe_Boden= new long[System.Convert.ToInt64(chunck_gr�sse * Map_gr�sse), System.Convert.ToInt32(chunck_gr�sse * Map_gr�sse)];
        for (int x = 0; x < Map_Rohstoffe.GetLength(0); x++)
        {
            for (int i = 0; i < Map_Rohstoffe.GetLength(1); i++)
            {
                Map_Rohstoffe[x, i] = 00100000000;
                Map_Rohstoffe_Boden[x, i] = 00100000000;
            }

        }
        Camara_body = transform.Find("Camera_body");
        GeneratMap();
        
    }
    private void Start()
    {
        Random_Map();
        Save.Save_Array();
        //Save.Load_maP();
    }

    
    private void Update()
    {
    }
    private void Set_Void()
    {
        for (int i = 0; i < chunck_gr�sse-2; i++)
        {
            for (int a = 0; a < chunck_gr�sse-2; a++)
            {
                Map_Rohstoffe[i + halbe_map -chunck_gr�sse+2, a + halbe_map - chunck_gr�sse+2 ] = 9000; // hier wrd nichts spawnen 
            }
        }
    }
}
