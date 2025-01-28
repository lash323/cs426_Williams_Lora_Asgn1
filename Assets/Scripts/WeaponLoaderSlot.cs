using UnityEngine;

public class WeaponLoaderSlot : MonoBehaviour
{
    public GameObject currentWeaponModel;

    // // destory previous weapon so player can only hold one weapon
    // public void UnLoadDestroyWeapon() {
    //     if (currentWeaponModel != null) {
    //         Destroy(currentWeaponModel);
    //     }
    // }

    // load and equip weapon onto player
    public void LoadWeaponModel(WeaponItem weapon){
        // UnLoadDestroyWeapon();

        if (weapon == null) {
            return;
        }

        GameObject weaponModel = Instantiate(weapon.itemModel, transform);
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localRotation = Quaternion.identity;
        weaponModel.transform.localScale = Vector3.one;
        currentWeaponModel = weaponModel;
    }
}
