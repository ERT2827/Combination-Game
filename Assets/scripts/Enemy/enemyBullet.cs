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

        if(player != null){
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity =  new Vector2(direction.x, direction.y).normalized * speed;

            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        playerController player = other.GetComponent<playerController>();
        EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();

        if(player != null){
            player.Ptakedamage();
        }

        if(other.gameObject.layer != environment){
            Destroy(gameObject);
        }


    }

}
