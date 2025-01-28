using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour

{

    public Score scoreManager;

    //this method is called whenever a collision is detected
    private void OnCollisionEnter(Collision collision) {
        if ((collision.gameObject.name == "Player") && (scoreManager.getHits() >= 5) ) {
            Debug.Log("Restart: Collision detected. " + collision.gameObject.name);

            // restart the level
            SceneManager.LoadScene(0);
        }
    }

}


