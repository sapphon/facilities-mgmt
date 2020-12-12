using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class InfiltrationModeController : MonoBehaviour
{
    public GameObject macGuffinPrefab;
    public GameObject infiltratorPrefab;
    private Camera _mainCamera;
    private BuildModeLevelModel _buildModeLevelModel;
    private InfiltrationModeRouteModel _routeModel;

    void Start()
    {
        _mainCamera = Camera.main;
        _buildModeLevelModel = FindObjectOfType<BuildModeLevelModel>();
        _routeModel = FindObjectOfType<InfiltrationModeRouteModel>();
    }

    public void InfiltrationModeBegun()
    {
        PlaceMacGuffin();
        Random.InitState(Mathf.FloorToInt(Time.time));
        for (int i = 0; i < _routeModel.routesToDo; i++)
        {
            PlaceInfiltrator();
        }
    }

    private void PlaceInfiltrator()
    {
        Rect bounds = _buildModeLevelModel.GetAggregateBoundingRect();
        bounds.Set(bounds.xMin - 5, bounds.yMin - 5, bounds.width + 10, bounds.height + 10);
        Vector3 infiltratorStart = PositionOnRectangle(bounds);
        Instantiate(infiltratorPrefab, infiltratorStart, Quaternion.identity);
    }

    private Vector3 PositionOnRectangle(Rect bounds)
    {
        int side = Random.Range(0, 4);
        Vector2 pointOnRectangle = new Vector2();
        if (side == 0)
        {
            pointOnRectangle =  Rect.NormalizedToPoint(bounds, new Vector2(Random.value, 0f));
        }
        else if (side == 1)
        {
            pointOnRectangle =  Rect.NormalizedToPoint(bounds, new Vector2(1.0f, Random.value));
        }
        else if(side == 2)
        {
            pointOnRectangle =  Rect.NormalizedToPoint(bounds, new Vector2(Random.value, 1.0f));
        }
        else
        {
            pointOnRectangle =  Rect.NormalizedToPoint(bounds, new Vector2(0f, Random.value));
        }
        
        return new Vector3(pointOnRectangle.x, 1.2f, pointOnRectangle.y);
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