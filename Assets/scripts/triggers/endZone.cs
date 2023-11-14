using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endZone : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    GameObject ui;
    [SerializeField] private endScreenScript screenScript;

    [Header("Par times (in seconds)")]

    [SerializeField] private float[] parTimes; //in seconds
    private usableInformation savedData;

    private void Start() {
        ui = GameObject.Find("UI");
        // endScreen = GameObject.Find("endScreen");
        // screenScript = endScreen.transform.GetChild(0).gameObject.GetComponent<endScreenScript>();
        savedData = GameObject.Find("SaveObject").GetComponent<usableInformation>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Player"){
            return;
        }

        playerController playerc = other.gameObject.GetComponent<playerController>();
        Timer timer = other.gameObject.GetComponent<Timer>();
        playerc.levelComplete = true;

        ui.SetActive(false);

        int n = resultCheck(timer.currentTime);

        endScreen.SetActive(true);
        screenScript.setScreen();
        screenScript.setMedal(n);
        medalSave(n);
        Debug.Log(timer.currentTime);

    }


    int resultCheck(float timeTotal){
        if(timeTotal < parTimes[2]){
            return 3;
        }else if (timeTotal < parTimes[1]){
            return 2;
        }else if (timeTotal < parTimes[0])
        {
            return 1;
        }else{
            return 0;
        }
    }

    void medalSave(int medal){
        if(SceneManager.GetActiveScene().name == "Movement101"){
            if(medal < savedData.level1Medal || medal == 0){
            return;
            }else{
                savedData.level1Medal = medal;
                savedData.levelUnlock[1] = true;
                savedData.saveInfo();
            }
        }else if(SceneManager.GetActiveScene().name == "wallruns"){
            if(medal < savedData.level2Medal || medal == 0){
            return;
            }else{
                savedData.level2Medal = medal;
                savedData.levelUnlock[2] = true;
                savedData.saveInfo();
            }
        }else if(SceneManager.GetActiveScene().name == "Level1"){
            if(medal < savedData.level3Medal || medal == 0){
            return;
            }else{
                savedData.level3Medal = medal;
                savedData.levelUnlock[3] = true;
                savedData.saveInfo();
            }
        }else if(SceneManager.GetActiveScene().name == "Level2"){
            if(medal < savedData.level4Medal || medal == 0){
            return;
            }else{
                savedData.level4Medal = medal;
                savedData.levelUnlock[4] = true;
                savedData.saveInfo();
            }
        }else if(SceneManager.GetActiveScene().name == "Level3"){
            if(medal < savedData.level5Medal || medal == 0){
            return;
            }else{
                savedData.level5Medal = medal;
                savedData.levelUnlock[5] = true;
                savedData.saveInfo();
            }
        }else if(SceneManager.GetActiveScene().name == "Level4"){
            if(medal < savedData.level6Medal || medal == 0){
            return;
            }else{
                savedData.level6Medal = medal;
                savedData.levelUnlock[6] = true;
                savedData.saveInfo();
            }
        }else if(SceneManager.GetActiveScene().name == "Level5"){
            if(medal < savedData.level7Medal || medal == 0){
            return;
            }else{
                savedData.level7Medal = medal;
                savedData.saveInfo();
            }
        }else if(SceneManager.GetActiveScene().name == "Level6"){
            if(medal < savedData.level1Medal || medal == 0){
            return;
            }else{
                savedData.level1Medal = medal;
                savedData.saveInfo();
            }
        }else if(SceneManager.GetActiveScene().name == "Level7"){
            if(medal < savedData.level1Medal || medal == 0){
            return;
            }else{
                savedData.level1Medal = medal;
                savedData.saveInfo();
            }
        }
    }
}
