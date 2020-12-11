using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPartModel : MonoBehaviour{

public int length;
public int height;
public int[] exitNorthOffsets;
public int[] exitEastOffsets;
public int[] exitSouthOffsets;
public int[] exitWestOffsets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool areDimensionsFlipped()
    {
        return Math.Abs(Math.Abs(this.transform.localRotation.y) - 0.7f) < 0.1f;
    }
}
