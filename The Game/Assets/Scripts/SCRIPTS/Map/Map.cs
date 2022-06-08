using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Globale array
    public static int[,] Map_Rohstoffe;
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
        W�lder.Wald_erstellen(15, 15, 0, 4, 100);
        Berge.Stein_Genarator(0.8,4,25);
        W�lder.Wald_erstellen(15, 15, 4, 6, 400);
        W�lder.Wald_erstellen(15, 15,6, 7, 2300);
        // Die Mitte kann auch als rand verwendet werden 
  
        
    }
    
    public void GeneratMap()
    {
        string holdename = "junks";

        Transform mapMolder = new GameObject(holdename).transform;
        mapMolder.parent = transform;

        for (float x = -Map_gr�sse / 2 ; x < Map_gr�sse / 2; x ++ )
        {
            for (float y = -Map_gr�sse / 2 ; y < Map_gr�sse / 2; y ++ )
            {
                
                    Vector3 tilePosition = new Vector3( x * (chunck_gr�sse - 1) +8 , 0, y * (chunck_gr�sse - 1) +8 ) + transform.position;
                    Transform a = Instantiate(Chunck, tilePosition, Quaternion.Euler(Vector3.right)) as Transform;
                    a.parent = transform;
                
            }
        }
    }
    public void Awake()
    {
        Map_Rohstoffe = Map_Stra�e = new int[System.Convert.ToInt32(chunck_gr�sse * Map_gr�sse), System.Convert.ToInt32(chunck_gr�sse * Map_gr�sse)];
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
