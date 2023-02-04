using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopInteractionPossibility : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject bottomInstruBar;
    Text instruText;

    private void Start() {
        bottomInstruBar = GameObject.Find("bottomInstruBar");
        instruText = GameObject.Find("instruText").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Interaction stuffies
            Debug.Log("Player could interact");
            GameObject.Find("shopUpgrade").GetComponent<LevelUp>().isShopOpen = true;

            bottomInstruBar.transform.localScale = new Vector3(1, 1, 1);
            instruText.text = "Press E to buy upgrades";
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Interaction stuffies
            Debug.Log("Player couldnt interact");
            GameObject.Find("shopUpgrade").GetComponent<LevelUp>().isShopOpen = false;

            bottomInstruBar.transform.localScale = new Vector3(0, 0, 0);
            instruText.text = "";
        }
    }
}
