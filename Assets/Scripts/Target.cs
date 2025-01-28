//lets add some target
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Target : MonoBehaviour
{
    public Score scoreManager;

    // amount of time before target despawns
    public float time = 1;
    public int numTargets = 5;

    private bool isHit = false;
 
    //this method is called whenever a collision is detected
    private void OnCollisionEnter(Collision collision) {
        
        // only detect collisions by projectiles
        if (collision.gameObject.name == "Bullet(Clone)") {

            // keep track of number of enemies hit
            if (isHit == false) {
                scoreManager.addHit();
            }
            
            //on collision adding point to the score
            scoreManager.AddPoint();
    
            // printing if collision is detected on the console
            Debug.Log("Collision Detected");

            //after collision is detected destroy the gameobject
            Destroy(gameObject, time);
        }
    }
}