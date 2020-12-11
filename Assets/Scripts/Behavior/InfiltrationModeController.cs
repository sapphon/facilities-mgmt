using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiltrationModeController : MonoBehaviour
{
    public GameObject macGuffinPrefab;
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        PlaceMacGuffin();
    }

    private void PlaceMacGuffin()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
