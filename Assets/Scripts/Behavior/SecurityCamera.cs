using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public Dictionary<InfiltratorController, float> spotScores;

    private List<InfiltratorController> _detectionTargets;
    public bool _isRecording;
    public float SPOTTING_THRESHOLD;


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
                    RaycastHit hitInfo;
                    Physics.Raycast(this.transform.position,
                        infiltrator.transform.position - this.transform.position, out hitInfo);
                    if (hitInfo.transform.GetComponent<InfiltratorController>() == infiltrator && infiltrator != null)
                    {
                        increaseSpotScore(infiltrator);

                        Debug.DrawRay(this.transform.position,
                            infiltrator.transform.position - this.transform.position, getRayColor(spotScores[infiltrator], SPOTTING_THRESHOLD));
                        if (spotScores[infiltrator] >= SPOTTING_THRESHOLD)
                        {
                            infiltrator.spottedByCamera(this);
                        }
                    }
                }
            }
        }
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