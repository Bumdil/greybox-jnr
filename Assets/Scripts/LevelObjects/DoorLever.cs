using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLever : InteractiveObject
{

    public Transform door;
    public RotationController wheel;

    private float doorSpeed = 2;
    private bool isDoorLocked = true;
    private bool isDoorOpening = false;

    /// <summary>
    /// On each interaction change the rotation of the wheel.
    /// On the first interaction also open the door.
    /// </summary>
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
