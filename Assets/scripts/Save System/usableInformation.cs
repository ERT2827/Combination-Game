using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usableInformation : MonoBehaviour
{
    [Header("Level Status")]
    public bool[] levelUnlock = {true, false, false, false, false, false, false};
    public int level1Medal = 0;
    public int level2Medal = 0;
    public int level3Medal = 0;
    public int level4Medal = 0;
    public int level5Medal = 0;
    public int level6Medal = 0;
    public int level7Medal = 0;

    [Header("Times")]

    public float bestTime1 = 100000;
    public float bestTime2 = 100000;
    public float bestTime3 = 100000;
    public float bestTime4 = 100000;
    public float bestTime5 = 100000;
    public float bestTime6 = 100000;
    public float bestTime7 = 100000;


    private void Start() {
        loadInfo();
    }


    public void loadInfo(){
    saveFormat data = saveScript.LoadPlayer();

    for (int i = 0; i < levelUnlock.Length; i++)
    {
        levelUnlock[i] = data.levelUnlock[i];
    }
    
    level1Medal = data.level1Medal;
    level2Medal = data.level2Medal;
    level3Medal = data.level3Medal;
    level4Medal = data.level4Medal;
    level5Medal = data.level5Medal;
    level6Medal = data.level6Medal;
    level7Medal = data.level7Medal;

    bestTime1 = data.bestTime1;
    bestTime2 = data.bestTime2;
    bestTime3 = data.bestTime3;
    bestTime4 = data.bestTime4;
    bestTime5 = data.bestTime5;
    bestTime6 = data.bestTime6;
    bestTime7 = data.bestTime7;    
    }

    public void saveInfo(){
        saveScript.SavePlayer(this);
    }

    public void resetInfo(){
        for (int i = 1; i < levelUnlock.Length; i++)
        {
            levelUnlock[i] = false;
        }
        
        level1Medal = 0;
        level2Medal = 0;
        level3Medal = 0;
        level4Medal = 0;
        level5Medal = 0;
        level6Medal = 0;
        level7Medal = 0;

        bestTime1 = 100000;
        bestTime2 = 100000;
        bestTime3 = 100000;
        bestTime4 = 100000;
        bestTime5 = 100000;
        bestTime6 = 100000;
        bestTime7 = 100000;

        saveInfo();
    }
}


