using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shootingScript : MonoBehaviour
{
    [SerializeField] private GameObject[] reticles;

    public bool onCoolDown;

    [SerializeField] private float coolDownTime;

    
    SpriteRenderer sRnd;

    private void Awake() {
        sRnd = gameObject.GetComponent<SpriteRenderer>();

        Debug.Log(reticles[0]);
    }

    private void Update() {
        #if UNITY_STANDALONE_WIN

        if (Input.GetMouseButtonDown (0) && !onCoolDown) {    
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = -5f; // zero z

            // if (hit != null){
            //     if (hit.collider.tag == "enemy") {
            //         Instantiate(reticles[0], mouseWorldPos, Quaternion.identity);
            //     }
            // }
            // else{
            //     Instantiate(reticles[1], mouseWorldPos, Quaternion.identity);
            // }

            Instantiate(reticles[1], mouseWorldPos, Quaternion.identity);


            StartCoroutine(coolDown());
		}

        #endif
    }

    #if UNITY_ANDROID

    public void mobileshoot(Vector3 TP){
            Ray ray = Camera.main.ScreenPointToRay(TP);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
            var touchWorldPos = Camera.main.ScreenToWorldPoint(TP);
            touchWorldPos.z = -5f; // zero z

            Instantiate(reticles[1], touchWorldPos, Quaternion.identity);


            StartCoroutine(coolDown());
    }

    #endif


    IEnumerator coolDown(){
        onCoolDown = true; 

        yield return new WaitForSeconds(coolDownTime);

        onCoolDown = false;
    }

}
