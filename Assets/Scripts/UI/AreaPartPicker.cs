﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AreaPartPicker : MonoBehaviour
{
    
    public GameObject paneTemplate;
    public Material validMaterial;
    public Material invalidMaterial;
    private GameObject _selectedArea;
    private Plane _areaPartSurface;
    private GameObject _areaPreview;
    private BuildModeLevelModel _buildModeLevelModel;

    void Start()
    {
        _buildModeLevelModel = FindObjectOfType<BuildModeLevelModel>();
        GameObject[] areaPrefabs = _buildModeLevelModel.areaParts;
        float buttonHeight = paneTemplate.GetComponent<RectTransform>().rect.height;
        this._selectedArea = null;
        this._areaPartSurface = new Plane(Vector3.up, 0);
        this._areaPreview = new GameObject();
        AddButtonsToPanel(areaPrefabs, buttonHeight);
        ResizePanelToFitButtons(areaPrefabs, buttonHeight);
    }

    void Update()
    {
        if (_selectedArea != null)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                _selectedArea = null;
                return;
            }
            else if (Input.GetKeyUp(KeyCode.Return))
            {
                _buildModeLevelModel.Place(_areaPreview);
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
             _selectedArea.transform.Rotate(Vector3.up, 90);   
            }
            ShowSelectedAreaAtCursor();
        }
        else
        {
            DisablePreview();
        }
    }

    private void ShowSelectedAreaAtCursor()
    {
        SetPreviewMeshes();
        SetPreviewLocation();
        SetPreviewMaterial(_buildModeLevelModel.IsValidNextPlacement(_areaPreview) ? validMaterial : invalidMaterial);
    }

    private void SetPreviewMeshes()
    {
        Destroy(this._areaPreview);
        this._areaPreview = Instantiate(_selectedArea);
    }

    private void DisablePreview()
    {
        SetPreviewMaterial(null);
    }

    private void SetPreviewMaterial(Material toSet)
    {

        foreach (MeshRenderer renderer in this._areaPreview.GetComponentsInChildren<MeshRenderer>())
        {
            if (toSet == null)
            {
                renderer.enabled = false;
            }else{ 
                renderer.enabled = true;
                renderer.material = toSet;
            }
        }
    }

    private Vector3 LockToUnitGrid(Vector3 unlocked)
    {
        return new Vector3(roundToHalf(unlocked.x), roundToHalf(unlocked.y), roundToHalf(unlocked.z));
    }

    float roundToHalf(float input)
    {
        float floor = Mathf.Floor(input);
        float relevant = input - floor;
        if (relevant < .25)
        {
            return floor;
        }
        else if (relevant < .75)
        {
            return floor + .5f;
        }
        else
        {
            return floor + 1f;
        }
    }

    private void SetPreviewLocation()
    {
        this._areaPreview.transform.position = LockToUnitGrid(getCursorPositionOnBuildingPlane()) + new Vector3(0,.2f,0);
    }

    private Vector3 getCursorPositionOnBuildingPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (_areaPartSurface.Raycast(ray, out var distance))
        {
            return ray.GetPoint(distance);
        }
        throw new Exception("Cannot find building surface");
    }

    private void AddButtonsToPanel(GameObject[] areaPrefabs, float buttonHeight)
    {
        void buttonClicked(int index)
        {
            this._selectedArea = areaPrefabs[index];
        }
        for (int i = 0; i < areaPrefabs.Length; i++)
        {
            var paneObject = CreateButton();
            PositionButtonVertically(-i * (buttonHeight + 5f), paneObject);
            Button button = paneObject.GetComponentInChildren<Button>();
            SetButtonText(areaPrefabs[i].name, button.gameObject);
            SetPaneText(0, _buildModeLevelModel.numberOfPartsRequired[i], _buildModeLevelModel.numberOfPartsAllowed[i],
                paneObject);
            int youHaveToDoThisInCSharpItsSilly = i;
            SetButtonCallback(() => buttonClicked(youHaveToDoThisInCSharpItsSilly), button);
        }
    }

    private void SetPaneText(int used, int required, int availableTotal, GameObject paneObject)
    {
        Text[] componentsInChildren = paneObject.GetComponentsInChildren<Text>();
        componentsInChildren[componentsInChildren.Length - 1].text = componentsInChildren[componentsInChildren.Length - 1].text.Replace("USED?", used + " USED").Replace("REQUIRED?", required + " REQUIRED").Replace("MAX?", availableTotal + " MAXIMUM");
    }

    private void SetButtonCallback(UnityAction action, Button button)
    {
        button.onClick.AddListener(action);
    }

    private static void PositionButtonVertically(float offset, GameObject paneObject)
    {
        paneObject.transform.Translate(0, offset, 0);
    }

    private GameObject CreateButton()
    {
        return Instantiate(paneTemplate, this.gameObject.transform);
    }

    private static void SetButtonText(String text, GameObject buttonObject)
    {
        Text buttonText = buttonObject.GetComponentInChildren<Text>();
        buttonText.text = text;
    }

    private void ResizePanelToFitButtons(GameObject[] areaPrefabs, float buttonHeight)
    {
        this.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, areaPrefabs.Length * (buttonHeight + 5f) - 5f);
    }
}