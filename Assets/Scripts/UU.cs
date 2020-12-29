using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Random = System.Random;

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

    public static bool IsRectAdjacent(Rect r1, Vector2 r2)
    {
        Rect wide = Rect.MinMaxRect(r1.xMin - 0.5f, r1.yMin, r1.xMax + 0.5f, r1.yMax);
        Rect loong = Rect.MinMaxRect(r1.xMin, r1.yMin - 0.5f, r1.xMax, r1.yMax + 0.5f);
        return !r1.Contains(r2) && (wide.Contains(r2) || loong.Contains(r2));
    }

    public static bool IsCloseTo(float f1, float f2, float acceptableDelta)
    {
        return Math.Abs(f1 - f2) < acceptableDelta;
    }
    
    public static Color InterpolateColor(float value, float maximumPossibleValue, Color maximumColor)
    {
        return Color.Lerp( Color.white, maximumColor, value / maximumPossibleValue);
    }

    public static T ChooseRandomFromList<T>(List<T> toChooseFrom)
    {
        return toChooseFrom[Mathf.FloorToInt(UnityEngine.Random.value * toChooseFrom.Count)];
    }
}