using System;
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
    private GameObject _selectedArea;
    private Plane _areaPartSurface;
    private GameObject _areaPreview;

    void Start()
    {
        GameObject[] areaPrefabs = FindObjectOfType<BuildModeLevelModel>().areaParts;
        float buttonHeight = paneTemplate.GetComponent<RectTransform>().rect.height;
        this._selectedArea = null;
        this._areaPartSurface = new Plane(new Vector3(0, 1, 0), 0);
        this._areaPreview = new GameObject();
        AddButtonsToPanel(areaPrefabs, buttonHeight);
        ResizePanelToFitButtons(areaPrefabs, buttonHeight);
    }

    void Update()
    {
        if (_selectedArea != null)
        {
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
        SetPreviewLocation(getCursorPositionOnBuildingPlane());
        SetPreviewMaterial(validMaterial);
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
            renderer.material = toSet;
        }
    }

    private void SetPreviewLocation(Vector3 vector3)
    {
        this._areaPreview.transform.position = getCursorPositionOnBuildingPlane() + new Vector3(0,.5f,0);
    }

    private Vector3 getCursorPositionOnBuildingPlane()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (_areaPartSurface.Raycast(ray, out distance))
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
            int youHaveToDoThisInCSharpItsSilly = i;
            SetButtonCallback(() => buttonClicked(youHaveToDoThisInCSharpItsSilly), button);
        }
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