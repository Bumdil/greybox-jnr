using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {

    [HideInInspector] public bool jump = false;
    public float moveForce = 365f;
    public float jumpForce = 1000f;
    public float maxSpeed = 15;
    public Transform groundCheck;

    public bool isOnGround;
    private bool isRestarting;
    private GameObject interactedObject;
    private Rigidbody rb;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isOnGround = true;
    }

    // Handle Physics, i.e. movement in x and y
    void FixedUpdate()
    {
        //horizontal movement
        float h = Input.GetAxis("Horizontal");

        if (h * rb.velocity.x < maxSpeed && isOnGround)
            rb.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

        //jumping
        if (jump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    // Handle Input
    private void Update()
    {
        //Ground Check
        isOnGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetAxis("Jump") > 0 && isOnGround)
        {
            jump = true;
        }
        if (Input.GetAxis("Use") > 0 && interactedObject)        {
            //trigger action associated with the object
            interactedObject.GetComponent<InteractiveObject>().Interact(this.gameObject);
            interactedObject = null;
        }
        if (Input.GetAxis("Restart") > 0 && !isRestarting)
        {
            //restart level
            Debug.Log("Player initiated Restart!");

            StartCoroutine(RestartScene(0));
        }

        // reset if player fell off map
        if (transform.position.y < -1f && !isRestarting)
        {
            Debug.Log("You fell off the map. The game will be restarting!");
            StartCoroutine(RestartScene(1));
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Object")
        {
            interactedObject = other.gameObject;
        }

        if (other.tag == "Finish")
        {
            Debug.Log("Game Over. You won!");
            this.enabled = false;
            StartCoroutine(RestartScene(3));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Object")
        {
            interactedObject = null;
        }
    }

    private IEnumerator RestartScene(float delay)
    {
        isRestarting = true;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("scene");
        isRestarting = false;
    }

}
