using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLever : InteractiveObject
{

    public Transform door;
    public RotationController wheel;

    private float doorSpeed = 2; // speed to open door
    private bool isDoorLocked = true; // flag: is the door still locked?
    private bool isDoorOpening = false; //flag: is the door currently opening?


    /// <summary>
    /// On each interaction change the rotation of the wheel.
    /// On the first interaction also open the door.
    /// </summary>
    /// <param name="player">the player object that triggered the interaction. Unnecessary in this case</param>
    public override void Interact(GameObject player)
    {
        Debug.Log("Interaction with Lever");
        wheel.isClockwise = !wheel.GetComponent<RotationController>().isClockwise;
        wheel.rotationAngle = 45;
        if (isDoorLocked && !isDoorOpening) isDoorOpening = true;
    }

    /// <summary>
    /// Continously open door if not opened already.
    /// </summary>
    private void Update()
    {
        if (isDoorOpening)
        {
            Vector3 targetDoorPosition = new Vector3(door.position.x, 25f, 0);
            if (door.position.y > 25f)
            {
                door.position = Vector3.MoveTowards(door.position, targetDoorPosition, doorSpeed * Time.deltaTime);
            }
            else
            {
                isDoorLocked = false;
                isDoorOpening = false;
            }
        }
    }

}
