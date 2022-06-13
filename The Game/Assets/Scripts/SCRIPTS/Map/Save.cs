using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour
{
    public WeightedRandomList<Transform> Straße;
    public static void Save_Array()
    {
        if (!File.Exists(Application.dataPath + "/Array.txt"))
        {
            File.Delete(Application.dataPath + "/Array.txt");
        }
        StreamWriter newFile = new StreamWriter(Application.dataPath + "/Array.txt");
         //int mitte = System.Convert.ToInt32((Map.chunck_grösse * Map.Map_grösse) / 2);
        //Zone.midPointCircleDraw(mitte, mitte, System.Convert.ToInt32((((3 + (2 * (4- 1))) * Map.chunck_grösse) - 1) / 2));


        for (int x = 0; x < Map.Map_Rohstoffe.GetLength(0); x++)
        {
            for (int y = 0; y < Map.Map_Rohstoffe.GetLength(1); y++)
            {
                /*
                if (Map.Map_Rohstoffe[x, y] == 1000) { newFile.Write("_"); }
                else if (Map.Map_Rohstoffe[x, y] == 2000) { newFile.Write("-"); }
                else if (Map.Map_Rohstoffe[x, y] == 0) { newFile.Write(" "); }
                else if (Map.Map_Rohstoffe[x,y] == 200) { newFile.Write("#"); }
                else if (Map.Map_Rohstoffe[x, y] == 300) { newFile.Write("?"); }
                else if (Map.Map_Rohstoffe[x, y] == 2100) { newFile.Write("ä"); }
                else if (Map.Map_Rohstoffe[x, y] == 1300) { newFile.Write("!"); }
                else if (Map.Map_Rohstoffe[x, y] == 2400) { newFile.Write("D"); }
                else if (Map.Map_Rohstoffe[x, y] == 9000) { newFile.Write("§"); }
                */

                newFile.Write(Map.Map_Rohstoffe[x, y] + ",");

            }

            newFile.Write("\n");
        }

        newFile.Close();
    }
    public static void Save_maP()
    {
        if (!File.Exists(Application.dataPath + "/SAVE.txt"))
        {
            File.Delete(Application.dataPath + "/SAVE.txt");
        }
        StreamWriter newFile = new StreamWriter(Application.dataPath + "/SAVE.txt");
        //int mitte = System.Convert.ToInt32((Map.chunck_grösse * Map.Map_grösse) / 2);
        //Zone.midPointCircleDraw(mitte, mitte, System.Convert.ToInt32((((3 + (2 * (4- 1))) * Map.chunck_grösse) - 1) / 2));


        for (int x = 0; x < Map.Map_Rohstoffe.GetLength(0); x++)
        {
            for (int y = 0; y < Map.Map_Rohstoffe.GetLength(1); y++)
            {
                newFile.Write(Map.Map_Rohstoffe[x,y]) ;
                newFile.Write(",");

            }

            newFile.Write("\n");
        }

        newFile.Close();
    }
    public static void Load_maP()
    {


            //int mitte = System.Convert.ToInt32((Map.chunck_grösse * Map.Map_grösse) / 2);
            //Zone.midPointCircleDraw(mitte, mitte, System.Convert.ToInt32((((3 + (2 * (4- 1))) * Map.chunck_grösse) - 1) / 2));
            StreamReader newFile = new StreamReader(Application.dataPath + "/SAVE.txt");
            string ln;
            int count = 0;
        while ((ln = newFile.ReadLine()) != null)
        {
            string[] save = ln.Split(',');
            for (int x = 0; x < save.GetLength(0); x++)
            {
                bool flag = true;
                try
                {
                    System.Convert.ToInt32(save[x]);
                }
                catch 
                {
                    flag = false;
                    
                }
                if (flag)Map.Map_Rohstoffe[count, x] = System.Convert.ToInt32(save[x]);



            }
            count++;
        }
           

            newFile.Close();
        
    }
    public static void Relod_Chunk(int X, int Y, Grid_script<Grid_opjekt> grid)
    {

        for (int x = 0; x < Map.chunck_grösse; x++)
        {
            for (int y = 0; y < Map.chunck_grösse; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null)
                {

// muss noch gemacht werden
                }

            }
        }
    }
    public static void Lode_Chunck(int X, int Y, Grid_script<Grid_opjekt> grid)
    {

        for (int x = 0; x < Map.chunck_grösse; x++)
        {
            for (int y = 0; y < Map.chunck_grösse; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null)
                {
                    //Muss noch gemacht werden
                }

            }
        }
    }

}
