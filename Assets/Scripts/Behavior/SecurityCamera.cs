using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public Dictionary<InfiltratorController, float> spotScores;

    private List<InfiltratorController> _detectionTargets;
    public bool _isRecording;
    public float spottingThreshold;
    [Range(15f, 180f)]
    public float fovAngleFromCenter;
    [Range(5f, 100f)] 
    public float maximumRange;
    
    void Start()
    {
        spotScores = new Dictionary<InfiltratorController, float>();
    }

    void Update()
    {
        if (_isRecording)
        {
            foreach (InfiltratorController infiltrator in _detectionTargets)
            {
                if (infiltrator != null)
                {
                    var differenceVector = infiltrator.transform.position - this.transform.position;
                    if (IsInRangeAndAngle(differenceVector))
                    {
                        RaycastHit hitInfo;
                        Physics.Raycast(this.transform.position,
                            differenceVector, out hitInfo);
                        if (hitInfo.transform.GetComponent<InfiltratorController>() == infiltrator &&
                            infiltrator != null)
                        {
                            increaseSpotScore(infiltrator);

                            Debug.DrawRay(this.transform.position,
                                differenceVector, getRayColor(spotScores[infiltrator], spottingThreshold));
                            if (spotScores[infiltrator] >= spottingThreshold)
                            {
                                infiltrator.spottedByCamera(this);
                            }
                        }
                    }
                }
            }
        }
    }

    protected virtual bool IsInRangeAndAngle(Vector3 relativePosition)
    {
        return Vector3.Angle(this.transform.forward, relativePosition) <= this.fovAngleFromCenter && 
               Vector3.SqrMagnitude(relativePosition) <= maximumRange * maximumRange;
    }

    private void increaseSpotScore(InfiltratorController infiltrator)
    {
        if (spotScores.ContainsKey(infiltrator))
        {
            spotScores[infiltrator] += 1;
        }
        else
        {
            spotScores.Add(infiltrator, 1f);
        }
    }

    private Color getRayColor(float spotScore, float spottingThreshold)
    {
        return UU.InterpolateColor(spotScore, spottingThreshold, Color.blue);
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