using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

    [Tooltip("Indicate whether the rotation of the wheel should be clockwise or counter clockwise")]
    public bool isClockwise = true;
    [HideInInspector]public float rotationAngle = 0;
    private float clockwiseMultiplier = 0f; // is used to controll whether the rotation happens clockwise or counter clockwise.

    // Each second, rotate by rotationAngle.
    void Update()
    {
        if (Mathf.Abs(rotationAngle) > 0)
        {
            clockwiseMultiplier = (isClockwise ? -1f : 1f); // check each frame as this may be manipulated.
            this.transform.Rotate(new Vector3(0, 0, rotationAngle) * clockwiseMultiplier * Time.deltaTime);
        }
    }
}
