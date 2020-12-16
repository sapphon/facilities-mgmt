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

    public static bool IsRectAdjacent(Rect r1, Rect r2)
    {
        Rect wide = Rect.MinMaxRect(r1.xMin - 0.5f, r1.yMin, r1.xMax + 0.5f, r1.yMax);
        Rect loong = Rect.MinMaxRect(r1.xMin, r1.yMin - 0.5f, r1.xMax, r1.yMax + 0.5f);
        return !r1.Overlaps(r2) && (wide.Overlaps(r2) || loong.Overlaps(r2));
    }
}