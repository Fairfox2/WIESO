using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour
{
    public WeightedRandomList<Transform> Straße;
    public static void Save_Array()
    {
        if (!File.Exists(Application.dataPath + "/Ob4.txt"))
        {
            File.Delete(Application.dataPath + "/Ob4.txt");
        }
        StreamWriter newFile = new StreamWriter(Application.dataPath + "/Ob4.txt");
         //int mitte = System.Convert.ToInt32((Map.chunck_grösse * Map.Map_grösse) / 2);
        //Zone.midPointCircleDraw(mitte, mitte, System.Convert.ToInt32((((3 + (2 * (4- 1))) * Map.chunck_grösse) - 1) / 2));


        for (int x = 0; x < Map.Map_Rohstoffe.GetLength(0); x++)
        {
            for (int y = 0; y < Map.Map_Rohstoffe.GetLength(1); y++)
            {
                if (Map.Map_Rohstoffe[x, y] == 1000) { newFile.Write("_"); }
                else if (Map.Map_Rohstoffe[x, y] == 2000) { newFile.Write("-"); }
                else if (Map.Map_Rohstoffe[x, y] == 0) { newFile.Write(" "); }
                else if (Map.Map_Rohstoffe[x,y] == 200) { newFile.Write("#"); }
                else if (Map.Map_Rohstoffe[x, y] == 300) { newFile.Write("?"); }
                else if (Map.Map_Rohstoffe[x, y] == 2100) { newFile.Write("ä"); }
                else if (Map.Map_Rohstoffe[x, y] == 1300) { newFile.Write("!"); }
                else if (Map.Map_Rohstoffe[x, y] == 2400) { newFile.Write("D"); }
                else if (Map.Map_Rohstoffe[x, y] == 9000) { newFile.Write("§"); }
                else { newFile.Write("t"); }

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
    public static void Relod_Chunk(int X, int Y, Grid_script<Grid_opjekt> grid)
    {

        for (int x = 0; x < Map.chunck_grösse; x++)
        {
            for (int y = 0; y < Map.chunck_grösse; y++)
            {
                Grid_opjekt ga = grid.GetGridOpjekt(x, y);
                if (ga != null)
                {
                    Map.Map_Rohstoffe[x + X, y + Y] = ga.Rohstoffe_ID;
                    ga.Fix_Rohstoffe_ID = ga.Rohstoffe_ID;
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
                    if(Map.Map_Rohstoffe[X + x, Y + y] != 0) { ga.Rohstoffe_ID = Map.Map_Rohstoffe[X + x, Y + y]; }
                }

            }
        }
    }

}
