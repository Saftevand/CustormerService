using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyAttributes : MonoBehaviour
{
    public int CurrentEnergy = 3;
    public int MaxEnergy = 3;
    
    void Start()
    {
        UnityEngine.UI.Text current = GetChildComponentByName<UnityEngine.UI.Text>("Current");
        UnityEngine.UI.Text max = GetChildComponentByName<UnityEngine.UI.Text>("Max");
        current.text = CurrentEnergy.ToString();
        max.text = MaxEnergy.ToString();
    }



    public T GetChildComponentByName<T>(string name) where T : Component
    {
        foreach (T component in GetComponentsInChildren<T>(true))
        {
            if (component.gameObject.name == name)
            {
                return component;
            }
        }
        return null;
    }
}
