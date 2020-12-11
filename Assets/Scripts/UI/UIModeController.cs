using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModeController : MonoBehaviour
{
    private BuildModeLevelModel _buildModeModel;
    private Text _buttonText;
    private string _mode;

    void Start()
    {
        this._mode = "BUILD";
        this._buildModeModel = FindObjectOfType<BuildModeLevelModel>();
        Button button = GetComponentInChildren<Button>();
        this._buttonText = button.GetComponentInChildren<Text>();
        button.onClick.AddListener(() => { this.tryAdvanceModes(); });
    }

    void Update()
    {
        SetButtonText();
    }

    void SetButtonText()
    {
        this._buttonText.text = this._mode == "BUILD" ? getBuildModeText() : getInfiltrateModeText();
    }

    private string getInfiltrateModeText()
    {
        return "Infiltrating...";
    }

    private string getBuildModeText()
    {
        return _buildModeModel.AllRequirementsMet() ? "FINISH BUILD" : "COMPANY REQS NOT MET";
    }

    bool tryAdvanceModes()
    {
        if (this._mode == "BUILD" && _buildModeModel.AllRequirementsMet())
        {
            this._mode = "INFILTRATE";
            return true;
        }

        return false;
    }
}
