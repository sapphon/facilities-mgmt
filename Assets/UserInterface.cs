using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    public enum UserInterfaceMode
    {
        DEBUG, PRODUCTION
    }

    public UserInterfaceMode currentMode = UserInterfaceMode.DEBUG;
}
