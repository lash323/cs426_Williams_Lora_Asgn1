// This script was made by following a tutorial made by Sevastian Graves 
// youtube video: https://www.youtube.com/watch?v=LOC5GJ5rFFw&list=PLD_vBJjpCwJtrHIW1SS5_BNRk6KZJZ7_d&index=2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    // class from input controls package
    PlayerControls inputActions;
    PlayerUIManager playerUIManager;
    PlayerManager playerManager;

    [Header("Player Movement")]
    public float horizontal;
    public float vertical;
    public float moveAmount;
    Vector2 movementInput;
    

    [Header("Camera Roation")]
    public float mouseX;
    public float mouseY;
    Vector2 cameraInput;

    [Header("Button Inputs")]
    public bool aimingInput;
    public bool shootInput;

    public void Awake() {
        playerUIManager = FindAnyObjectByType<PlayerUIManager>();
        playerManager = FindAnyObjectByType<PlayerManager>();
    }


    public void OnEnable() {

        if (inputActions == null) {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
           
            // aiming continues until button is let go
            inputActions.PlayerActions.Aim.performed += i => aimingInput = true;
            inputActions.PlayerActions.Aim.canceled += i => aimingInput = false;
            
            // shooting continues until button is let go
            inputActions.PlayerActions.Shoot.performed += i => shootInput = true;
            inputActions.PlayerActions.Shoot.canceled += i => shootInput = false;

            // Note: => is a lambda expression: (parameters) => expression_or_block_of_code
            // Note: .performed is an event; += is the listener? 
        }

        inputActions.Enable();
    }
    
    public void OnDisable() {
        inputActions.Disable();
    }

    public void HandleAllInput() {
        HandleMoveInput();
        HandleCameraInput();
        HandleAimingInput();
        HandleShootingInput();
    }

    public void HandleMoveInput() {

        // get player direcctional input
        horizontal = movementInput.x;
        vertical = movementInput.y;

        // calculate total movement 
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }

    private void HandleCameraInput() {
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleAimingInput() {

        // prevent player from aiming while moving
        if (vertical != 0 || horizontal != 0) {
            aimingInput = false;
            playerUIManager.crossHair.SetActive(false);
            return;
        }

        // activate crosshair while aiming
        if (aimingInput) {
            playerUIManager.crossHair.SetActive(true);
        }
        else {
            playerUIManager.crossHair.SetActive(false);
        }
        
    }

    private void HandleShootingInput() {

        if (shootInput && aimingInput) {
            shootInput = false;
            
            // used for debugging before implemnted bullets (look at console)
            Debug.Log("Bang!");

            playerManager.UseWeapon();
        }
    }
}

