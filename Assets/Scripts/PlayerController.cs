using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    public float moveSpeed = 1.0f;

    private Vector3 movementVelocity;

    private PlayerInputController playerInputController;

    private float verticalVelocity;

    public float gravity = -9.0f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Awake is called when the scripts loaded before the start
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInputController = GetComponent<PlayerInputController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CalculatePlayerMovemt()
    {
        movementVelocity.Set(playerInputController.horizontalInput,0f,playerInputController.verticalInput);
        movementVelocity.Normalize();
        movementVelocity = Quaternion.Euler(0, -45f, 0) * movementVelocity;
        animator.SetFloat("Speed",movementVelocity.magnitude);

        movementVelocity *= moveSpeed * Time.deltaTime;

        if(movementVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity);
        }

        animator.SetBool("AirBorn",!characterController.isGrounded);


    }

    private void FixedUpdate()
    {

        CalculatePlayerMovemt();

        if(characterController.isGrounded == false)
        {
            verticalVelocity = gravity;
        }
        else
        {
            verticalVelocity = gravity * 0.2f;
        }
        movementVelocity += verticalVelocity * Vector3.up * Time.deltaTime;
        characterController.Move(movementVelocity);

    }

}
