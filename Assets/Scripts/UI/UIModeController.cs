﻿using System.Collections;
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
    private UserInterface _ui;

    void Start()
    {
        this._mode = "BUILD";
        this._ui = FindObjectOfType <UserInterface>();
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
        return "Infiltrating..." + (_ui.currentMode == UserInterface.UserInterfaceMode.DEBUG ? " (RESTART)" : "");
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
        else if (
                 _ui.currentMode == UserInterface.UserInterfaceMode.DEBUG)
        {
            return tryRestartMode();
        }

        return false;
    }

    bool tryRestartMode()
    {
        if (this._mode == "INFILTRATE")
        {
            _infilModeController.restartInfiltrationMode();
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
