using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject bullet;

    public float force = 40f;
    public void ShootWeapon(PlayerCamera playerCamera, GameObject weapon) {

        // // allows us to track what we hit
        // RaycastHit hit; // the object that's being hit
        // if (Physics.Raycast(playerCamera.cameraObject.transform.position, playerCamera.cameraObject.transform.forward, out hit)) {
        //     Debug.Log(hit.transform.gameObject.name);
        // }  // Note: above creates raycast (??) from camera to whatever it hits


        // Find exact hit position
        Ray ray = playerCamera.cameraObject.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit; // object that's being hit 

        // get point where ray hits; otherwise random point far away from player
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit)) {
            targetPoint = hit.point;
        }
        else {
            targetPoint = ray.GetPoint(75);
        }

        // calculate distance from weapon to target
        Vector3 direction = targetPoint - weapon.transform.position;

        // create bullets
        // GameObject newBullet = GameObject.Instantiate(bullet, weapon.transform.position, playerCamera.transform.rotation) as GameObject;
        GameObject newBullet = Instantiate(bullet, weapon.transform.position, Quaternion.identity);
        
        // rotate bullet to shoot direction
        newBullet.transform.forward = direction.normalized;

        // add force to bullet
        newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * force, ForceMode.Impulse);
        // newBullet.GetComponent<Rigidbody>().AddForce(playerCamera.cameraObject.transform.up * 5, ForceMode.Impulse);





        // newBullet.GetComponent<Rigidbody>().linearVelocity += Vector3.up * 2;
        // newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1500);

        // newBullet.GetComponent<Rigidbody>().linearVelocity = playerCamera.transform.forward * 10f;

    }
}
