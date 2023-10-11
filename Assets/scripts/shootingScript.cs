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
    }

    private void Update() {
        if (Input.GetMouseButtonDown (0) && !onCoolDown) {    
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = -5f; // zero z

            if (hit.collider.tag == "enemy") {
                Instantiate(reticles[1], mouseWorldPos, Quaternion.identity);
            }else{
                Instantiate(reticles[0], mouseWorldPos, Quaternion.identity);
            }

            StartCoroutine(coolDown());
		}
    }


    IEnumerator coolDown(){
        onCoolDown = true; 

        yield return new WaitForSeconds(coolDownTime);

        onCoolDown = false;
    }

}
