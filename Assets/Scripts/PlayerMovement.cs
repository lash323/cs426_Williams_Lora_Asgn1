// This script was modified by following a tutorial made by Sevastian Graves 
// youtube video: https://www.youtube.com/watch?v=LOC5GJ5rFFw&list=PLD_vBJjpCwJtrHIW1SS5_BNRk6KZJZ7_d&index=2


// lets make him move
// using __ imports namespace
// Namespaces are collection of classes, data types
using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;


    // MonoBehavior is the base class from which every Unity Script Derives
public class PlayerMovement : MonoBehaviour
{

    [Header("Camera Transform")]
    PlayerManager playerManager;
    Transform playerCamera;
    InputHandler inputHandler;
    Vector3 moveDirection;

    [HideInInspector]
    public Transform t;
    public Rigidbody rb;


// SerializedField allows you to see it in Inspector
    [Header("Movement Stats")] // what does this do?
    [SerializeField] public float speed = 5;
    [SerializeField] public float rotationSpeed = 10;


    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();

        inputHandler = GetComponent<InputHandler>();
        playerCamera = Camera.main.transform;

    }

    #region Movement
    Vector3 normalVector;
    Vector3 targetPosition;
    Quaternion targetRotation;
    Quaternion playerRotation;

    public void HandleAllMovement(float delta) {
        HandleRotation(delta);
        HandleMovement(delta);
    }

    private void HandleRotation(float delta) {

        if (playerManager.isAiming) {
            targetRotation = Quaternion.Euler(0, playerCamera.eulerAngles.y, 0);
            playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * delta);
            transform.rotation = playerRotation;
        }
        else {
            targetRotation = Quaternion.Euler(0, playerCamera.eulerAngles.y, 0);
            playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * delta);

            // roate player based on camera angle
            if (inputHandler.mouseX != 0 || inputHandler.mouseY != 0) {
                transform.rotation = playerRotation;
            }

            // rotate player based on movement
            if (inputHandler.horizontal != 0 || inputHandler.vertical != 0) {
            
            // get movement and camera direction
                Vector3 targetDirection = new Vector3(inputHandler.horizontal, 0, inputHandler.vertical);
                targetDirection = Quaternion.Euler(0, playerCamera.eulerAngles.y, 0) * targetDirection;

                targetRotation = Quaternion.LookRotation(targetDirection);
                
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * delta * 0.5f);
            }
        } 
    }

    public void HandleMovement(float delta) {

        moveDirection = playerCamera.forward * inputHandler.vertical;
        moveDirection += playerCamera.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float movementSpeed = speed; 
        moveDirection *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rb.linearVelocity = projectedVelocity;

    }
    #endregion
}


