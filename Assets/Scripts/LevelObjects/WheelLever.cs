using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelLever : InteractiveObject {

    public RotationController wheel;
    [Range(15,90)][Tooltip("Set how many degrees per second the wheel will rotate.")]
    public float wheelRotationAngle = 45f;

    /// <summary>
    /// Start the wheel's rotation.
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(GameObject player)
    {
        wheel.rotationAngle = wheelRotationAngle;
        Debug.Log("Start wheel rotation");
    }
}
