using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InfiltrationModeController : MonoBehaviour
{
    public GameObject macGuffinPrefab;
    public GameObject infiltratorPrefab;
    private Camera _mainCamera;
    private BuildModeLevelModel _buildModeLevelModel;

    void Start()
    {
        _mainCamera = Camera.main;
        _buildModeLevelModel = FindObjectOfType<BuildModeLevelModel>();
    }

    public void InfiltrationModeBegun()
    {
        PlaceMacGuffin();
        PlaceInfiltrator();
    }

    private void PlaceInfiltrator()
    {
        Rect bounds = _buildModeLevelModel.GetAggregateBoundingRect();
        bounds.Set(bounds.x - 5, bounds.y - 5, bounds.x + 5, bounds.y + 5);
        Vector3 infiltratorStart = PositionNotWithinBounds(bounds);
        Debug.Log("Placing infiltrator at " + infiltratorStart);
        Instantiate(infiltratorPrefab, infiltratorStart, Quaternion.identity);
    }

    private Vector3 PositionNotWithinBounds(Rect bounds)
    {
        float x = Random.Range(-_buildModeLevelModel.playSurfaceLength / 2f,
            (_buildModeLevelModel.playSurfaceLength / 2f - bounds.width));
        if (x > bounds.xMin)
        {
            x += bounds.width;
        }
        float z = Random.Range(-_buildModeLevelModel.playSurfaceHeight / 2f,
            (_buildModeLevelModel.playSurfaceHeight / 2f - bounds.height));
        if (z > bounds.yMin)
        {
            z += bounds.height;
        }

        return new Vector3(x, 1.2f, z);
    }

    private void PlaceMacGuffin()
    {
        List<GameObject> possibleLocations = _buildModeLevelModel.getBuiltAreas();
        GameObject chosenArea = possibleLocations[Random.Range(0, possibleLocations.Count)];
        Instantiate(macGuffinPrefab, chosenArea.transform.position + Vector3.up, Quaternion.identity);
    }
    
    void Update()
    {
    }
}