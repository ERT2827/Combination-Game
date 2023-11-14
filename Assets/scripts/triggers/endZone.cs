using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endZone : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    GameObject ui;
    [SerializeField] private endScreenScript screenScript;

    private void Start() {
        ui = GameObject.Find("UI");
        // endScreen = GameObject.Find("endScreen");
        // screenScript = endScreen.transform.GetChild(0).gameObject.GetComponent<endScreenScript>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Player"){
            return;
        }

        playerController playerc = other.gameObject.GetComponent<playerController>();
        playerc.levelComplete = true;

        screenScript.setScreen();

        endScreen.SetActive(true);
        ui.SetActive(false);
    }
}
