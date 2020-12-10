using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeLevelModel : MonoBehaviour
{
    public GameObject[] areaParts;
    public int[] numberOfPartsRequired;
    public int[] numberOfPartsAllowed;

    private GameObject[] _builtParts;

    public bool IsValidNextPlacement(GameObject part)
    {
        if (Math.Abs(part.transform.position.x % 2) < 0.001)
        {
            return true;
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
