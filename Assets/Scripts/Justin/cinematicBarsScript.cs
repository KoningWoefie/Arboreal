using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematicBarsScript : MonoBehaviour
{
    GameObject topBlackBar;
    GameObject bottomBlackBar;
    void Start()
    {
        topBlackBar = GameObject.Find("topBlackBar");
        bottomBlackBar = GameObject.Find("bottomBlackBar");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.UpArrow)) {
        //    StartCoroutine(OutwardsBars());
        //}
//
        //if (Input.GetKeyUp(KeyCode.DownArrow)) {
        //    StartCoroutine(InwardBars());
        //}
    }

    public IEnumerator InwardBars() {
        float startTime = Time.time;
        while (Time.time < startTime + 1f) {
            topBlackBar.transform.localPosition = Vector3.Lerp(topBlackBar.transform.localPosition, new Vector3(0, 850, 0), (Time.time - startTime) / 3f);
            // move bottom black bar from -540 to -330
            bottomBlackBar.transform.localPosition = Vector3.Lerp(bottomBlackBar.transform.localPosition, new Vector3(0, -850, 0), (Time.time - startTime) / 3f);
            yield return null;
        }
    }

    public IEnumerator OutwardsBars() {
        float startTime = Time.time;
        while (Time.time < startTime + 1f) {
            topBlackBar.transform.localPosition = Vector3.Lerp(topBlackBar.transform.localPosition, new Vector3(0, 1100, 0), (Time.time - startTime) / 3f);
            // move bottom black bar from -540 to -330
            bottomBlackBar.transform.localPosition = Vector3.Lerp(bottomBlackBar.transform.localPosition, new Vector3(0, -1100, 0), (Time.time - startTime) / 3f);
            yield return null;
        }
    }

}
