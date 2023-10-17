using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallRun : MonoBehaviour
{
    playerController playa;
    SpriteRenderer spriteRenderer;
    GameObject parant;

    private void Start() {
        parant = gameObject.transform.parent.gameObject;
        spriteRenderer = parant.GetComponent<SpriteRenderer>();
    }
    
    
    private void OnTriggerEnter2D(Collider2D other) {
        playa = other.GetComponent<playerController>();

        if(playa != null){
            spriteRenderer.color = Color.yellow;
            playa.runningWall = parant;
        }
    }


    private void OnTriggerExit2D(Collider2D other) {

        if (playa != null){}
        {
            spriteRenderer.color = Color.grey;
            playa.runningWall = null;
        }
    }
}
