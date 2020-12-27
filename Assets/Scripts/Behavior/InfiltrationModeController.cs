using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<SecurityCamera> _cameras;
    private int _currentRound;

    void Start()
    {
        _currentRound = 0;
        _mainCamera = Camera.main;
        _buildModeLevelModel = FindObjectOfType<BuildModeLevelModel>();
        _routeModel = FindObjectOfType<InfiltrationModeRouteModel>();
        _cameras = FindObjectsOfType<SecurityCamera>().ToList();
    }

    public void InfiltrationModeBegun()
    {
        PlaceMacGuffin();
        Random.InitState(Mathf.FloorToInt(Time.time));
        BeginInfiltrationRound();
    }

    private void BeginInfiltrationRound()
    {
        RemoveInfiltrators();

        for (int i = 0; i < _routeModel.routesToDoPerRound; i++)
        {
            PlaceInfiltrator();
        }

        ConfigureCameras();
    }

    private void ConfigureCameras()
    {
        _cameras = FindObjectsOfType<SecurityCamera>().ToList();
        List<InfiltratorController> infiltratorsToWatchFor = GetAllInfiltrators();
        foreach (SecurityCamera _camera in _cameras)
        {
            _camera.SetInfiltrators(infiltratorsToWatchFor);
            _camera.BeginRecording();
        }
    }

    public void restartInfiltrationMode()
    {
        RemoveMacguffin();
        _routeModel.clear();
        _currentRound = 0;
        InfiltrationModeBegun();
    }

    public List<InfiltratorController> GetAllInfiltrators()
    {
        List<InfiltratorController> infiltratorControllers = FindObjectsOfType<InfiltratorController>().ToList();
        return infiltratorControllers;
    }

    private void RemoveMacguffin()
    {
        foreach (MacGuffinController controller in FindObjectsOfType<MacGuffinController>())
        {
            Destroy(controller.gameObject);
        }
    }

    private void RemoveInfiltrators()
    {
        foreach (InfiltratorController controller in FindObjectsOfType<InfiltratorController>())
        {
            Destroy(controller.gameObject);
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
            pointOnRectangle = Rect.NormalizedToPoint(bounds, new Vector2(Random.value, 0f));
        }
        else if (side == 1)
        {
            pointOnRectangle = Rect.NormalizedToPoint(bounds, new Vector2(1.0f, Random.value));
        }
        else if (side == 2)
        {
            pointOnRectangle = Rect.NormalizedToPoint(bounds, new Vector2(Random.value, 1.0f));
        }
        else
        {
            pointOnRectangle = Rect.NormalizedToPoint(bounds, new Vector2(0f, Random.value));
        }

        return new Vector3(pointOnRectangle.x, 1.2f, pointOnRectangle.y);
    }

    private void PlaceMacGuffin()
    {
        List<GameObject> possibleLocations = _buildModeLevelModel.getBuiltAreas();
        GameObject chosenArea = possibleLocations[Random.Range(0, possibleLocations.Count)];
        Instantiate(macGuffinPrefab, chosenArea.transform.position + Vector3.up, Quaternion.identity);
    }

    private bool lastRoundReached()
    {
        return this._currentRound >= _routeModel.numberOfRounds;
    }

    private bool shouldStartNewRound()
    {
        return !lastRoundReached() && (_currentRound + 1) * _routeModel.routesToDoPerRound == _routeModel.CountRoutesFinished();
    }

    void Update()
    {if(shouldStartNewRound())
        {
            this._currentRound++;
            this.BeginInfiltrationRound();
        }
    }
}