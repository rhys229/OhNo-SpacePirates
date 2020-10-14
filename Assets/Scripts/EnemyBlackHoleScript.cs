using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlackHoleScript : MonoBehaviour
{
    public float               speed = 2f;
    public int direction;
    private Rigidbody2D        rb;

    public Animator animator;
    public CircleCollider2D collider;
    public bool exploding = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
        StartCoroutine("Launch");
    }

    private void Update()
    {
        if (exploding == false && transform.position.y < -4)
        {
            StartCoroutine("Explode");
        }
    }


    private IEnumerator Launch() {
        //yield return new WaitForSeconds(1);
        //rb.AddForce(transform.right * -1);
        rb.AddForce(transform.up * speed * direction);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("We hit the player");
        }

        if (other.gameObject.tag == "wall")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Ally")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ally")
        {
            //award points
            Debug.Log("We Hit an Ally Ship");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "wall")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Explode()
    {
        Debug.Log("Exploding");
        exploding = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
        exploding = false;
    }
}
