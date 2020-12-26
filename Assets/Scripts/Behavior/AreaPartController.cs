using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AreaPartController : MonoBehaviour
{
    private AreaPartModel model;
    public GameObject obstaclePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        obstaclePrefab = Resources.Load<GameObject>("Prefabs/FacilityPieces/Obstacles/StaticObstacle");
        model = GetComponent<AreaPartModel>();
        GenerateObstacles(model);
    }

    private void GenerateObstacles(AreaPartModel fromModel)
    {
        Random.InitState(Mathf.FloorToInt(Time.time));
        for (float i = 0.5f; i < fromModel.widthAxis(); i+=2)
        {
            for(float j = 0.5f; j < fromModel.heightAxis(); j+=2)
            if (isBlockingEntrance(i, j))
            {
                continue;
            }
            else
            {
                if (Random.value > 0.5f)
                {
                    Vector2 topLeft = model.getBoundingRect().min;
                    PlaceObstacle(topLeft.x + i, topLeft.y + j);
                }
            }
        }
    }

    private float roundToHalf(float toRound)
    {
        float integerPortion = Mathf.Floor(toRound);
        float fractionalPortion = toRound - integerPortion;
        if (fractionalPortion <= 0.25f)
        {
            return integerPortion;
        }
        else if (fractionalPortion <= 0.75f)
        {
            return integerPortion + 0.5f;
        }else
            return integerPortion + 1f;
    }

    private void PlaceObstacle(float x, float z)
    {
        float heightRandomization = roundToHalf(0.5f + Random.value * 1.5f);
        GameObject obstacle = Instantiate(obstaclePrefab,
            new Vector3(x, this.transform.position.y + heightRandomization / 2f, z), Quaternion.identity, this.transform);
        obstacle.transform.localScale = new Vector3(obstacle.transform.localScale.x, heightRandomization, obstacle.transform.localScale.z);
    }

    private bool isBlockingEntrance(float x, float z)
    {
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetPartMaterial(Material toSet)
    {

        foreach (MeshRenderer renderer in this.gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (toSet == null)
            {
                renderer.enabled = false;
            }else{ 
                renderer.enabled = true;
                renderer.material = toSet;
            }
        }
    }
}
