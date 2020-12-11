using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildModeLevelModel : MonoBehaviour
{
    public GameObject[] areaParts;
    public int playSurfaceLength;
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
        AreaPartModel areaPartModel = part.GetComponent<AreaPartModel>();
        float validZMax = (playSurfaceHeight / 2) - (areaPartModel.height / 2);
        float validXMax = (playSurfaceLength / 2) - (areaPartModel.length / 2);
        var position = part.transform.position;
        return position.z <= validZMax && position.x <= validXMax && position.x >= -validXMax && position.z >= -validZMax;
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
