using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Grid_script<Grid_opjekt>
{

    private int width;
    private int height;
    private float cellSize;
    public Grid_opjekt[,] gridArray;

    public Grid_script(int width, int height, float cellSize,Func<Grid_opjekt> createGridObjekt )
    {
        this.width = width;
        this.height = height;
        gridArray = new Grid_opjekt[width, height];

        for (int x = 0; x <height; x++)
        {
            for (int y = 0; y <width; y++)
            {
                gridArray[x, y] = createGridObjekt();
            }
        }
    }


    public Grid_opjekt GetGridOpjekt(int x , int y)
    {
        if(x >= 0 && y >= 0 && x<= width&& y<= height)
        {

            return gridArray[x,y];
        }
        else
        {
            return default(Grid_opjekt);
        }
    }
    public Grid_opjekt GetGridOpjekt_Global(int x, int y, Vector3 World)
    {
        int x_global = Convert.ToInt32(World.x);
        int y_global = Convert.ToInt32(World.z);
        if (x > 0 && y > 0 && x <= width && y <= height)
        {
            return gridArray[x + x_global   , y +y_global];
        }
        else
        {

            return default(Grid_opjekt);
        }
    }

    private Vector3 GetWorldPosition(int x, int y,Vector3 World)
    {
        return new Vector3(x, y) * cellSize + World;
    }

}




