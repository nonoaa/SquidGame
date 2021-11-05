using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    static Dictionary<Type, BasePanel> Panels = new Dictionary<Type, BasePanel>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static bool RegistPanel(Type PanelClassType, BasePanel basePanel)
    {
        if (Panels.ContainsKey(PanelClassType))
        {
            Debug.LogError("RegistPanel Error! Already exist Type! PanelClassType = " + PanelClassType.ToString());
            return false;
        }

        Panels.Add(PanelClassType, basePanel);
        return true;
    }

    public static bool UnregistPanel(Type PanelClassType)
    {
        if (!Panels.ContainsKey(PanelClassType))
        {
            Debug.LogError("UnregistPanel Error! Can't Find Type! PanelClassType = " + PanelClassType.ToString());
            return false;
        }

        Panels.Remove(PanelClassType);
        return true;
    }

    public static BasePanel GetPanel(Type PanelClassType)
    {
        if (!Panels.ContainsKey(PanelClassType))
        {
            Debug.LogError("GetPanel Error! Can't Find Type! PanelClassType = " + PanelClassType.ToString());
            return null;
        }

        return Panels[PanelClassType];
    }
}