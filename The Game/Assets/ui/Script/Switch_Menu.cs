using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Menu : MonoBehaviour
{
    //Men�s
    [SerializeField]GameObject Buildmode;

    public void switch_Buildmod()
    {
        Buildmode.SetActive(!Buildmode.active);
    }
}
