using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorHandling : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlatformToMove;

    private bool canInteract;
    private bool isMoving;

    private GameObject Player;

    void Start() {
        Player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Interaction stuffies
            Debug.Log("Player could interact");
            canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Interaction stuffies
            Debug.Log("Player couldnt interact");
            canInteract = false;
        }
    }

    void Update() {
        if (canInteract && Input.GetKeyDown(KeyCode.E)) {
            if (!isMoving) {
                Debug.Log("Player interacted");
                isMoving = true;
            }
        }

        if (isMoving) {
            PlatformToMove.transform.Translate(Vector3.down * Time.deltaTime * 2);
            Invoke("TeleportToPlayArea", 5);
        }
    }

    void TeleportToPlayArea() {
        // Teleport player to play area
        Player.transform.position = new Vector3(-0.29f, 0.56f, -15.6f);
        RenderSettings.fog = false;
        CancelInvoke();
    }
}
