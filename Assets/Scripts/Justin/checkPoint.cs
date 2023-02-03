using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkPoint : MonoBehaviour
{
    private int checkpointNumber;
    GameObject instructionalPopup;
    Text instructionalTitle;
    Text instructionalInstruction;
    void Start()
    {
        checkpointNumber = int.Parse(gameObject.name.Substring(10));

        instructionalPopup = GameObject.Find("instructionalPopup");
        instructionalTitle = GameObject.Find("instructionalTitle").GetComponent<Text>();
        instructionalInstruction = GameObject.Find("instructionalInstruction").GetComponent<Text>();

        instructionalPopup.SetActive(false);
        instructionalPopup.transform.localScale = new Vector3(1, 1, 1);

    }

    void Update()
    {
        // If player presses Enter while the instructional popup is active, reset the popup
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PopupReset();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(!instructionalPopup.activeSelf) {
            if (other.gameObject.tag == "Player")
            {
                if(checkpointNumber == 1) {
                    Debug.Log("Fakka strijder, du er nået til checkpoint 1");
                    instructionalTitle.text = "YOU HAVE ACHIEVED SEXY";
                    instructionalInstruction.text = "You have now unlocked ultimate sexy, you are a sexlord. No one has ever looked this sexy, a tree, sexy. Press 'S' to become even sexier";
                    instructionalPopup.SetActive(true);
                    //SelfDestruct();
                }
            }
        }
    }

    void SelfDestruct() {
        Destroy(gameObject);
    }

    void PopupReset()
    {
        instructionalTitle.text = "";
        instructionalInstruction.text = "";
        instructionalPopup.SetActive(false);
        SelfDestruct();
    }
}
