using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Player PlayerScript;
    public void PlayerTap()
    {
        PlayerScript.FlyUp();
    }
}
