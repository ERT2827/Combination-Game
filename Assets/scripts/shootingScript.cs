using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shootingScript : MonoBehaviour
{
    SpriteRenderer sRnd;

    private void Awake() {
        sRnd = gameObject.GetComponent<SpriteRenderer>();
    }
    
    private void FixedUpdate() {
        if (Input.GetMouseButton(0))
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = -5f; // zero z
            transform.position = mouseWorldPos;
            StartCoroutine(colSet());
        }
    }


    IEnumerator colSet(){
        sRnd.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(1);

        sRnd.color = new Color(1, 1, 1, 0);

    }

}
