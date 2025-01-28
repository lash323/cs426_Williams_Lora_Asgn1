using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerCamera playerCamera;
    InputHandler inputHandler;
    PlayerMovement playerMovement;
    PlayerEquipmentManager playerEquipmentManager;

    [Header("Player Flags")]
    public bool isAiming;

    private void Awake() {
        playerCamera = FindAnyObjectByType<PlayerCamera>();
        inputHandler = GetComponent<InputHandler>();
        playerMovement = GetComponent<PlayerMovement>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
    }

    public void Update() {
        inputHandler.HandleAllInput();
        isAiming = inputHandler.aimingInput;
    }

    public void FixedUpdate() {
        float delta = Time.deltaTime;
        playerMovement.HandleAllMovement(delta);
    }

    private void LateUpdate() {
        playerCamera.HandleAllCameraMovement();
    }

    public void UseWeapon() {
        GameObject weapon = playerEquipmentManager.WeaponLoaderSlot.currentWeaponModel;
        playerEquipmentManager.weaponManager.ShootWeapon(playerCamera, weapon);
    }
}
