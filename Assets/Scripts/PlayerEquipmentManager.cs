using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{

    public WeaponLoaderSlot  WeaponLoaderSlot;

    [Header("Current Equipment")]
    public WeaponItem weapon;
    public WeaponManager weaponManager;

    private void Awake() {
        LoadWeaponLoaderSlot();
    }

    private void Start() {
        LoadCurrentWeapon();
    }

    private void LoadWeaponLoaderSlot() {
        // For now, only head slot exists
        WeaponLoaderSlot = GetComponentInChildren<WeaponLoaderSlot>();
    }

    private void LoadCurrentWeapon() {
        // load weapon
        WeaponLoaderSlot.LoadWeaponModel(weapon);
        weaponManager = WeaponLoaderSlot.currentWeaponModel.GetComponentInChildren<WeaponManager>();
        
        // change player movement/ idle animation (for later)
    }
}
