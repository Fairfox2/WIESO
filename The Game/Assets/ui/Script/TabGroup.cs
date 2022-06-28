using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    public TabButton Selectedtab;
    public List<Building_Page_info> Objectstoswap;

    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)  
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (Selectedtab == null || button != Selectedtab)
        {
            button.background.sprite = tabHover;
        }
    }
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButton button)
    {
        Selectedtab = button;
        ResetTabs();
        button.background.sprite = tabActive;

        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < Objectstoswap.Count; i++)
        {
            if(i == index)
            {
                Global.buildmoide = i;
                Objectstoswap[i].Page.SetActive(true);
                Objectstoswap[i].Info_Upadate();
            }
            else
            {
                Objectstoswap[i].Page.SetActive(false);
            }

        }

    }

    public void ResetTabs()
    {
        foreach ( TabButton  button in tabButtons)
        {
            if(Selectedtab != null && button == Selectedtab) { continue; }
            button.background.sprite = tabIdle;
        }
    }
}
