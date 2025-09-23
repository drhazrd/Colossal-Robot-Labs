using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Walk")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float playerSpeed = 6f;
    [SerializeField] private float playerTurnSpeed = 500f;


    [Header("Jump")]
    [SerializeField] private float playerJumpSpeed = 8f;
    private float gravity = -9.8f;
    public float jumpHeight = 1f;
    private Vector3 velocity;
    private bool isGrounded;

    private PlayerActions playerActions;
    private PlayerActions.ControlsActions controls;


    private Vector3 playerInput;
    private Vector3 skewedInput;
    private Vector3 lastPosition;

    void Awake()
    {
        playerActions = new PlayerActions();
        characterController = GetComponent<CharacterController>();
        controls = playerActions.Controls;

        controls.Attack.performed += _ => Attack();
        controls.Jump.performed += _ => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        PlayerRotate();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        //TrackSpeed();
    }

#region Movement

    //Check movement speed when moving diagonally
    private void TrackSpeed()
    {
        Vector3 displacement = transform.position - lastPosition;
        var currentSpeed = displacement.magnitude / Time.fixedDeltaTime;

        if (currentSpeed < 0.01f)
            currentSpeed = 0f;

        Debug.Log("Player Speed: " + currentSpeed);

        lastPosition = transform.position;
    }

    private void GatherInput()
    {
        Vector3 inputMovement = Vector3.zero;
        inputMovement.x = controls.Movement.ReadValue<Vector2>().x;
        inputMovement.z = controls.Movement.ReadValue<Vector2>().y;

        Vector3 rawPlayerInput = inputMovement;

        playerInput = Vector3.ClampMagnitude(rawPlayerInput, 1f);
    }

    private void PlayerRotate()
    {
        if(playerInput != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            skewedInput = matrix.MultiplyPoint3x4(playerInput);

            var rot = Quaternion.LookRotation(skewedInput, Vector3.up);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, playerTurnSpeed * Time.deltaTime);
        }
    }
    private void MovePlayer()
    {
        isGrounded = characterController.isGrounded;

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 move = skewedInput * playerInput.normalized.magnitude * playerSpeed;

        characterController.Move((move + velocity) * Time.deltaTime);
    }
    #endregion

#region Actions
    private void Jump()
    {
        if (!isGrounded)
        {
            return;
        }

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    private void Attack()
    {
        throw new NotImplementedException();
    }
    #endregion

    void OnEnable()
    {
        playerActions.Enable();
    }

    void OnDisable()
    {
        playerActions.Disable();
    }
}
