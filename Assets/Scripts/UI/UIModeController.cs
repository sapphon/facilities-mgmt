using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIModeController : MonoBehaviour
{
    private BuildModeLevelModel _buildModeModel;
    private Text _buttonText;
    private string _mode;
    private InfiltrationModeController _infilModeController;

    void Start()
    {
        this._mode = "BUILD";
        this._buildModeModel = FindObjectOfType<BuildModeLevelModel>();
        this._infilModeController = FindObjectOfType<InfiltrationModeController>();
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
            EndBuildMode();
            this._mode = "INFILTRATE";
            _infilModeController.InfiltrationModeBegun();
            return true;
        }

        return false;
    }

    private void EndBuildMode()
    {
        FindObjectOfType<NavMeshSurface>().BuildNavMesh();
        Destroy(FindObjectOfType<ConstructionPicker>().gameObject);
    }
}
