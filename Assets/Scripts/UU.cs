using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UU 
{
    public static T GetOrAddComponent<T>(UnityEngine.GameObject gO) where T : UnityEngine.Component
    {
        T componentMaybe = gO.GetComponent<T>();
        if (componentMaybe != null)
        {
            return componentMaybe;
        }
        return gO.AddComponent<T>();
    }
}