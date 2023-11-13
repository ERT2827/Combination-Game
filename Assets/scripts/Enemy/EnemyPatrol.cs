using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Script References
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    public SpriteRenderer sprite;

    shootingScript shootS;


    [SerializeField] float health, maxHealth = 3f;

    // Start is called before the first frame update
    void Start()
    {
        shootS = GameObject.Find("Player").GetComponent<shootingScript>();
        //Assigning of variables to references.
        rb = GetComponent<Rigidbody2D>();
        //initial start point.
        currentPoint = pointB.transform;
        //sets enemy health to max health at the beginnging of the scene.
        health = maxHealth;
    }

    // Update is called once per frame.
    void Update()
    {
        //Sets initial direction for enemy (sets target point).
        Vector2 point = currentPoint.position - transform.position;

        //Tells enemy to change direction when reaching the target point.
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        //If enemy has reached the current target point the target point is changed (i.e. if enemy reaches pointB target point is changed to pointA).
        if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }


    }
    //Changes direction of sprite so it is facing the right way.
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    //Visualization tool used to make pointA, pointB ,and partol pathway visble for testing.
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 1f);
        Gizmos.DrawWireSphere(pointB.transform.position, 1f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
    // Does damage to enemies upon click 
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Timer timer = GameObject.Find("Player").GetComponent<Timer>();
            timer.currentTime -= 10;
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        if (!shootS.onCoolDown)
        {
            TakeDamage(1);
            StartCoroutine(FlashRed());
        }
    }


    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
    
}