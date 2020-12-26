using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public Dictionary<InfiltratorController, float> spotScores;

    private List<InfiltratorController> _detectionTargets;
    public bool _isRecording;

    void Update()
    {
        if (_isRecording)
        {
            foreach (InfiltratorController infiltrator in _detectionTargets)
            {
                if (infiltrator != null)
                {
                    RaycastHit hitInfo;
                    Physics.Raycast(this.transform.position,
                        infiltrator.transform.position - this.transform.position, out hitInfo);
                    if (hitInfo.transform.GetComponent<InfiltratorController>() == infiltrator && infiltrator != null)
                    {
                        //spotScores[infiltrator] += 1;
                        //infiltrator.spottedByCamera(this);
                        Debug.DrawRay(this.transform.position,
                            infiltrator.transform.position - this.transform.position);
                    }
                }
            }
        }
    }

    public void SetInfiltrators(List<InfiltratorController> controllers)
    {
        this._detectionTargets = controllers;
    }

    public bool BeginRecording()
    {
        this._isRecording = true;
        return _isRecording;
    }
}