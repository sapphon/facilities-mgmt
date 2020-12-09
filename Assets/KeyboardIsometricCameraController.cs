using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardIsometricCameraController : MonoBehaviour
{
    private float speed = .03f;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(speed, 0, speed, Space.World);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(-speed, 0, -speed, Space.World);

        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-speed, 0, speed, Space.World);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(speed, 0, -speed, Space.World);
        }
    }
}
