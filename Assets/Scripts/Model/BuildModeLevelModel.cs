using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildModeLevelModel : MonoBehaviour
{
    public GameObject[] areaParts;
    public int playSurfaceWidth;
    public int playSurfaceHeight;
    public int[] numberOfPartsRequired;
    public int[] numberOfPartsAllowed;

    private List<GameObject> _builtParts;

    public bool IsValidNextPlacement(GameObject part)
    {
        return withinBounds(part);
    }

    private bool withinBounds(GameObject part)
    {
        return true;
    }



    public void Place(GameObject part)
    {
        if (part != null && IsValidNextPlacement(part))
        {
            this._builtParts.Add(Instantiate(part));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _builtParts = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
