using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaPartPicker : MonoBehaviour
{

    public Sprite buttonBackground;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] areaPrefabs = FindObjectOfType<BuildModeLevelModel>().areaParts;
        for (int i = 0; i < areaPrefabs.Length; i++)
        {
            GameObject buttonObject = DefaultControls.CreateButton(new DefaultControls.Resources());
            buttonObject.transform.SetParent(this.gameObject.transform);
            Text buttonText = buttonObject.GetComponentInChildren<Text>();
            buttonText.text = areaPrefabs[i].name;
            Image buttonImage = buttonObject.GetComponent<Image>();
            buttonImage.sprite = buttonBackground;
        }
    }

// Update is called once per frame
    void Update()
    {
    }
}