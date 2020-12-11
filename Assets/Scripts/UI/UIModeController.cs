using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModeController : MonoBehaviour
{
    private BuildModeLevelModel _buildModeModel;
    private Text _buttonText;

    // Start is called before the first frame update
    void Start()
    {
        this._buildModeModel = FindObjectOfType<BuildModeLevelModel>();
        this._buttonText = GetComponentInChildren<Button>().GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        SetButtonText();
    }

    void SetButtonText()
    {
        this._buttonText.text = _buildModeModel.AllRequirementsMet() ? "FINISH BUILD" : "COMPANY REQS NOT MET";
    }
}
