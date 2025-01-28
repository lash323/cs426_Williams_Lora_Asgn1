using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Crosshair")]
    public GameObject crossHair;

    public void Start() {
        Cursor.visible = false;
    }
}
