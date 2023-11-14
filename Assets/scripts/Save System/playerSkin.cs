using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSkin : MonoBehaviour
{
    
    [SerializeField] Sprite[] sprites;
    private usableInformation data;
    
    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<usableInformation>();
        
        if(GameObject.Find("Player") != null){
            StartCoroutine(skinSetter());
        }
    }

    void setSkin(int s){
        data.currentSkin = s;
        data.saveInfo();
    }

    public void set1(){
        setSkin(0);
    }

    public void set2(){
        setSkin(1);
    }

    public void set3(){
        setSkin(2);
    }

    IEnumerator skinSetter(){
        yield return new WaitForSeconds(0.1f);

        SpriteRenderer SR = GameObject.Find("Player").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        // Debug.Log(data.currentSkin + " Skin");
        SR.sprite = sprites[data.currentSkin];
    }
}
