using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [Range(0,2000)][Tooltip("The amount of force to apply to horizontal movement")]
    public float moveForce = 365f;
    [Range(0,2000)][Tooltip("The amount of force to apply to a jump")]
    public float jumpForce = 1000f;
    [Range(0,50)][Tooltip("The maximum horizontal speed the player can have")]
    public float maxSpeed = 15;
    public Transform rightFoot; // right bottom corner of player game object
    public Transform leftFoot; // left bottom corner of player game object

    private bool isJumping; // flag: is player jumping in this frame?
    private bool isOnGround; // flag: is player touching the ground, i.e. can he possible jump or move?
    private bool isRestarting; // flag: is the game currently in process of being restarted?
    private float jumpDelay = 0f; // cooldown to avoid unwanted double jumps in consecutive frames
    private InteractiveObject interactedObject; // an interactive object the player could interact with by pressing E
    private Rigidbody rb; // the player's rigidbody

    #region built in unity function overrides
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Handle Physics, i.e. movement in x and y directions
    void FixedUpdate()
    {
        //horizontal movement
        float h = Input.GetAxis("Horizontal");

        if (h * rb.velocity.x < maxSpeed)
        {
            if (isOnGround)
                rb.AddForce(Vector3.right * h * moveForce); // on ground, add full move force
            else
                rb.AddForce(Vector3.right * h * moveForce / 30f); // in the air add reduced force to simulate less control
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

        //jumping
        if (isJumping)
        {
            rb.AddForce(new Vector3(0f, jumpForce));
            isJumping = false;
        }
    }

    // Update player behavior
    private void Update()
    {
        //Ground Check
        isOnGround = Physics.Linecast(transform.position, rightFoot.position, 1 << LayerMask.NameToLayer("Ground")) ||
                     Physics.Linecast(transform.position, leftFoot.position, 1 << LayerMask.NameToLayer("Ground"));

        //avoid multiple triggers of jump
        if (jumpDelay > 0f)
            jumpDelay -= Time.deltaTime;
        
        // check for user input
        CheckInputs();

        // reset if player fell off map
        if (transform.position.y < -5f && !isRestarting)
        {
            Debug.Log("You fell off the map. The game will be restarting!");
            StartCoroutine(RestartScene(0.5f));
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // player is in range of an interactive object.
        if (other.tag == "Object")
        {
            interactedObject = other.GetComponent<InteractiveObject>();
            if (!interactedObject)
            {
                Debug.LogError(other.name + " is tagged Object but does not have an Interactive Object script assigned.");
            }
        }

        // player has reached the finish.
        if (other.tag == "Finish")
        {
            Debug.Log("Game Over. You won!");
            this.enabled = false;
            StartCoroutine(RestartScene(2));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // player has walked out of reach of an interactive object
        if (other.tag == "Object")
        {
            interactedObject = null;
        }
    }

    #endregion

    #region utility functions
    /// <summary>
    /// Check if user is currently pressing jump, use or restart and react accordingly if they do.
    /// </summary>
    private void CheckInputs()
    {
        if (Input.GetAxis("Jump") > 0 && isOnGround && jumpDelay <= 0f)
        {
            isJumping = true;
            jumpDelay = 0.25f;
        }
        if (Input.GetAxis("Use") > 0 && interactedObject)
        {
            //trigger action associated with the object
            interactedObject.Interact(this.gameObject);
            interactedObject = null;
        }
        if (Input.GetAxis("Restart") > 0 && !isRestarting)
        {
            //restart level
            Debug.Log("Player initiated Restart!");

            StartCoroutine(RestartScene(0));
        }
    }

    /// <summary>
    /// Restart the scene after a delay.
    /// </summary>
    /// <param name="delay">delay before restart in seconds.</param>
    /// <returns></returns>
    private IEnumerator RestartScene(float delay)
    {
        isRestarting = true;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("scene");
        isRestarting = false;
    }
    #endregion

}
