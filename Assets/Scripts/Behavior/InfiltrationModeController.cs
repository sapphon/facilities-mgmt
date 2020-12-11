using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InfiltrationModeController : MonoBehaviour
{
    public GameObject macGuffinPrefab;
    private Camera _mainCamera;
    private BuildModeLevelModel _buildModeLevelModel;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _buildModeLevelModel = FindObjectOfType<BuildModeLevelModel>();
    }

    public void InfiltrationModeBegun()
    {
        PlaceMacGuffin();
    }

    private void PlaceMacGuffin()
    {
        List<GameObject> possibleLocations = _buildModeLevelModel.getBuiltAreas();
        GameObject chosenArea = possibleLocations[Random.Range(0, possibleLocations.Count)];
        Instantiate(macGuffinPrefab, chosenArea.transform.position + Vector3.up, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}