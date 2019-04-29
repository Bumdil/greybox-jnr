using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

    public bool isClockwise = true;
    public float rotationAngle = 0;
    private float clockwiseMultiplier = 0f;

	// Add a negative 1 multiplier to rotation for counter clockwise
	void Start () {
        clockwiseMultiplier = (isClockwise ? -1f : 1f);
	}

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
