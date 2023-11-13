using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallRun : MonoBehaviour
{
    private playerController playa;
    SpriteRenderer spriteRenderer;
    GameObject parant;

    [SerializeField] private bool booster;

    private void Start() {
        parant = gameObject.transform.parent.gameObject;
        spriteRenderer = parant.GetComponent<SpriteRenderer>();
    }
    
    
    private void OnTriggerEnter2D(Collider2D other) {
        playa = other.GetComponent<playerController>();

        if(playa != null && !booster){
            spriteRenderer.color = Color.yellow;
            playa.runningWall = parant;
        }else if(playa != null && booster){
            spriteRenderer.color = Color.blue;
            playa.boostedDismount = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other) {

        if (playa != null){
            spriteRenderer.color = Color.grey;
            playa.runningWall = null;
        }else if(playa != null && booster){
            spriteRenderer.color = Color.grey;
            playa.boostedDismount = false;
        }
    }
}
