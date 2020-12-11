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
    public Material playSurfaceMaterial;
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
        float validZMax = (playSurfaceHeight / 2) - ((areaPartModel.areDimensionsFlipped() ? areaPartModel.length : areaPartModel.height) / 2);
        float validXMax = (playSurfaceLength / 2) - ((areaPartModel.areDimensionsFlipped() ? areaPartModel.height : areaPartModel.length) / 2);
        var position = part.transform.position;
        return position.z <= validZMax && position.x <= validXMax && position.x >= -validXMax && position.z >= -validZMax;
        

    }

    public int getNumberOfPartsUsed(int index)
    {
        return _builtParts.Count(p => identifyPartIndex(p) == index);
    }



    public void Place(GameObject part)
    {
        if (part != null && IsValidNextPlacement(part))
        {
            GameObject instantiated = Instantiate(part);
            UU.GetOrAddComponent<AreaPartController>(instantiated).SetPartMaterial(playSurfaceMaterial);
            this._builtParts.Add(instantiated);
        }
    }
    
    public int getNumberRequired(GameObject part)
    {
        if (part != null && identifyPartIndex(part) >= 0)
        {
            return this.numberOfPartsRequired[identifyPartIndex(part)];
        }
        else throw new Exception("Cannot get number of part required: no such part in model");
    }
    
    public int getNumberMaximum(GameObject part)
    {
        if (part != null && identifyPartIndex(part) >= 0)
        {
            return this.numberOfPartsAllowed[identifyPartIndex(part)];
        }
        else throw new Exception("Cannot get number of part allowed max: no such part in model");
    }

    public int identifyPartIndex(GameObject part)
    {
        for (int i = 0; i < this.areaParts.Length; i++)
        {
            string partNameScrubbed = part.name.Replace("(Clone)", "");
            if (partNameScrubbed == areaParts[i].name)
            {
                return i;
            }
        }

        return -1;
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
