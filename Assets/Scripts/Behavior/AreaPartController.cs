using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPartController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
