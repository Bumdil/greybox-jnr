using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {

    public bool isClockwise = true;
    private float clockwiseMultiplier = 0f;

	// Add a negative 1 multiplier to rotation for counter clockwise
	void Start () {
        clockwiseMultiplier = (isClockwise ? -1f : 1f);
	}
	
	// Each second, rotate by 45 degrees.
	void Update () {
        clockwiseMultiplier = (isClockwise ? -1f : 1f); // check each frame as this may be manipulated.
        this.transform.Rotate(new Vector3(0, 0, 45) * clockwiseMultiplier * Time.deltaTime);
	}
}
