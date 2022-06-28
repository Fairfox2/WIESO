using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building_Page_info : MonoBehaviour
{



    public GameObject Page;
    [SerializeField]Text Level;

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    public void Info_Upadate()
    {
        Level.text = System.Convert.ToString(Global.Mine[Global.Building_index].Level);
    }
    public void Aktiv(bool t)
    {
        Page.SetActive(t);
    }
}
