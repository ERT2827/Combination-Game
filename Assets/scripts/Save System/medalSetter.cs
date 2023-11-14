using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medalSetter : MonoBehaviour
{
    [Header("Medal Variables")]
    [SerializeField] Transform[] medalHolders;
    [SerializeField] GameObject[] medalPrefabs;

    [Header("Level Variables")]
    [SerializeField] GameObject[] levelButtons;
    public List<bool> levelUnlocked = new List<bool>();

    [Header("Other variables")]
    [SerializeField] private usableInformation info;
    public List<int> medals = new List<int>();

    private void Start() {
        info = GameObject.Find("SaveController").GetComponent<usableInformation>();
        StartCoroutine(Delayer());
    }

    void spawnMedal(int rank, Transform pos){
        Debug.Log(rank);
        
        if (rank == 3){
            Instantiate(medalPrefabs[2], pos.position, Quaternion.identity, pos);
        }else if(rank == 2){
            Instantiate(medalPrefabs[1], pos.position, Quaternion.identity, pos);
        }else if (rank == 1){
            Instantiate(medalPrefabs[0], pos.position, Quaternion.identity, pos);
        }
    }

    void activateLevel(int n, bool unlocked){
        levelButtons[n].SetActive(unlocked);
    }

    IEnumerator Delayer(){
        yield return new WaitForSeconds(0.1f);
        
        medals.Add(info.level1Medal);
        medals.Add(info.level2Medal);
        medals.Add(info.level3Medal);
        medals.Add(info.level4Medal);
        medals.Add(info.level5Medal);
        medals.Add(info.level6Medal);
        medals.Add(info.level7Medal);

        foreach (bool b in info.levelUnlock)
        {
            levelUnlocked.Add(b);
        }

        for (int i = 0; i < 7; i++)
        {
            spawnMedal(medals[i], medalHolders[i]);
            activateLevel(i, levelUnlocked[i]);
        }

        levelButtons[0].SetActive(true);
    }

}
