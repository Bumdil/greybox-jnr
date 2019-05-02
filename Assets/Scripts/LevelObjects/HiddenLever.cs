using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenLever : InteractiveObject
{

    public RotationController[] wheels;


    /// <summary>
    /// On each interaction change the rotation of the wheels.
    /// On the first interaction also open the door.
    /// </summary>
    /// <param name="player">the player object that triggered the interaction. Unnecessary in this case</param>
    public override void Interact(GameObject player)
    {
        Debug.Log("Congrats! You took the hard way to a dead end. But good job making it this far!!");
        foreach (RotationController wheel in wheels)
        {
            wheel.isClockwise = !wheel.isClockwise;
            wheel.rotationAngle = 45;
        }
    }
}
