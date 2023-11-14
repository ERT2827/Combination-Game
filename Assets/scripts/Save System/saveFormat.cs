using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class saveFormat{

    public bool[] levelUnlock = {true, false, false, false, false, false, false};
    public int level1Medal = 0;
    public int level2Medal = 0;
    public int level3Medal = 0;
    public int level4Medal = 0;
    public int level5Medal = 0;
    public int level6Medal = 0;
    public int level7Medal = 0;

    public float bestTime1 = 100000;
    public float bestTime2 = 100000;
    public float bestTime3 = 100000;
    public float bestTime4 = 100000;
    public float bestTime5 = 100000;
    public float bestTime6 = 100000;
    public float bestTime7 = 100000;
    
    public int currentSkin = 0;
    


    public saveFormat(usableInformation info){
        for (int i = 0; i < levelUnlock.Length; i++)
        {
            levelUnlock[i] = info.levelUnlock[i];
        }
        level1Medal = info.level1Medal;
        level2Medal = info.level2Medal;
        level3Medal = info.level3Medal;
        level4Medal = info.level4Medal;
        level5Medal = info.level5Medal;
        level6Medal = info.level6Medal;
        level7Medal = info.level7Medal;

        bestTime1 = info.bestTime1;
        bestTime2 = info.bestTime2;
        bestTime3 = info.bestTime3;
        bestTime4 = info.bestTime4;
        bestTime5 = info.bestTime5;
        bestTime6 = info.bestTime6;
        bestTime7 = info.bestTime7;

        currentSkin = info.currentSkin;
    }

}



    


