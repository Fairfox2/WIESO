using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_mode : MonoBehaviour
{
    public void buildmod(int mode)
    {
        if(Global.buildmoide == 0)
        {
            Global.buildmoide = mode;
            print(Global.buildmoide);   
        }
        else
        {
            Global.buildmoide = 0;
        }

    }
}
