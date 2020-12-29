using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private NavMeshAgent _agent;
    private float GOAL_THRESHOLD = 2f;
    private BuildModeLevelModel _facilityAreaModel;

    // Start is called before the first frame update
    void Start()
    {
        this._agent = GetComponent<NavMeshAgent>();
        this._facilityAreaModel = FindObjectOfType<BuildModeLevelModel>();
    }

    // Update is called once per frame
    void Update()
    {
        Meander();
    }

    private void Meander()
    {
        if (distanceToGoal() < GOAL_THRESHOLD)
        {
            pickNewRandomWalkGoal();
        }
    }

    private void pickNewRandomWalkGoal()
    {
        AreaPartModel nextDestination = UU.ChooseRandomFromList<AreaPartModel>(_facilityAreaModel.getBuiltAreas().Select((area) => area.GetComponent<AreaPartModel>()).ToList());
        _agent.destination = new Vector3(nextDestination.getBoundingRect().center.x, 0.2f,
            nextDestination.getBoundingRect().center.y);
    }

    private float distanceToGoal()
    {
        return Vector3.Distance(this.transform.position, _agent.destination);
    }
}
