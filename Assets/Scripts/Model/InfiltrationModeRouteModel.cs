﻿using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class InfiltrationModeRouteModel : MonoBehaviour
{
    public int routesToDoPerRound = 10;
    public int numberOfRounds = 10;
    private List<Route> recordedRoutes;
    // Start is called before the first frame update
    void Start()
    {
        recordedRoutes = new List<Route>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            foreach(Route route in recordedRoutes)
            {
                List<Vector3> moves = route.getMoves();
                for (int i = 0; i < moves.Count - 1; i++)
                {
                    Debug.DrawLine(moves[i], moves[i+1], getRouteColor(route), 5f);
                }
            }
        }
    }

    public Color getRouteColor(Route route)
    {
        return route.HasEnded() ? route.WasSuccessful() ? Color.green : Color.red : Color.yellow;
    }

    public void clear()
    {
        this.recordedRoutes = new List<Route>();
    }

    public int CountRoutesFinished()
    {
        return recordedRoutes.Count;
    }

    public void recordFinishedRoute(Route done)
    {
        this.recordedRoutes.Add(done);
    }
}
