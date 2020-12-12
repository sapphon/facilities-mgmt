using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Model;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class InfiltratorController : MonoBehaviour
{

    private Route _route;

    private float GOAL_THRESHOLD = 3.0f;
    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._route = new Route(this.transform.position);
        this._navMeshAgent.destination = FindObjectOfType<MacGuffinController>().transform.position;
    }

    void Update()
    {
        this._route.AddMove(this.transform.position);
        if (distanceToGoal() < GOAL_THRESHOLD)
        {
            FindObjectOfType<InfiltrationModeRouteModel>().recordFinishedRoute(this._route);
            Destroy(this.gameObject);
        }
    }

    private float distanceToGoal()
    {
        return Vector3.Distance(this.transform.position, _navMeshAgent.destination);
    }
}
