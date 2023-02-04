using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class elevatorHandling : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlatformToMove;

    private bool canInteract;
    private bool isMoving;

    private GameObject Player;

    GameObject bottomInstruBar;
    Text instruText;

    void Start() {
        Player = GameObject.Find("Player");

        bottomInstruBar = GameObject.Find("bottomInstruBar");
        instruText = GameObject.Find("instruText").GetComponent<Text>();

        bottomInstruBar.transform.localScale = new Vector3(0, 0, 0);

        GameObject.Find("mainArea").transform.localScale = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Interaction stuffies
            Debug.Log("Player could interact");
            canInteract = true;

            bottomInstruBar.transform.localScale = new Vector3(1, 1, 1);
            instruText.text = "Press E to use elevator";
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Interaction stuffies
            Debug.Log("Player couldnt interact");
            canInteract = false;

            bottomInstruBar.transform.localScale = new Vector3(0, 0, 0);
            instruText.text = "";
        }
    }

    void Update() {
        if (canInteract && Input.GetKeyDown(KeyCode.E)) {
            if (!isMoving) {
                Debug.Log("Player interacted");
                isMoving = true;

                bottomInstruBar.transform.localScale = new Vector3(0, 0, 0);
                instruText.text = "";
            }
        }

        if (isMoving) {
            PlatformToMove.transform.Translate(Vector3.down * Time.deltaTime * 2);
            Invoke("TeleportToPlayArea", 5);
        }
    }

    void TeleportToPlayArea() {
        // Teleport player to play area
        GameObject.Find("mainArea").transform.localScale = new Vector3(1,1,1);

        Player.transform.position = new Vector3(-2.61f, -16.66f, -267.07f);
        RenderSettings.fog = false;
        CancelInvoke();

        GameObject.Find("TutorialMap").transform.localScale = new Vector3(0, 0, 0);
    }
}
