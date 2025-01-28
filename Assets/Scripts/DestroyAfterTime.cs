using UnityEngine;
 
// script for removing objects (such as bullets) 
// after a specififed amount of time
public class DestroyAfterTime : MonoBehaviour
{
    public float time = 5;
    private void Awake() {
        Destroy(gameObject, time);
    }
}
