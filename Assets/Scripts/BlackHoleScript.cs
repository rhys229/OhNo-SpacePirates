using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public float               speed = 4f;
    public int direction;
    private Rigidbody2D        rb;
    public GameObject manager;
    public EnemyManagerScript ManagerScript;
    
    public Animator animator;
    public CircleCollider2D collider;
    public bool exploding = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
        manager = GameObject.Find("EnemyManager");
        ManagerScript = manager.GetComponent<EnemyManagerScript>();
        StartCoroutine("Launch");
    }

    
    private IEnumerator Launch() {
        //yield return new WaitForSeconds(1);
        //rb.AddForce(transform.right * -1);
        yield return new WaitForSeconds(.6f);
        rb.AddForce(transform.up * speed * direction);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ManagerScript.score += 10;
            ManagerScript.tally++;
            Destroy(other.gameObject);
            if (exploding == false)
            {
                StartCoroutine("Stop");
            }
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            return;
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
    IEnumerator Stop()
    {
        exploding = true;
        animator.SetTrigger("Explode");
        yield return new WaitForSeconds(.4f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(15f);
        Destroy(this.gameObject);
        exploding = false;
    }
}
