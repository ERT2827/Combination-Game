using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    public float speed = 2f;

    [SerializeField] private LayerMask environment;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(despawnSelf());

        if(player != null){
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity =  new Vector2(direction.x, direction.y).normalized * speed;

            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        playerController playerC = other.GetComponent<playerController>();
        EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();

        Debug.Log(other);

        if(playerC != null && playerC.speedX < 11){
            playerC.Ptakedamage();
            Destroy(gameObject);
        }else if(other.gameObject.layer == environment){
            Destroy(gameObject);
        }


    }


    IEnumerator despawnSelf(){
        yield return new WaitForSeconds(8f);

        Destroy(gameObject);
    }

}
