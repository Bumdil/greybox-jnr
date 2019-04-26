using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractiveObject
{

    public Transform door;
    public Transform wheel;

    private float doorSpeed = 2;
    private bool isDoorLocked = true;
    private bool isDoorOpening = false;

    public override void Interact(GameObject player)
    {
        Debug.Log("Interaction with Lever");
        wheel.GetComponent<RotationController>().isClockwise = !wheel.GetComponent<RotationController>().isClockwise;
        if (isDoorLocked && !isDoorOpening) isDoorOpening = true;
    }


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
