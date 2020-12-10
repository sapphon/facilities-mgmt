using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaPartPicker : MonoBehaviour
{

    public GameObject paneTemplate;
    void Start()
    {
        GameObject[] areaPrefabs = FindObjectOfType<BuildModeLevelModel>().areaParts;
        float buttonHeight = paneTemplate.GetComponent<RectTransform>().rect.height;
        
        AddButtonsToPanel(areaPrefabs, buttonHeight);
        ResizePanelToFitButtons(areaPrefabs, buttonHeight);
    }

    private void AddButtonsToPanel(GameObject[] areaPrefabs, float buttonHeight)
    {
        for (int i = 0; i < areaPrefabs.Length; i++)
        {
            var paneObject = CreateButton();
            PositionButtonVertically(-i * (buttonHeight + 5f), paneObject);
            SetButtonText(areaPrefabs[i].name, paneObject);
        }
    }

    private static void PositionButtonVertically(float offset, GameObject paneObject)
    {
        paneObject.transform.Translate(0, offset, 0);
    }

    private GameObject CreateButton()
    {
        return Instantiate(paneTemplate, this.gameObject.transform);
    }

    private static void SetButtonText(String text, GameObject paneObject)
    {
        GameObject buttonObject = paneObject.GetComponentInChildren<Button>().gameObject;
        Text buttonText = buttonObject.GetComponentInChildren<Text>();
        buttonText.text = text;
    }

    private void ResizePanelToFitButtons(GameObject[] areaPrefabs, float buttonHeight)
    {
        this.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, areaPrefabs.Length * (buttonHeight + 5f) - 5f);
    }
}