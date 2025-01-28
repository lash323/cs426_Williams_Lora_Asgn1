using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerCamera : MonoBehaviour
{

    InputHandler inputHandler;
    PlayerManager playerManager;

    public Transform cameraPivot;
    public Camera cameraObject;


    [Header("Camera Follow Targets")] 
    public GameObject player; // follows player while not aiming
    public Transform aimedCameraPosition; // follows this position while aiming

    Vector3 cameraFollowVelocity = Vector3.zero;
    Vector3 targetPosition;
    Vector3 cameraRotation;
    Quaternion targetRotation;

    [Header("Camera Speed")]
    public float cameraSmoothTime = 0.2f;
    public float aimedCameraSmoothTime = 3f;

    float lookAmountVertical;
    float lookAmountHorizontal;
    float maxPivotAngle = 15;
    float minPivotAngle = -15;

    public void Awake() {
        inputHandler = player.GetComponent<InputHandler>();
        playerManager = player.GetComponent<PlayerManager>();
    }

    public void HandleAllCameraMovement() {
        // follow the player
        FollowPlayer();
        // roate the camera
        RotateCamera();
    }

    private void FollowPlayer() {

        if (playerManager.isAiming) { // zoom into over shoulder position while aiming
            targetPosition = Vector3.SmoothDamp(transform.position, aimedCameraPosition.transform.position, ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);
            transform.position = targetPosition;
        }
        else {
            targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);
            transform.position = targetPosition;
        }
    }

    private void RotateCamera() {

        if (playerManager.isAiming) {

            cameraPivot.localRotation = Quaternion.Euler(0,0,0);

            lookAmountVertical += inputHandler.mouseX;
            lookAmountHorizontal -= inputHandler.mouseY;
            lookAmountHorizontal = Mathf.Clamp(lookAmountHorizontal, minPivotAngle, maxPivotAngle);
        
            cameraRotation = Vector3.zero;
            cameraRotation.y = lookAmountVertical;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, aimedCameraSmoothTime);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = lookAmountHorizontal;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, aimedCameraSmoothTime);
            cameraObject.transform.localRotation = targetRotation;
        }
        else {

            cameraObject.transform.localRotation = Quaternion.Euler(0,0,0); // reset position

            lookAmountVertical += inputHandler.mouseX;
            lookAmountHorizontal -= inputHandler.mouseY;
            lookAmountHorizontal = Mathf.Clamp(lookAmountHorizontal, minPivotAngle, maxPivotAngle);
        
            cameraRotation = Vector3.zero;
            cameraRotation.y = lookAmountVertical;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraSmoothTime);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = lookAmountHorizontal;
            targetRotation = Quaternion.Euler(cameraRotation);
            targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, cameraSmoothTime);
            cameraPivot.localRotation = targetRotation;
        }
        
    }
}

