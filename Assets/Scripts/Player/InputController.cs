using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    [Range(0,100)]
    public float speed = 15;
    [Range(0, 100)]
    public float jumpHeight = 30;
    private bool isOnGround;
    public GameObject interactedObject;
    private BoxCollider trigger;
    private Rigidbody rb;

    private void Start()
    {
        trigger = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        isOnGround = true;
    }

    private void Update()
    {

        if (Input.GetAxis("Jump") > 0 && isOnGround)
        {
            //jump
            rb.AddForce(new Vector3(0, Input.GetAxis("Jump") * jumpHeight * 20, 0));
            //Debug.Log("Jump!!");
        }
        else if (Input.GetAxis("Horizontal") != 0 && isOnGround)
        {
            //go right/left
            rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, 0);
            //Debug.Log("Input: " + Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Use") > 0 && interactedObject)        {
            //trigger action associated with the object
            interactedObject.GetComponent<InteractiveObject>().Interact(this.gameObject);
            interactedObject = null;
        }
        if (Input.GetAxis("Restart") > 0)
        {
            //restart level
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            isOnGround = true;
        }

        if (other.tag == "Object")
        {
            interactedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            isOnGround = false;
        }

        if (other.tag == "Object")
        {
            interactedObject = null;
        }
    }
}
