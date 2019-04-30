using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour {

    /// <summary>
    /// Interact with the object. Must be implemented by each object individually to match specific interaction demands.
    /// </summary>
    /// <param name="player">player object that interacted with the object. Might be necessary for some interactions.</param>
    public abstract void Interact(GameObject player);
}
