using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllers : MonoBehaviour
{
    public float movementSpeed, jumpForce, rotateSpeed;
    // public Rigidbody rb;
    public CharacterController characterController;
    private Vector3 movementDirection;
    public float gravityScale;
    public Animator animator;
    public Transform pivot;
    public GameObject playerModel;

    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, rb.velocity.y, Input.GetAxis("Vertical") * movementSpeed);
        // movementDirection = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, movementDirection.y, Input.GetAxis("Vertical") * movementSpeed);
        float yStore = movementDirection.y;
        movementDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        movementDirection = movementDirection.normalized * movementSpeed;
        movementDirection.y = yStore;

        if(characterController.isGrounded)
        {
            movementDirection.y = 0f;
            if(Input.GetButtonDown("Jump"))
            {
                // rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                movementDirection.y = jumpForce;
            }
        } 

        movementDirection.y = movementDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        characterController.Move(movementDirection * Time.deltaTime);

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(movementDirection.x, 0f, movementDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        animator.SetBool("IsGrounded", characterController.isGrounded);
        animator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Horizontal"))+ Mathf.Abs(Input.GetAxis("Vertical"))));
    }
}
