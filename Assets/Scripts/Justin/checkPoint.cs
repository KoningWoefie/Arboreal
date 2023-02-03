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
    private GameObject SelectionManager;

    private GameObject Player;
    void Start()
    {
        checkpointNumber = int.Parse(gameObject.name.Substring(10));

        instructionalPopup = GameObject.Find("instructionalPopup");
        instructionalTitle = GameObject.Find("instructionalTitle").GetComponent<Text>();
        instructionalInstruction = GameObject.Find("instructionalInstruction").GetComponent<Text>();

        instructionalPopup.SetActive(false);
        instructionalPopup.transform.localScale = new Vector3(1, 1, 1);

        Player = GameObject.Find("Player");
        Player.GetComponent<rootForesight>().enabled = true;
        Player.GetComponent<RootManipulation>().enabled = false;


        SelectionManager = GameObject.Find("SelectionManager");
        SelectionManager.SetActive(false);


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
                    instructionalTitle.text = "<b>UNLOCKED:</b> Root Manipulation";
                    instructionalInstruction.text = "Pick up objects with <b>E</b>, then change the position with <b>scrollwheel</b> and hold <b>Z</b> and use your <b>scrollwheel</b> at the same time to rotate the object. Yeah this makes no sense, no we don't care";
                    Player.GetComponent<RootManipulation>().enabled = true;
                    instructionalPopup.SetActive(true);
                    SelectionManager.SetActive(true);
                    //SelfDestruct();

                    DisableEverything();
                }
            }
        }
    }

    void SelfDestruct() {
        Destroy(gameObject);
    }

    void PopupReset()
    {
        EnableEverything();
        instructionalTitle.text = "";
        instructionalInstruction.text = "";
        instructionalPopup.SetActive(false);
        SelfDestruct();
    }

    void DisableEverything() {
        Debug.Log("DISABLED EVERYTHING");
        // set level speed to 0
        Camera.main.GetComponent<camera>().enabled = false;
        Player.GetComponent<Player>().enabled = false;
    }

    void EnableEverything() {
        Debug.Log("ENABLED EVERYTHING");
        // set level speed to 1
        Camera.main.GetComponent<camera>().enabled = true;
        Player.GetComponent<Player>().enabled = true;
    }
}
